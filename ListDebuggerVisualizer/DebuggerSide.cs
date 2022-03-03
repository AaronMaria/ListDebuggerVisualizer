using ListDebuggeeSide;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

[assembly: DebuggerVisualizer(
    typeof(ListDebuggerVisualizer.ListDebuggerVisualizerClient), 
    typeof(ListDebuggeeSide.VisualizerJsonObjectSource), 
    Target = typeof(Collection<>), 
    Description = "List Debugger Visualizer"
)]
namespace ListDebuggerVisualizer {
    public class ListDebuggerVisualizerClient : DialogDebuggerVisualizer {
        override protected void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider) {
            try {
                ShowVisualizer(objectProvider);
            } catch (Exception ex) {
                MessageBox.Show("Exception getting object data: " + ex.Message);
            }
        }

        private static void ShowVisualizer(IVisualizerObjectProvider objectProvider) {
            //var objectProvider2 = (IVisualizerObjectProvider2)objectProvider;

            var jsonStream = objectProvider.GetData();
            var reader = new StreamReader(jsonStream);
            string json = reader.ReadToEnd();
            var container = Newtonsoft.Json.JsonConvert.DeserializeObject<VisualizerDataContainer>(json);

            if (container.TypeName == "string") {
                var prim = new List<PrimitiveListItem>();
                foreach (string strItem in container.Data.Cast<string>()) {
                    var pli = new PrimitiveListItem();
                    pli.Value = strItem;
                    prim.Add(pli);
                }
                ListDebuggerVisualizerClient.ShowVisualizerForm(prim, container.TypeName);
            } else if (container.IsPrimitive) {
                var prim = new List<PrimitiveListItem>();
                foreach (object objItem in container.Data.Cast<object>()) {
                    var pli = new PrimitiveListItem();
                    pli.Value = objItem?.ToString() ?? "";
                    prim.Add(pli);
                }
                ListDebuggerVisualizerClient.ShowVisualizerForm(prim, container.TypeName);
            } else {
                ListDebuggerVisualizerClient.ShowVisualizerForm(container.Data, container.TypeName);
            }
        }

        public static void TestShowVisualizer(object objectToVisualize) {
            var visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(ListDebuggerVisualizerClient), typeof(ListDebuggeeSide.VisualizerJsonObjectSource));
            visualizerHost.ShowVisualizer();
        }

        public static void ShowVisualizerForm(IList data, string typeName) {
            var mf = new MainForm();
            mf.Model = data;
            mf.ListType = typeName;
            mf.ShowDialog();
        }

    }

}
