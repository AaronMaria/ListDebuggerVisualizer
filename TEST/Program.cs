using ListDebuggerVisualizer;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TEST {
    internal class TestConsole {
        static void Main(string[] args) {
            var list = new WTFLIST {
                new TEST {name = "TEST1", num=1},
                new TEST { name = "TEST2", num=2}
            };
            var akldata = new AKLData();
            akldata.HOST_REC.AddRange(
                new List<AKLData.HOST_RECItem>{
                    new AKLData.HOST_RECItem { MESSAGE = "TEST" } ,
                    new AKLData.HOST_RECItem { MESSAGE = "TEST" } ,
                    new AKLData.HOST_RECItem { MESSAGE = "TEST" } ,
                    new AKLData.HOST_RECItem { MESSAGE = "TEST" }
                }
            );


            ListDebuggerVisualizerClient.TestShowVisualizer(akldata.HOST_REC);
            //ListDebuggerVisualizerClient.TestShowVisualizer(new List<string> { "TEST1", "TEST2" });
        }

        public class WTFLIST : IList<TEST> {
            public List<TEST> list = new();

            public TEST this[int index] { get => ((IList<TEST>)list)[index]; set => ((IList<TEST>)list)[index] = value; }

            public int Count => ((ICollection<TEST>)list).Count;

            public bool IsReadOnly => ((ICollection<TEST>)list).IsReadOnly;

            public void Add(TEST item) {
                ((ICollection<TEST>)list).Add(item);
            }

            public void Clear() {
                ((ICollection<TEST>)list).Clear();
            }

            public bool Contains(TEST item) {
                return ((ICollection<TEST>)list).Contains(item);
            }

            public void CopyTo(TEST[] array, int arrayIndex) {
                ((ICollection<TEST>)list).CopyTo(array, arrayIndex);
            }

            public IEnumerator<TEST> GetEnumerator() {
                return ((IEnumerable<TEST>)list).GetEnumerator();
            }

            public int IndexOf(TEST item) {
                return ((IList<TEST>)list).IndexOf(item);
            }

            public void Insert(int index, TEST item) {
                ((IList<TEST>)list).Insert(index, item);
            }

            public bool Remove(TEST item) {
                return ((ICollection<TEST>)list).Remove(item);
            }

            public void RemoveAt(int index) {
                ((IList<TEST>)list).RemoveAt(index);
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return ((IEnumerable)list).GetEnumerator();
            }
        }

        [Serializable]
        public class TEST {
            public string name { get; set; }
            public int num { get; set; }
        }
    }
}
