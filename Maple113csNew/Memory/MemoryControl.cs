using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTools.Memory
{
    
    public class MemoryControl : IProcess 
    {
        public bool ReadPtr(IntPtr ptr, int size, out byte[] buffer)
        {
            buffer = null;
            if (!IsOpen) return false;
            buffer = new byte[size];
            if (API.ReadProcessMemory(
                Handle,
                ptr,
                buffer,
                size,
                out var obr
                ))
                return true;
            return false;
        }

        public IntPtr ReadPtrs(IntPtr ptr, out bool susses, params IntPtr[] offsets)
        {
            IntPtr tempptr = ptr;
            bool temp = false;
            foreach (var i in offsets)
            {
                if (IsX64)
                {
                    tempptr = ReadInt64(tempptr, out temp).ToIntPtr();
                    tempptr = (tempptr.ToInt64() + i.ToInt64()).ToIntPtr();
                }
                else
                {
                    tempptr = ReadInt32(tempptr, out temp).ToIntPtr();
                    tempptr = (tempptr.ToInt32() + i.ToInt32()).ToIntPtr();
                }
                if (!temp)
                {
                    susses = false;
                    return IntPtr.Zero;
                }
            }
            susses = true;
            return tempptr;
        }

        public byte ReadByte(IntPtr ptr, out bool susses)
        {
            susses = false;
            try
            {
                if(susses = ReadPtr(ptr, 1, out var buffer))
                {
                    return buffer[0];
                }
            }
            catch
            {
            }
            return 0;

        }
        public Int16 ReadInt16(IntPtr ptr, out bool susses)
        {

            susses = false;
            try
            {
                if (susses = ReadPtr(ptr, 2, out var buffer))
                {
                    return BitConverter.ToInt16(buffer, 0);
                }
            }
            catch
            {
            }
            return 0;
        }

        public Int32 ReadInt32(IntPtr ptr, out bool susses)
        {
            susses = false;
            try
            {
                if (susses = ReadPtr(ptr, 4, out var buffer))
                {
                    return BitConverter.ToInt32(buffer, 0);
                }
            }
            catch
            {
            }
            return 0;
        }

        public Int64 ReadInt64(IntPtr ptr, out bool susses)
        {
            susses = false;
            try
            {
                if (susses = ReadPtr(ptr, 8, out var buffer))
                {
                    return BitConverter.ToInt64(buffer, 0);
                }
            }
            catch
            {
            }
            return 0;
        }

        public byte[] ReadBytes(IntPtr ptr, int length, out bool susses)
        {
            susses = false;
            try
            {
                if (susses = ReadPtr(ptr, length, out var buffer))
                {
                    return buffer;
                }
            }
            catch
            {
            }
            return new byte[0];
        }

        public string ReadString(IntPtr ptr, int length, Encoding encoding, out bool susses)
        {
            susses = false;
            try
            {
                if (susses = ReadPtr(ptr, length, out var buffer))
                {
                    return encoding.GetString(buffer);
                }
            }
            catch
            {
            }
            return "";
        }

        public Single ReadFloat(IntPtr ptr, out bool susses)
        {
            susses = false;
            try
            {
                if (susses = ReadPtr(ptr, sizeof(float), out var buffer))
                {
                    return BitConverter.ToSingle(buffer, 0);
                }
            }
            catch
            {
            }
            return 0;
        }

        public Double ReadDouble(IntPtr ptr, out bool susses)
        {
            susses = false;
            try
            {
                if (susses = ReadPtr(ptr, sizeof(double), out var buffer))
                {
                    return BitConverter.ToDouble(buffer, 0);
                }
            }
            catch
            {
            }
            return 0;
        }
        public bool WritePtr(IntPtr ptr, params byte[] buffer)
        {
            if (!IsOpen) return false;

            if (API.WriteProcessMemory(
                Handle,
                ptr,
                buffer,
                buffer.Length,
                out var obr
                ))
                return true;
            return false;
        }


        public bool WriteByte(IntPtr ptr, byte input)
        {
            try
            {
                if (WritePtr(ptr, input))
                {
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }

        public bool WriteInt16(IntPtr ptr, Int16 input)
        {
            try
            {
                if (WritePtr(ptr, BitConverter.GetBytes(input)))
                {
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }

        public bool WriteInt32(IntPtr ptr, Int32 input)
        {
            try
            {
                if (WritePtr(ptr, BitConverter.GetBytes(input)))
                {
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }

        public bool WriteInt64(IntPtr ptr, Int64 input)
        {
            try
            {
                if (WritePtr(ptr, BitConverter.GetBytes(input)))
                {
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }

        public bool WriteBytes(IntPtr ptr, params byte[] input)
        {
            try
            {
                if (WritePtr(ptr, input))
                {
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }

        public bool WriteString(IntPtr ptr, string input, Encoding encoding)
        {
            try
            {
                if (WritePtr(ptr, encoding.GetBytes(input)))
                {
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }

        public bool WriteFloat(IntPtr ptr, Single input)
        {
            try
            {
                if (WritePtr(ptr, BitConverter.GetBytes(input)))
                {
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }

        public bool WriteDouble(IntPtr ptr, Double input)
        {
            try
            {
                if (WritePtr(ptr, BitConverter.GetBytes(input)))
                {
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }

    }
}
