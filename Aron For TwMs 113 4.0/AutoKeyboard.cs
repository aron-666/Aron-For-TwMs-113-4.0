using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aron_For_TwMs_113_4
{
    public class AutoKeyboard
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKeyEx(uint uCode, uint uMapType, IntPtr dwhkl);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        private readonly static string[] keys;

        private readonly static uint[] values;
        
        public readonly static List<KeyValue> keyValues;

        public static uint WM_KEYDOWN = 0x100;
        public static uint WM_KEYUP = 0x101;

        static AutoKeyboard()
        {
            keys = new string[] { "None", "Enter", "Shift", "Ctrl", "Alt", "Space", "PageUp", "PageDown", "Insert", "Delete", "Home", "End", "Left", "Up", "Right", "Down", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "ESCAPE" };
            values = new uint[] { 0, 0xD, 0x10, 0x11, 0x12, 0x20, 0x21, 0x22, 0x2D, 0x2E, 0x24, 0x23, 0x25, 0x26, 0x27, 0x28, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F, 0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A, 0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7A, 0x7B, 0x1B };
            keyValues = new List<KeyValue>(keys.Length + 1);

            for(int i = 0; i < keys.Length; i++)
            {
                keyValues.Add(new KeyValue() { Key = keys[i], Value = values[i], Msg = (uint)i });
            }
        }



        public string Name { get; set; }

        protected readonly Thread thread;

        public virtual bool Enable { get; set; }

        public bool Exit { get; set; }

        public KeyValue SelectKey { get; set; }

        public int Interval { get; set; }

        public virtual IntPtr Hwnd { get; set; }

        public AutoKeyboard(IntPtr hwnd, string name, int interval, KeyValue key)
        {
            Hwnd = hwnd;
            Name = name;
            Interval = interval;
            SelectKey = key;
            thread = new Thread(thread_Func);
            thread.Name = Name;
            thread.IsBackground = true;
            thread.Start();
        }

        protected virtual void thread_Func()
        {
            while (!Exit)
            {
                if (Enable)
                {
                    if(Hwnd != IntPtr.Zero && SelectKey.Value != 0u)
                    {
                        PostMessage(Hwnd, WM_KEYDOWN, SelectKey.Msg, (MapVirtualKey(SelectKey.Value, 0) << 16) + 1);
                        PostMessage(Hwnd, WM_KEYUP, SelectKey.Msg, (MapVirtualKey(SelectKey.Value, 0) << 16) + 1);
                    }
                    
                }
                
                for(int i = 0; i < Interval;)
                {
                    int interval = 0;

                    if (Interval - i > 1000)
                        interval = 1000;
                    else if(Interval - i > 100)
                        interval = 100;
                    else if (Interval - i > 10)
                        interval = 10;
                    else 
                        interval = 1;
                    Thread.Sleep(interval);
                    i += interval;
                    if (Exit) 
                        break;
                }
            }
        }
    }

    public class KeyValue
    {
        public string Key { get; set; }

        public uint Value { get; set; }

        public uint Msg { get; set; }

        public override string ToString()
        {
            return Key;
        }
    }
}
