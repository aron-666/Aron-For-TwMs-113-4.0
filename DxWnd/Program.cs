using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DxWnd
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_HIDE);
            string path = args[0];
            int pid = int.Parse(args[1]);

            DxWnd.TARGETMAP[] t = new DxWnd.TARGETMAP[2]
            {
                new DxWnd.TARGETMAP(){ path = path, dxversion = 8, flags = 0, initx = 0, inity = 0, minx = 0, miny = 0, maxx = 1920, maxy = 1080 },
                new DxWnd.TARGETMAP()
            };
            DxWnd.EndHook();
            DxWnd.SetTarget(t);
            DxWnd.StartHook();
            Process p = Process.GetProcessById(pid);
            p.WaitForExit();
            DxWnd.EndHook();
        }
    }
}
