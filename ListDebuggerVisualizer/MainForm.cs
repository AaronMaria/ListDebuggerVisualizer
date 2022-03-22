//using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;
//using Telerik.WinControls;
//using Telerik.WinControls.UI;

namespace ListDebuggerVisualizer {
    public partial class MainForm : Form {
        /// <summary>
        /// if exceptions occures when Visualizer form is visible (and modal) Visual Studio chrashes when closing form, and user cannot close Visualizer form. It's annoying, very.
        /// </summary>
        private bool formLoaded = false;
        public IList Model { get; set; }
        public string ListType { get; set; }

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            if (this.Model == null) {
                return;
            }
            InitGrid();

            this.Text += " - List<" + ListType + ">";
            this.gridControl.DataSource = this.Model;
            //this.grid.AutoGenerateHierarchy = true;
            ReadSettings();
            //toolStripLabelTypeName.Text = "List item type: " + this.ListType;
            this.formLoaded = true;
        }

        private void InitGrid() {
            //this.grid.ReadOnly = true;
            //this.grid.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
            //this.grid.AllowSearchRow = true;
            //this.grid.EnableFiltering = true;
            //this.grid.MasterTemplate.ShowHeaderCellButtons = true;
            //this.grid.MasterTemplate.ShowFilteringRow = false;
        }

        private void ReadSettings() {
            var settings = GetSettingsList();
            if (settings == null)
                return;

            var mySetting = settings.FirstOrDefault(ltis => ltis.Name == this.ListType);
            if (mySetting != null) {
                this.Location = mySetting.Location;
                this.Size = mySetting.Size;

                if (File.Exists(mySetting.GridSettingsFile)) {
                    this.gridView.RestoreLayoutFromXml(mySetting.GridSettingsFile);
                }
                this.gridView.ClearColumnsFilter();
            }
        }

        private void SaveSettings() {
            ListTypeItemSettings mySetting = null;
            var settingsFile = new FileInfo(GetSettingsStorageFile());

            var settings = GetSettingsList();
            if (settings != null) {
                mySetting = settings.FirstOrDefault(ltis => ltis.Name == this.ListType);
            } else {
                settings = new List<ListTypeItemSettings>();
            }

            if (mySetting == null) {
                mySetting = new ListTypeItemSettings();
                mySetting.Name = this.ListType;
                mySetting.GridSettingsFile = GetSettingsPath() + "\\grid_settings_" + mySetting.Name + ".xml";
                settings.Add(mySetting);
            }
            mySetting.Location = this.Location;

            if (this.WindowState == FormWindowState.Normal) {
                mySetting.Size = this.Size;
            } else {
                mySetting.Size = this.RestoreBounds.Size;
            }
            string xml = SerializeToXml(settings);
            File.WriteAllText(settingsFile.FullName, xml);
            this.gridView.SaveLayoutToXml(mySetting.GridSettingsFile);
        }



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.formLoaded) {
                SaveSettings();
            }
        }

        public static string SerializeToXml(object obj) {
            var serializer = new XmlSerializer(obj.GetType());
            StringWriter sw = new StringWriter();
            serializer.Serialize(sw, obj);
            return sw.ToString();
        }

        public static T DeserializeFromXml<T>(string xmlFilePath) {
            var serializer = new XmlSerializer(typeof(T));
            T obj = default(T);
            using (TextReader textReader = new StreamReader(xmlFilePath)) {
                obj = (T)serializer.Deserialize(textReader);
            }
            return obj;
        }



        private List<ListTypeItemSettings> GetSettingsList() {
            string settingsFile = GetSettingsStorageFile();
            if (File.Exists(settingsFile)) {
                return DeserializeFromXml<List<ListTypeItemSettings>>(settingsFile);
            } else {
                return null;
            }
        }
        private string GetSettingsStorageFile() {
            return Path.Combine(GetSettingsPath(), "ListDebuggerVisualizerSettings.xml");
        }

        private string GetSettingsPath() {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\GridSettings\\" + Environment.UserName + "\\";
            Directory.CreateDirectory(path);
            return path;
        }

    }

    [Serializable]
    public class ListTypeItemSettings {
        public string Name { get; set; }
        public Point Location { get; set; }
        public Size Size { get; set; }
        public string GridSettingsFile { get; set; }
    }
}
