using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TEST {
    public static class PropertyExtensions {
        public static TResult GetPropertyValueT<TResult>(this object t, string propertyName) {
            return (TResult)t.GetPropertyValue(propertyName);
        }
        public static dynamic GetPropertyValue(this object t, string propertyName) {
            return t.GetType().GetProperty(propertyName).GetValue(t, null);
        }
        public static void SetPropertyValue(this object obj, string name, object value) {
            var pi = obj.GetType().GetProperty(name);
            pi.SetValue(obj, value, null);
        }
        public static PropertyInfo GetProperty(this object obj, string name) {
            return obj.GetType().GetProperty(name);
        }

        public static IEnumerable<string> GetPropertieNames(this object t) => t.GetType().GetProperties().Select(p => p.Name);
    }
}
