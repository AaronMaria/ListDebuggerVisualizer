using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections;
using System.IO;

namespace ListDebuggeeSide {
    public class VisualizerJsonObjectSource : VisualizerObjectSource {
        public override void GetData(object target, Stream outgoingData) {
            var itemType = target.GetType().GetProperty("Item").PropertyType;
            var container = new VisualizerDataContainer();
            container.TypeName = itemType.Name;
            container.Data = (IList)target;
            container.IsPrimitive = itemType.IsPrimitive;

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(container);
            var writer = new StreamWriter(outgoingData);
            writer.WriteLine(json);
            writer.Flush();
        }
    }
    public class VisualizerDataContainer {
        public string TypeName { get; set; }
        public IList Data { get; set; }
        public bool IsPrimitive { get; set; }
    }
    public class PrimitiveListItem {
        public string Value { get; set; }
    }
}
