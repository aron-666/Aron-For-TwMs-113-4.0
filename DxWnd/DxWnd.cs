using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DxWnd
{
    public static class DxWnd
    {
        [DllImport(@"lib\dxwnd.dll", EntryPoint = "StartHook", CallingConvention = CallingConvention.Cdecl)]
        public extern static int StartHook();

        [DllImport(@"lib\dxwnd.dll", EntryPoint = "EndHook", CallingConvention = CallingConvention.Cdecl)]
        public extern static int EndHook();

        [DllImport(@"lib\dxwnd.dll", EntryPoint = "SetTarget", CallingConvention = CallingConvention.Cdecl)]
        public extern static int SetTarget(TARGETMAP[] args);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TARGETMAP
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string path;
            public int dxversion;
            public int flags;
            public int initx;
            public int inity;
            public int minx;
            public int miny;
            public int maxx;
            public int maxy;
        }


    }
}
