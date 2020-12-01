using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTools.Memory
{
    public class ScriptObject
    {
        public ScriptType Type { get; set; } = ScriptType.Ptr;
        public object Obj { get; protected set; } = new object();
        public Encoding StrEncoding { get; set; } = Encoding.Default;
        public IntPtr[] PtrOffsets { get; set; } = new IntPtr[0];
        public bool IsRead { get; set; } = true;
        public object WriteOfData { get; set; } = new object();

        /// <summary>
        /// byte長度，在String ByteArray內。
        /// </summary>
        public int Length { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">資料類型</param>
        /// <param name="len">byte長度，在String ByteArray內。</param>
        /// <param name="encoding">string read & write.if null Encoding.Default </param>
        public ScriptObject(ScriptType type = ScriptType.Ptr, bool isread = true, int len = 0, Encoding encoding = null, params IntPtr[] offsets)
        {
            Type = type;
            Length = len;
            if (encoding != null) StrEncoding = encoding;
            IsRead = isread;
            PtrOffsets = offsets;
            Obj = DefaultObj;
        }

        public ScriptObject()
        {
            Obj = DefaultObj;
        }

        public bool ReadObj(MemoryControl control, IntPtr ptr, out object data)
        {
            data = new object(); object def = new object();
            bool susses = false;
            switch (Type)
            {
                case ScriptType.Ptr:
                    data = control.ReadPtrs(ptr, out susses, PtrOffsets);
                    break;
                case ScriptType.Byte:
                    data = control.ReadByte(ptr, out susses);
                    break;
                case ScriptType.Int16:
                    data = control.ReadInt16(ptr, out susses);
                    break;
                case ScriptType.Int32:
                    data = control.ReadInt32(ptr, out susses);
                    break;
                case ScriptType.Int64:
                    data = control.ReadInt64(ptr, out susses);
                    break;
                case ScriptType.ByteArray:
                    data = control.ReadBytes(ptr, Length, out susses);
                    break;
                case ScriptType.String:
                    data = control.ReadString(ptr, Length, StrEncoding, out susses);
                    break;
                case ScriptType.Float:
                    data = control.ReadFloat(ptr, out susses);
                    break;
                case ScriptType.Double:
                    data = control.ReadDouble(ptr, out susses);
                    break;
                default:
                    susses = false;
                    Obj = null;
                    break;
            }
            data = Obj = susses ? data : DefaultObj;
            return susses;
        }

        public bool WriteObj(MemoryControl control, IntPtr ptr)
        {
            bool error = false;
            switch (Type)
            {
                case ScriptType.Ptr:
                    {
                        if (WriteOfData is IntPtr p)
                        {
                            if (control.IsX64)
                            {
                                if (!control.WriteInt64(ptr, p.ToInt64())) error = true;
                            }
                            else
                            {
                                if(!control.WriteInt64(ptr, p.ToInt32())) error = true;
                            }
                        }
                        else error = true;
                    }
                    break;
                case ScriptType.Byte:
                    {
                        if (!(WriteOfData is byte b) || !control.WriteByte(ptr, b)) error = true;
                    }
                    break;
                case ScriptType.Int16:
                    {
                        if (!(WriteOfData is Int16 b) || !control.WriteInt16(ptr, b)) error = true;
                    }
                    break;
                case ScriptType.Int32:
                    {
                        if (!(WriteOfData is Int32 b) || !control.WriteInt32(ptr, b)) error = true;
                    }
                    break;
                case ScriptType.Int64:
                    {
                        if (!(WriteOfData is Int64 b) || !control.WriteInt64(ptr, b)) error = true;
                    }
                    break;
                case ScriptType.ByteArray:
                    {
                        if (!(WriteOfData is byte[] b) || !control.WriteBytes(ptr, b)) error = true;
                    }
                    break;
                case ScriptType.String:
                    {
                        if (!(WriteOfData is string b) || !control.WriteString(ptr, b, StrEncoding)) error = true;
                    }
                    break;
                case ScriptType.Float:
                    {
                        if (!(WriteOfData is float b) || !control.WriteFloat(ptr, b)) error = true;
                    }
                    break;
                case ScriptType.Double:
                    {
                        if (!(WriteOfData is double b) || !control.WriteDouble(ptr, b)) error = true;
                    }
                    break;
                default:
                    error = true;
                    break;
            }
            return error;
        }
        public bool DoScript(MemoryControl control, IntPtr ptr, out object output)
        {
            output = DefaultObj;
            if (control == null) return false;
            try
            {
                var addr = control.ReadPtrs(ptr, out bool b, PtrOffsets);
                if (!b) return false;
                if (!IsRead)
                {
                    //write
                    if (!WriteObj(control, addr)) return false;
                }
                return ReadObj(control, addr, out output);
            }
            catch
            {
                return false;
            }
            
        }
        public static bool DoScripts(ScriptObject[] scripts, MemoryControl control, IntPtr ptr, out object output)
        {

            output = null;
            //check
            for (int i = 0; i < scripts.Length - 1; i++) if (!scripts[i].IsRead && scripts[i].Type != ScriptType.Ptr) return false;

            for (int i = 0; i < scripts.Length - 1; i++)
            {
                if (!scripts[i].ReadObj(control, ptr, out output))
                {
                    return false;
                }
                ptr = (IntPtr)output;
            }

            if (!scripts.Last().IsRead)
            {
                if(!scripts.Last().WriteObj(control, ptr)) return false;
            }
            else
            {
                if (!scripts.Last().ReadObj(control, ptr, out output)) return false;
            }
            return true;
        }
        
        public object DefaultObj
        {
            get
            {
                object def = new object();
                switch (Type)
                {
                    case ScriptType.Ptr:
                        def = IntPtr.Zero;
                        break;
                    case ScriptType.Byte:
                        def = (byte)0;
                        break;
                    case ScriptType.Int16:
                        def = (short)0;
                        break;
                    case ScriptType.Int32:
                        def = 0;
                        break;
                    case ScriptType.Int64:
                        def = (long)0;
                        break;
                    case ScriptType.ByteArray:
                        def = new byte[0];
                        break;
                    case ScriptType.String:
                        def = string.Empty;
                        break;
                    case ScriptType.Float:
                        def = 0f;
                        break;
                    case ScriptType.Double:
                        def = 0.0;
                        break;
                    default:
                        def = null;
                        break;
                }
                return def;
            }
        }
    }

    public enum ScriptType : int
    {
        Ptr = 0,
        Byte = 1,
        Int16 = 2,
        Int32 = 3,
        Int64 = 4,
        ByteArray = 5,
        String = 6,
        Float = 7,
        Double = 8
    }


}
