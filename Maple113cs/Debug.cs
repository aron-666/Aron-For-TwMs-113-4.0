using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTools
{
    public static class Debug
    {
        public static DebugSetting XmlProcess = new DebugSetting(WriteConsole)
        {
            Enable = true
        };

        public static void WriteConsole(string str) => Console.WriteLine(str);
    }

    public class DebugSetting
    {
        public DebugSetting(bool enable, params Dbg[] dbgs)
        {
            Enable = enable;
            foreach (var i in dbgs) DbgWrite += i;
        }

        public DebugSetting(params Dbg[] dbgs)
        {
            foreach (var i in dbgs) DbgWrite += i;
        }

        public DebugSetting()
        {
        }

        public bool Enable { get; set; }

        public delegate void Dbg(string ex);

        public Dbg DbgWrite = delegate(string ex) { System.Diagnostics.Debug.WriteLine(ex); };

        internal void Write(string str) { if (Enable) DbgWrite(str); }
    }
}
