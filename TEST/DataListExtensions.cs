using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace TEST {
    public static class DataListExtensions {
        public static DataList<T> ToDataList<T>(this IEnumerable<T> rows) where T : DataItem, new() {
            var dl = new DataList<T>();
            dl.AddRange(rows);
            return dl;
        }
        public static BindingList<T> ToBindingList<T>(this IList<T> rows) where T : DataItem, new() {
            var dl = new BindingList<T>(rows);
            return dl;
        }

        public static void DeleteDataListItems<T>(this IEnumerable<T> rowsToDelete, DataList<T> drs) where T : DataItem, new() {
            foreach (var r in rowsToDelete) {
                r.AcceptChanges();
                r.Delete = true;
                r.AcceptChanges();
            }
            drs.RemoveAll(x => x.Delete);
        }

        public static void Delete<T>(this T r, DataList<T> drs) where T : DataItem, new() {
            drs.Remove(r);
        }

        public static DataList<T> AcceptChangesEfficient<T>(this DataList<T> dt) where T : DataItem, new() {
            var drs = dt.AsEnumerable().Where(x => x.IsChanged).ToList();
            for (int i = drs.Count - 1; i >= 0; i--) {
                drs[i].AcceptChanges();
            }
            return dt;
        }

    }
}