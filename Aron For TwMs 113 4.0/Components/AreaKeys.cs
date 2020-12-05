using ProcessTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aron_For_TwMs_113_4.Components
{
    public partial class AreaKeys : UserControl
    {
        private readonly IComponentEvents _componentEvents;

        private readonly List<AutoKeyboard> autoKeyboards = new List<AutoKeyboard>(10);
        public AreaKeys(IComponentEvents componentEvents)
        {
            InitializeComponent();
            _componentEvents = componentEvents;
            _componentEvents.OnStatusChange += (s, a) =>
            {
                var ptr2 = IntPtr.Zero;
                if (MapleProcess.IsOpen)
                {
                    MapleProcess.MsProc.Refresh();
                    ptr2 = MapleProcess.MsProc.MainWindowHandle;
                }

                foreach (var i in autoKeyboards)
                {
                    i.Hwnd = ptr2;
                }

            };
        }

        ~AreaKeys()
        {
            autoKeyboards.ForEach(x =>
            {
                x.Interval = 0;
                x.Exit = true;
            });
        }

        private void AreaKeys_Load(object sender, EventArgs e)
        {
            var ptr = IntPtr.Zero;
            if (MapleProcess.IsOpen)
            {
                MapleProcess.MsProc.Refresh();
                ptr = MapleProcess.MsProc.MainWindowHandle;
            }
            foreach (Control i in groupBox2.Controls)
            {
                if (i is CheckBox ck)
                {

                    var autoKeyboard = new MyAutoKeyboard(ptr, i.Name, 10, AutoKeyboard.keyValues[0]);
                    autoKeyboards.Add(autoKeyboard);
                    ck.Tag = autoKeyboard;
                }
                 
            }
            foreach (Control i in groupBox2.Controls)
            {
                if (i is ComboBox cb)
                {
                    cb.DataSource = new List<KeyValue>(AutoKeyboard.keyValues);
                    cb.SelectedIndex = 0;
                }
            }

            foreach (Control i in groupBox4.Controls)
            {
                if (i is CheckBox ck)
                {

                    var autoKeyboard = new AutoRecover(ptr, i.Name, 10, AutoKeyboard.keyValues[0]) { MinValue = 1000, GetValue = () => 
                    {
                        if (i.Name.Contains("HP"))
                        {
                            return _componentEvents.GetGameInfo().HP;
                        }
                        else
                            return _componentEvents.GetGameInfo().MP;
                    }
                    };
                    autoKeyboards.Add(autoKeyboard);
                    ck.Tag = autoKeyboard;
                }
            }

            foreach (Control i in groupBox4.Controls)
            {
                if (i is ComboBox cb)
                {
                    cb.DataSource = new List<KeyValue>(AutoKeyboard.keyValues);
                    cb.SelectedIndex = 0;
                }
            }


        }
        private void combo_AutoKeys_Changed(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            var ck = groupBox2.Controls.Find(cb.Name.Replace("Co", "Ck"), false).First();

            var autoKeybord = ck.Tag as AutoKeyboard;
            autoKeybord.SelectKey = cb.SelectedItem as KeyValue;
        }

        private void ckAutoKeyboard_Changed(object sender, EventArgs e)
        {
            var ck = sender as CheckBox;

            var autoKeybord = ck.Tag as AutoKeyboard;
            autoKeybord.Enable = ck.Checked;

        }

        private void num_AutoKeyboard_ValueChanged(object sender, EventArgs e)
        {
            var num = sender as NumericUpDown;
            var ck = groupBox2.Controls.Find(num.Name.Replace("num", "CkKey"), false).First();

            var autoKeybord = ck.Tag as AutoKeyboard;
            autoKeybord.Interval = decimal.ToInt32(num.Value);
        }

        private void num_HPMP_ValueChanged(object sender, EventArgs e)
        {
            var num = sender as NumericUpDown;
            var ck = groupBox4.Controls.Find(num.Name.Replace("num", "Ck"), false).First();

            var autoKeybord = ck.Tag as AutoRecover;
            autoKeybord.MinValue = decimal.ToInt32(num.Value);
        }

        private void combo_AutoRecoverKeys_Changed(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            var ck = groupBox4.Controls.Find(cb.Name.Replace("Co", "Ck"), false).First();

            var autoKeybord = ck.Tag as AutoKeyboard;
            autoKeybord.SelectKey = cb.SelectedItem as KeyValue;
        }
    }

    public class MyAutoKeyboard : AutoKeyboard
    {

        public override IntPtr Hwnd { get => MapleProcess.IsOpen ? MapleProcess.MsProc.MainWindowHandle : IntPtr.Zero; }
        public MyAutoKeyboard(IntPtr hwnd, string name, int interval, KeyValue key) : base(hwnd, name, interval, key)
        {
        }
    }

    public class AutoRecover : MyAutoKeyboard
    {
        public Func<int> GetValue { get; set; }
        public int MinValue { get; set; }
        public AutoRecover(IntPtr hwnd, string name, int interval, KeyValue key) : base(hwnd, name, interval, key)
        {
            
        }

        protected override void thread_Func()
        {
            while (!Exit)
            {
                if (Enable)
                {
                    if (Hwnd != IntPtr.Zero && SelectKey.Value != 0u)
                    {
                        int val = GetValue.Invoke();
                        if(val < MinValue)
                        {
                            PostMessage(Hwnd, WM_KEYDOWN, SelectKey.Msg, (MapVirtualKey(SelectKey.Value, 0) << 16) + 1);
                            PostMessage(Hwnd, WM_KEYUP, SelectKey.Msg, (MapVirtualKey(SelectKey.Value, 0) << 16) + 1);
                        }
                    }
                        

                }

                for (int i = 0; i < Interval;)
                {
                    int interval = 0;

                    if (Interval - i > 1000)
                        interval = 1000;
                    else if (Interval - i > 100)
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
}
