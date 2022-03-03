using PropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TEST {

    public interface IDataList : IEnumerable {
        [XmlIgnore]
        public DataListCollection DLC { get; set; }
        void AcceptChanges();
        void RejectChanges();
        void AcceptChangesEfficient();
        void RejectChangesEfficient();
        bool HasChanges();
    }

    [Serializable]
    public class DataListCollection : IContainer, INotifyPropertyChanged {
        [XmlIgnore]
        public Dictionary<string, IDataList> DataLists = new Dictionary<string, IDataList>();

        public event PropertyChangedEventHandler PropertyChanged;

        [XmlIgnore]
        public ComponentCollection Components => throw new NotImplementedException();

        public DataList<T> AddToCollection<T>(DataList<T> value, [CallerMemberName] string propertyName = "") where T : DataItem, new() {
            value.DLC = this;
            DataLists[propertyName] = value;
            return value;
        }
        public IDataList AddToCollection(IDataList value, [CallerMemberName] string propertyName = "") {
            value.DLC = this;
            DataLists[propertyName] = value;
            return value;
        }

        public void AcceptChanges() {
            foreach (var dl in DataLists.Values) {
                dl.AcceptChanges();
            }
        }
        public void AcceptChangesEfficient() {
            foreach (var dl in DataLists.Values) {
                dl.AcceptChangesEfficient();
            }
        }
        public void RejectChanges() {
            foreach (var dl in DataLists.Values) {
                dl.RejectChanges();
            }
        }
        public void RejectChangesEfficient() {
            foreach (var dl in DataLists.Values) {
                dl.RejectChangesEfficient();
            }
        }

        public void Add(IComponent component) {
            var dl = (IDataList)component;
            AddToCollection(dl);
        }

        public void Add(IComponent component, string name) {
            var dl = (IDataList)component;
            AddToCollection(dl, name);
        }

        public void Remove(IComponent component) {
            var dl = (IDataList)component;
            dl.DLC = null;
            foreach (var item in DataLists.Where(kvp => kvp.Value == dl).ToList()) {
                DataLists.Remove(item.Key);
            }
        }

        public void Dispose() {
            DataLists.Clear();
        }
        public bool HasChanges() {
            return DataLists.Values.Any(x => x.HasChanges());
        }
    }

    [Serializable]
    public class DataList<T> : Collection<T>, IList<T>, IList, IComponent, IDataList, IBindingList, ICancelAddNew, IRaiseItemChangedEvents, INotifyPropertyChanged where T : DataItem, new() {
        private int addNewPos = -1;
        private bool raiseListChangedEvents = true;
        private bool raiseItemChangedEvents = false;

        [NonSerialized()]
        private PropertyDescriptorCollection itemTypeProperties = null;

        [NonSerialized()]
        private PropertyChangedEventHandler propertyChangedEventHandler = null;

        [NonSerialized()]
        private AddingNewEventHandler onAddingNew;

        [NonSerialized()]
        private ListChangedEventHandler onListChanged;

        [NonSerialized()]
        private int lastChangeIndex = -1;

        private bool allowNew = true;
        private bool allowEdit = true;
        private bool allowRemove = true;
        private bool userSetAllowNew = false;

        #region Constructors

        /// <devdoc>
        ///     Default constructor.
        /// </devdoc>
        public DataList() : base() {
            Initialize();
        }

        /// <devdoc>
        ///     Constructor that allows substitution of the inner list with a custom list.
        /// </devdoc>
        public DataList(IList<T> list) : base(list) {
            Initialize();
        }

        private void Initialize() {
            // Set the default value of AllowNew based on whether type T has a default constructor
            this.allowNew = ItemTypeHasDefaultConstructor;

            // Check for INotifyPropertyChanged
            if (typeof(INotifyPropertyChanged).IsAssignableFrom(typeof(T))) {
                // Supports INotifyPropertyChanged
                this.raiseItemChangedEvents = true;

                // Loop thru the items already in the collection and hook their change notification.
                foreach (T item in this.Items) {
                    HookPropertyChanged(item);
                }
            }
        }

        private bool ItemTypeHasDefaultConstructor {
            get {
                Type itemType = typeof(T);

                if (itemType.IsPrimitive) {
                    return true;
                }

                if (itemType.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance, null, new Type[0], null) != null) {
                    return true;
                }

                return false;
            }
        }

        #endregion

        #region AddingNew event

        /// <devdoc>
        ///     Event that allows a custom item to be provided as the new item added to the list by AddNew().
        /// </devdoc>
        public event AddingNewEventHandler AddingNew {
            add {
                bool allowNewWasTrue = AllowNew;
                onAddingNew += value;
                if (allowNewWasTrue != AllowNew) {
                    FireListChanged(ListChangedType.Reset, -1);
                }
            }
            remove {
                bool allowNewWasTrue = AllowNew;
                onAddingNew -= value;
                if (allowNewWasTrue != AllowNew) {
                    FireListChanged(ListChangedType.Reset, -1);
                }
            }
        }

        /// <devdoc>
        ///     Raises the AddingNew event.
        /// </devdoc>
        protected virtual void OnAddingNew(AddingNewEventArgs e) {
            if (onAddingNew != null) {
                onAddingNew(this, e);
            }
        }

        // Private helper method
        private object FireAddingNew() {
            AddingNewEventArgs e = new AddingNewEventArgs(null);
            OnAddingNew(e);
            return e.NewObject;
        }

        #endregion

        #region ListChanged event

        /// <devdoc>
        ///     Event that reports changes to the list or to items in the list.
        /// </devdoc>
        public event ListChangedEventHandler ListChanged {
            add {
                onListChanged += value;
            }
            remove {
                onListChanged -= value;
            }
        }

        /// <devdoc>
        ///     Raises the ListChanged event.
        /// </devdoc>
        protected virtual void OnListChanged(ListChangedEventArgs e) {
            if (onListChanged != null) {
                onListChanged(this, e);
            }
        }

        public bool RaiseListChangedEvents {
            get {
                return this.raiseListChangedEvents;
            }

            set {
                if (this.raiseListChangedEvents != value) {
                    this.raiseListChangedEvents = value;
                }
            }
        }

        /// <devdoc>
        /// </devdoc>
        public void ResetBindings() {
            FireListChanged(ListChangedType.Reset, -1);
        }

        /// <devdoc>
        /// </devdoc>
        public void ResetItem(int position) {
            FireListChanged(ListChangedType.ItemChanged, position);
        }

        // Private helper method
        private void FireListChanged(ListChangedType type, int index) {
            if (this.raiseListChangedEvents) {
                OnListChanged(new ListChangedEventArgs(type, index));
            }
        }

        #endregion

        #region Collection<T> overrides

        // Collection<T> funnels all list changes through the four virtual methods below.
        // We override these so that we can commit any pending new item and fire the proper ListChanged events.

        protected override void ClearItems() {
            EndNew(addNewPos);

            bool pc = this.raiseItemChangedEvents;
            foreach (T item in this.Items) {
                if (pc) {
                    UnhookPropertyChanged(item);
                }
                _deletedList.Add(item);
            }

            base.ClearItems();
            FireListChanged(ListChangedType.Reset, -1);
        }

        protected override void InsertItem(int index, T item) {
            EndNew(addNewPos);
            item.IsAdded = true;
            base.InsertItem(index, item);

            if (this.raiseItemChangedEvents) {
                HookPropertyChanged(item);
            }

            FireListChanged(ListChangedType.ItemAdded, index);
        }

        protected override void RemoveItem(int index) {
            // Need to all RemoveItem if this on the AddNew item
            if (!this.allowRemove && !(this.addNewPos >= 0 && this.addNewPos == index)) {
                throw new NotSupportedException();
            }

            EndNew(addNewPos);

            if (this.raiseItemChangedEvents) {
                UnhookPropertyChanged(this[index]);
            }

            _deletedList.Add(this[index]);
            base.RemoveItem(index);
            FireListChanged(ListChangedType.ItemDeleted, index);
        }

        protected override void SetItem(int index, T item) {
            if (this.raiseItemChangedEvents) {
                UnhookPropertyChanged(this[index]);
            }

            base.SetItem(index, item);

            if (this.raiseItemChangedEvents) {
                HookPropertyChanged(item);
            }

            FireListChanged(ListChangedType.ItemChanged, index);
        }

        #endregion

        #region ICancelAddNew interface

        /// <devdoc>
        ///     If item added using AddNew() is still cancellable, then remove that item from the list.
        /// </devdoc>
        public virtual void CancelNew(int itemIndex) {
            if (addNewPos >= 0 && addNewPos == itemIndex) {
                RemoveItem(addNewPos);
                addNewPos = -1;
            }
        }

        /// <devdoc>
        ///     If item added using AddNew() is still cancellable, then commit that item.
        /// </devdoc>
        public virtual void EndNew(int itemIndex) {
            if (addNewPos >= 0 && addNewPos == itemIndex) {
                addNewPos = -1;
            }
        }

        #endregion

        #region IBindingList interface

        /// <devdoc>
        ///     Adds a new item to the list. Calls <see cref='AddNewCore'> to create and add the item.
        ///
        ///     Add operations are cancellable via the <see cref='ICancelAddNew'> interface. The position of the
        ///     new item is tracked until the add operation is either cancelled by a call to <see cref='CancelNew'>,
        ///     explicitly commited by a call to <see cref='EndNew'>, or implicitly commmited some other operation
        ///     that changes the contents of the list (such as an Insert or Remove). When an add operation is
        ///     cancelled, the new item is removed from the list.
        /// </devdoc>
        public T AddNew() {
            var item = (T)((this as IBindingList).AddNew());
            item.IsAdded = true;
            return item;
        }

        object IBindingList.AddNew() {
            // Create new item and add it to list
            object newItem = AddNewCore();

            // Record position of new item (to support cancellation later on)
            addNewPos = (newItem != null) ? IndexOf((T)newItem) : -1;

            // Return new item to caller
            return newItem;
        }

        private bool AddingNewHandled {
            get {
                return onAddingNew != null && onAddingNew.GetInvocationList().Length > 0;
            }
        }

        /// <devdoc>
        ///     Creates a new item and adds it to the list.
        ///
        ///     The base implementation raises the AddingNew event to allow an event handler to
        ///     supply a custom item to add to the list. Otherwise an item of type T is created.
        ///     The new item is then added to the end of the list.
        /// </devdoc>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2113:SecureLateBindingMethods")]
        protected virtual object AddNewCore() {
            // Allow event handler to supply the new item for us
            object newItem = FireAddingNew();

            // If event hander did not supply new item, create one ourselves
            if (newItem == null) {

                Type type = typeof(T);
                //newItem = System.SecurityUtils.SecureCreateInstance(type);
                newItem = new T();
            }

            var newT = (T)newItem;
            newT.IsAdded = true;
            // Add item to end of list. Note: If event handler returned an item not of type T,
            // the cast below will trigger an InvalidCastException. This is by design.
            Add(newT);

            // Return new item to caller
            return newItem;
        }

        /// <devdoc>
        /// </devdoc>
        public bool AllowNew {
            get {
                //If the user set AllowNew, return what they set.  If we have a default constructor, allowNew will be 
                //true and we should just return true.
                if (userSetAllowNew || allowNew) {
                    return this.allowNew;
                }
                //Even if the item doesn't have a default constructor, the user can hook AddingNew to provide an item.
                //If there's a handler for this, we should allow new.
                return AddingNewHandled;
            }
            set {
                bool oldAllowNewValue = AllowNew;
                userSetAllowNew = true;
                //Note that we don't want to set allowNew only if AllowNew didn't match value,
                //since AllowNew can depend on onAddingNew handler
                this.allowNew = value;
                if (oldAllowNewValue != value) {
                    FireListChanged(ListChangedType.Reset, -1);
                }
            }
        }

        /* private */
        bool IBindingList.AllowNew {
            get {
                return AllowNew;
            }
        }

        /// <devdoc>
        /// </devdoc>
        public bool AllowEdit {
            get {
                return this.allowEdit;
            }
            set {
                if (this.allowEdit != value) {
                    this.allowEdit = value;
                    FireListChanged(ListChangedType.Reset, -1);
                }
            }
        }

        /* private */
        bool IBindingList.AllowEdit {
            get {
                return AllowEdit;
            }
        }

        /// <devdoc>
        /// </devdoc>
        public bool AllowRemove {
            get {
                return this.allowRemove;
            }
            set {
                if (this.allowRemove != value) {
                    this.allowRemove = value;
                    FireListChanged(ListChangedType.Reset, -1);
                }
            }
        }

        /* private */
        bool IBindingList.AllowRemove {
            get {
                return AllowRemove;
            }
        }

        bool IBindingList.SupportsChangeNotification {
            get {
                return SupportsChangeNotificationCore;
            }
        }

        protected virtual bool SupportsChangeNotificationCore {
            get {
                return true;
            }
        }

        bool IBindingList.SupportsSearching {
            get {
                return SupportsSearchingCore;
            }
        }

        protected virtual bool SupportsSearchingCore {
            get {
                return false;
            }
        }

        bool IBindingList.SupportsSorting {
            get {
                return SupportsSortingCore;
            }
        }

        protected virtual bool SupportsSortingCore {
            get {
                return false;
            }
        }

        bool IBindingList.IsSorted {
            get {
                return IsSortedCore;
            }
        }

        protected virtual bool IsSortedCore {
            get {
                return false;
            }
        }

        PropertyDescriptor IBindingList.SortProperty {
            get {
                return SortPropertyCore;
            }
        }

        protected virtual PropertyDescriptor SortPropertyCore {
            get {
                return null;
            }
        }

        ListSortDirection IBindingList.SortDirection {
            get {
                return SortDirectionCore;
            }
        }

        protected virtual ListSortDirection SortDirectionCore {
            get {
                return ListSortDirection.Ascending;
            }
        }

        void IBindingList.ApplySort(PropertyDescriptor prop, ListSortDirection direction) {
            ApplySortCore(prop, direction);
        }

        protected virtual void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction) {
            throw new NotSupportedException();
        }

        void IBindingList.RemoveSort() {
            RemoveSortCore();
        }

        protected virtual void RemoveSortCore() {
            throw new NotSupportedException();
        }

        int IBindingList.Find(PropertyDescriptor prop, object key) {
            return FindCore(prop, key);
        }

        protected virtual int FindCore(PropertyDescriptor prop, object key) {
            throw new NotSupportedException();
        }

        void IBindingList.AddIndex(PropertyDescriptor prop) {
            // Not supported
        }

        void IBindingList.RemoveIndex(PropertyDescriptor prop) {
            // Not supported
        }

        #endregion

        #region Property Change Support

        private void HookPropertyChanged(T item) {
            INotifyPropertyChanged inpc = (item as INotifyPropertyChanged);

            // Note: inpc may be null if item is null, so always check.
            if (null != inpc) {
                if (propertyChangedEventHandler == null) {
                    propertyChangedEventHandler = new PropertyChangedEventHandler(Child_PropertyChanged);
                }
                inpc.PropertyChanged += propertyChangedEventHandler;
            }
        }

        private void UnhookPropertyChanged(T item) {
            INotifyPropertyChanged inpc = (item as INotifyPropertyChanged);

            // Note: inpc may be null if item is null, so always check.
            if (null != inpc && null != propertyChangedEventHandler) {
                inpc.PropertyChanged -= propertyChangedEventHandler;
            }
        }

        void Child_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.RaiseListChangedEvents) {
                if (sender == null || e == null || string.IsNullOrEmpty(e.PropertyName)) {
                    // Fire reset event (per INotifyPropertyChanged spec)
                    ResetBindings();
                } else {
                    // The change event is broken should someone pass an item to us that is not
                    // of type T.  Still, if they do so, detect it and ignore.  It is an incorrect
                    // and rare enough occurrence that we do not want to slow the mainline path
                    // with "is" checks.
                    T item;

                    try {
                        item = (T)sender;
                    } catch (InvalidCastException) {
                        ResetBindings();
                        return;
                    }

                    // Find the position of the item.  This should never be -1.  If it is,
                    // somehow the item has been removed from our list without our knowledge.
                    int pos = lastChangeIndex;

                    if (pos < 0 || pos >= Count || !this[pos].Equals(item)) {
                        pos = this.IndexOf(item);
                        lastChangeIndex = pos;
                    }

                    if (pos == -1) {
                        Debug.Fail("Item is no longer in our list but we are still getting change notifications.");
                        UnhookPropertyChanged(item);
                        ResetBindings();
                    } else {
                        // Get the property descriptor
                        if (null == this.itemTypeProperties) {
                            // Get Shape
                            itemTypeProperties = TypeDescriptor.GetProperties(typeof(T));
                            Debug.Assert(itemTypeProperties != null);
                        }

                        PropertyDescriptor pd = itemTypeProperties.Find(e.PropertyName, true);

                        // Create event args.  If there was no matching property descriptor,
                        // we raise the list changed anyway.
                        ListChangedEventArgs args = new ListChangedEventArgs(ListChangedType.ItemChanged, pos, pd);

                        // Fire the ItemChanged event
                        OnListChanged(args);
                    }
                }
            }
        }

        #endregion

        #region IRaiseItemChangedEvents interface

        /// <devdoc>
        ///     Returns false to indicate that BindingList<T> does NOT raise ListChanged events
        ///     of type ItemChanged as a result of property changes on individual list items
        ///     unless those items support INotifyPropertyChanged
        /// </devdoc>
        bool IRaiseItemChangedEvents.RaisesItemChangedEvents {
            get {
                return this.raiseItemChangedEvents;
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly BindingList<T> _deletedList = new();

        public object BoundGrid { get; set; }

        public DataListCollection DLC { get; set; }


        public event EventHandler Disposed { add { } remove { } }

        private ISite _currItem;

        public ISite Site {
            get {
                return _currItem;
            }
            set {
                _currItem = value;
            }
        }

        public void AddRange(IEnumerable<T> collection) {
            foreach (var item in collection) {
                if (item is not null) item.IsAdded = true;
                this.Add(item);
            }
        }

        public int RemoveAll(Func<T, bool> match) {
            int removedRows = 0;
            foreach (var item in this.Where(match).ToArray()) {
                _deletedList.Add(item);
                this.Remove(item);
                removedRows++;
            }
            return removedRows;
        }

        public IEnumerable<T> GetChanges() {
            return this.Where(x => x.IsChanged || x.IsAdded);
        }
        public IEnumerable<T> GetDeleted() {
            return _deletedList;
        }

        public void AcceptChanges() {
            foreach (var item in this) {
                item.AcceptChanges();
            }
            _deletedList.Clear();
        }
        public bool HasChanges() {
            return this.Any(x => x.IsChanged || x.IsAdded) || _deletedList.Any();
        }
        public void RejectChanges() {
            this.Where(x => x.IsAdded).DeleteDataListItems(this);
            foreach (var item in this) {
                item.RejectChanges();
            }
            _deletedList.Clear();
        }

        public void AcceptChangesEfficient() {
            foreach (var item in this.Where(x => x.IsChanged || x.IsAdded)) {
                item.AcceptChanges();
            }
            _deletedList.Clear();
        }

        public void RejectChangesEfficient() {
            this.Where(x => x.IsAdded).DeleteDataListItems(this);
            foreach (var item in this.Where(x => x.IsChanged)) {
                item.RejectChanges();
            }
            _deletedList.Clear();
        }

        public T NewRow() {
            return new T();
        }
        public List<string> ColumnNames {
            get {
                List<string> columnNames = new List<string>();
                foreach (PropertyInfo item in typeof(T).GetProperties()) {
                    columnNames.Add(item.Name);
                }
                return columnNames;
            }
        }

        public void Dispose() { }
    }

    [Serializable]
    public class DataItem : IRevertibleChangeTracking, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        [XmlIgnore] [DoNotNotify] Dictionary<string, dynamic> _Values = new();
        [XmlIgnore] [DoNotNotify] public Dictionary<string, dynamic> Values => _Values;
        [XmlIgnore] [DoNotNotify] public bool IsChanged { get; set; } = false;
        [XmlIgnore] [DoNotNotify] public bool IsAdded { get; set; } = false;
        [XmlIgnore] [DoNotNotify] public bool Delete { get; set; } = false;

        public void AcceptChanges() {
            _Values.Clear();
            IsChanged = false;
            IsAdded = false;
        }

        public void RejectChanges() {
            if (IsChanged) {
                foreach (var property in _Values) {
                    GetType().GetRuntimeProperty(property.Key).SetValue(this, property.Value);
                }
            }
            AcceptChanges();
        }

        protected void OnPropertyChanged(string propertyName, object before, object after) {
            if (before != after) {
                if (!_Values.ContainsKey(propertyName)) {
                    _Values[propertyName] = before;
                }
                IsChanged = true;
            }
        }

        public object GetService(Type serviceType) {
            //This does not use any service object.
            return null;
        }

        [XmlIgnore] [DoNotNotify] public IEnumerable<string> Columns => this.GetPropertieNames();
        [XmlIgnore] [DoNotNotify] public dynamic this[string name] { get { return this.GetPropertyValue(name); } set { this.SetPropertyValue(name, (object)value); } }

    }

}
