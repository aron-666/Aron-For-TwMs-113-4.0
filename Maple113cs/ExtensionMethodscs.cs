using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTools
{
    public static class ExtensionMethodscs
    {
        public static IntPtr ToIntPtr(this long ptr) => new IntPtr(ptr);
        public static IntPtr ToIntPtr(this int ptr) => new IntPtr(ptr);
    }
}
