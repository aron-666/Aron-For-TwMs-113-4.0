using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ProcessTools.CtTools
{
    public static class CtFactory
    {
        public static CtDataCollection Read(string fileNme)
        {
            string buffer = "";
            using (StreamReader sr = new StreamReader(fileNme))
            {
                buffer = sr.ReadToEnd();
            }
            return Load(buffer);
        }

        public static CtDataCollection Load(string ctString)
        {
            var ret = new CtDataCollection(108);
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(ctString);
                var nodes = doc.SelectNodes("/CheatTable/CheatEntries/CheatEntry");

                List<XmlNode> list = new List<XmlNode>();

                list.AddRange(Prase(nodes, ret));

                while(list.Count != 0)
                {
                    var i = list.First();
                    list.RemoveAt(0);

                    nodes = i.SelectNodes("CheatEntries/CheatEntry");
                    list.AddRange(Prase(nodes, ret, i.SelectSingleNode("Description").InnerText.ExName()));
                }

            }
            catch(Exception ex)
            {
                Debug.XmlProcess.Write(ex.Message);
                ret = null;
            }
            return ret;
        }

        private static List<XmlNode> Prase(IEnumerable list, CtDataCollection ret, string groupName = "")
        {
            var d = new List<XmlNode>();

            foreach (XmlNode i in list)
            {
                XmlNode x = null;
                if ((x = i.SelectSingleNode("AssemblerScript")) != null && i.SelectSingleNode("VariableType").InnerText == "Auto Assembler Script")
                {
                    ret[i.SelectSingleNode("Description").InnerText.ExName()] = new CtData()
                    {
                        ID = Convert.ToInt32(i.SelectSingleNode("ID").InnerText),
                        AsmScript = i.SelectSingleNode("AssemblerScript").InnerText,
                        Name = i.SelectSingleNode("Description").InnerText.ExName(),
                        GroupName = groupName,
                        SType = i.SelectSingleNode("VariableType").InnerText
                    };
                }
                else if ((x = i.SelectSingleNode("Address")) != null && i.SelectSingleNode("VariableType").InnerText != "Auto Assembler Script")
                {
                    var temp = ret[i.SelectSingleNode("Description").InnerText.ExName()] = new CtData()
                    {
                        ID = Convert.ToInt32(i.SelectSingleNode("ID").InnerText),
                        Name = i.SelectSingleNode("Description").InnerText.ExName(),
                        GroupName = groupName,
                        Address = new IntPtr(Convert.ToInt64(i.SelectSingleNode("Address").InnerText, 16)),
                        Offsets = ReadPtr(i.SelectSingleNode("Offsets")),
                        SType = i.SelectSingleNode("VariableType").InnerText
                    };


                    switch (temp.SType)
                    {
                        case "Byte":
                            temp.ScriptObject = new Memory.ScriptObject(Memory.ScriptType.Byte,
                                offsets: temp.Offsets);
                            break;
                        case "2 Bytes":
                            temp.ScriptObject = new Memory.ScriptObject(Memory.ScriptType.Int16,
                                offsets: temp.Offsets);
                            break;
                        case "4 Bytes":
                            temp.ScriptObject = new Memory.ScriptObject(Memory.ScriptType.Int32,
                                offsets: temp.Offsets);
                            break;
                        case "8 Bytes":
                            temp.ScriptObject = new Memory.ScriptObject(Memory.ScriptType.Int64,
                                offsets: temp.Offsets);
                            break;
                        case "Float":
                            temp.ScriptObject = new Memory.ScriptObject(Memory.ScriptType.Float,
                                offsets: temp.Offsets);
                            break;
                        case "Double":
                            temp.ScriptObject = new Memory.ScriptObject(Memory.ScriptType.Double,
                                offsets: temp.Offsets);
                            break;
                        case "String":
                            temp.ScriptObject = new Memory.ScriptObject(Memory.ScriptType.String,
                                len: int.Parse(i.SelectSingleNode("Length").InnerText),
                                encoding: i.SelectSingleNode("Unicode").InnerText == "0" ? Encoding.ASCII : Encoding.Unicode,
                                offsets: temp.Offsets);
                            break;
                        case "Array of byte":
                            temp.ScriptObject = new Memory.ScriptObject(Memory.ScriptType.ByteArray,
                                len: int.Parse(i.SelectSingleNode("ByteLength").InnerText),
                                offsets: temp.Offsets);
                            break;


                    }
                }
                else if ((x = i.SelectSingleNode("GroupHeader")) != null)
                {
                    d.Add(i);
                }
            }
            return d;
        }

        private static string ExName(this string name)
        {
            return name.Remove(name.Length - 1, 1).Remove(0, 1);
        }
        private static IntPtr[] ReadPtr(XmlNode node)
        {
            var x = node.SelectNodes("Offset");
            var ret = new IntPtr[x.Count];
            for(int i = 0; i < x.Count; i++)
            {
                ret[i] = (new IntPtr(Convert.ToInt64(x[i].InnerText, 16)));
            }
            return ret;
        }
    }

    public class CtDataCollection : Dictionary<string, CtData>, IDisposable
    {
        protected CtDataCollection(Dictionary<string, CtData> v) : base(v)
        {
        }

        public CtDataCollection() : base()
        {
        }

        public CtDataCollection(int capacity) : base(capacity)
        {
        }

        public CtDataCollection GetGropCtDataCollection(string gropName) => new CtDataCollection(this.Where(x => x.Value.GroupName != null && x.Value.GroupName == gropName).ToDictionary(c => c.Key, c => c.Value));

        public List<string> GetGroupNames() => (this.Select(x => x.Value.GroupName).Where(c => c != null && c != "").Distinct()).ToList();

        #region IDisposable Support
        private bool disposedValue = false; // 偵測多餘的呼叫

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 處置 Managed 狀態 (Managed 物件)。
                    this.Clear();
                }

                // TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。

                disposedValue = true;
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放 Unmanaged 資源的程式碼時，才覆寫完成項。
        // ~CtDataCollection()
        // {
        //   // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 加入這個程式碼的目的在正確實作可處置的模式。
        void IDisposable.Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            // GC.SuppressFinalize(this);
        }
        #endregion

    }


    public class CtData
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string GroupName { get; set; }

        public string AsmScript { get; set; } = null;

        public bool Enable { get; set; }

        public IntPtr Address { get; set; }

        public IntPtr[] Offsets { get; set; }

        public Memory.ScriptObject ScriptObject { get; set; }

        public string SType { get; set; }

        public bool CeAutoAsm(bool enable)
        {
            Enable = enable;
            try
            {
                
                bool ret = ProcessTools.MapleProcess.CeAutoAsm(Name, AsmScript, enable);
                if (!ret) enable = ret;
                return ret;
            }
            catch
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is CtData data &&
                   ID == data.ID &&
                   Name == data.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = 1524444790;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GroupName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AsmScript);
            hashCode = hashCode * -1521134295 + Enable.GetHashCode();
            return hashCode;
        }
    }
}
