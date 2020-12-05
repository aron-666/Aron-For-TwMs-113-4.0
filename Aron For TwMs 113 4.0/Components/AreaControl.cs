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
    public partial class AreaControl : UserControl
    {

        private readonly IComponentEvents _componentEvents;
        private bool OnOff = true;

        private readonly List<PointAccesser> pointAccessers = new List<PointAccesser>(10);

        public AreaControl(IComponentEvents componentEvents)
        {
            InitializeComponent();
            _componentEvents = componentEvents;

            foreach (Control i in groupBox6.Controls)
            {
                if (i.Tag != null && i.Tag.ToString().Length >= 1 && i is CheckBox ck)
                {
                    ck.CheckedChanged += onSimpleClick;
                }
            }

            foreach (Control i in groupBox5.Controls)
            {
                if (i.Tag != null && i.Tag.ToString().Length >= 1 && i is CheckBox ck)
                {
                    ck.CheckedChanged += onSimpleClick;
                }
            }

            _componentEvents.HotKeys.Add(new HotKeyRegister(Handle, _componentEvents.HotKeys.Count + 1, (int)KeyModifiers.Control, Keys.D)
            {
                HotKeyPressed = (sender, e) => { Ck無敵.Checked = !Ck無敵.Checked; }
            });

            _componentEvents.HotKeys.Add(new HotKeyRegister(Handle, _componentEvents.HotKeys.Count + 1, (int)KeyModifiers.Control, Keys.F10)
            {
                HotKeyPressed = (sender, e) => { CkMouCS.Checked = !CkMouCS.Checked; }
            });

            _componentEvents.HotKeys.Add(new HotKeyRegister(Handle, _componentEvents.HotKeys.Count + 1, (int)KeyModifiers.Control, Keys.F11)
            {
                HotKeyPressed = (sender, e) => { Ck滑鼠SS.Checked = !Ck滑鼠SS.Checked; }
            });

            var ct = _componentEvents.GetCt();

            var s = "新無延遲";
            var a = ct[s];
            a.ScriptObject.IsRead = false;
            a.ScriptObject.WriteOfData = -1;
            pointAccessers.Add(new PointAccesser(a, s, WritePoint, 1));

            s = "停止呼吸";
            a = ct[s];
            a.ScriptObject.WriteOfData = 0;
            pointAccessers.Add(new PointAccesser(a, s, WritePoint, 1));

            s = "攻擊不停";
            a = ct[s];
            a.ScriptObject.WriteOfData = 0;
            pointAccessers.Add(new PointAccesser(a, s, WritePoint, 1));


            s = "紅點數量";
            a = ct[s];
            pointAccessers.Add(new PointAccesser(a, s, (sender, args) => 
            {
                PointAccesser accesser = sender as PointAccesser;
                lock (accesser.CtData.ScriptObject)
                {
                    accesser.CtData.ScriptObject.DoScript(MapleProcess.MemoryControl, accesser.CtData.Address, out var redDot);
                    if((int)redDot >  numRedDots.Value)
                    {
                        MapleProcess.Kill();
                        Thread.Sleep(10000);
                    }
                }
            }, 1));
        }
        ~AreaControl()
        {
            pointAccessers.ForEach(x => x.Exit = true);
        }

        private void WritePoint(object sender, EventArgs args)
        {
            PointAccesser accesser = sender as PointAccesser;
            lock (accesser.CtData.ScriptObject)
            {
                accesser.CtData.ScriptObject.IsRead = false;
                accesser.CtData.ScriptObject.DoScript(MapleProcess.MemoryControl, accesser.CtData.Address, out _);
            }
        }

        private void AreaControl_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;

            _componentEvents.OnStatusChange += (s, a) => 
            {
                OnOff = false;
                foreach (Control i in groupBox6.Controls)
                {
                    if (i is CheckBox ck && ck.Checked)
                    {
                        ck.Checked = false;
                    }
                }

                foreach (Control i in groupBox5.Controls)
                {
                    if (i is CheckBox ck && ck.Checked)
                    {
                        ck.Checked = false;
                    }
                }
                OnOff = true;
            };

            Co生怪.SelectedIndex = 0;
        }

        private void onSimpleClick(object sender, EventArgs args)
        {
            if (!OnOff)
                return;
            var ck = sender as CheckBox;
            var ct = _componentEvents.GetCt();
            if (MapleProcess.IsOpen)
            {

                var sc = ct[ck.Tag.ToString()];//.CeAutoAsm(ck.Checked);
                sc.CeAutoAsm(ck.Checked);
            }
        }

        private void ckWritePoint_CheckChange(object sender, EventArgs e)
        {
            if (!OnOff)
                return;
            var ck = sender as CheckBox;
            var acc = pointAccessers.Where(x => x.Name == ck.Text).First();
            acc.Enable = ck.Checked;
        }

        private void CkRed_CheckedChanged(object sender, EventArgs e)
        {
            if (!OnOff)
                return;
            var ck = sender as CheckBox;
            var acc = pointAccessers.Where(x => x.Name == "紅點數量").First();
            acc.Enable = ck.Checked;
        }

        private void Ck改能力_CheckedChanged(object sender, EventArgs e)
        {
            if (!OnOff)
                return;
            var ck = sender as CheckBox;
            var ct = _componentEvents.GetCt();
            if (MapleProcess.IsOpen)
            {

                var sc = ct["改能力"];//.CeAutoAsm(ck.Checked);
                var str = string.Format(sc.AsmScript, decimal.ToInt32(numStr.Value), decimal.ToInt32(numDex.Value), decimal.ToInt32(numInt.Value), decimal.ToInt32(numLuk.Value));

                MapleProcess.CeAutoAsm(sc.Name, str, ck.Enabled);
                //sc.CeAutoAsm(ck.Checked);
            }
        }

        private void ckHPMP_CheckedChanged(object sender, EventArgs e)
        {
            if (!OnOff)
                return;
            var ck = sender as CheckBox;
            var ct = _componentEvents.GetCt();
            if (MapleProcess.IsOpen)
            {

                var sc = ct["快速恢復HPMP"];//.CeAutoAsm(ck.Checked);
                var str = string.Format(sc.AsmScript, decimal.ToInt32(numHP.Value), decimal.ToInt32(numMP.Value));

                MapleProcess.CeAutoAsm(sc.Name, str, ck.Enabled);
                //sc.CeAutoAsm(ck.Checked);
            }
        }

        private void ap_ValueChange(object sender, EventArgs e)
        {
            if (!OnOff)
                return;
            var ck = Ck改能力;
            var ct = _componentEvents.GetCt();
            if (MapleProcess.IsOpen && ck.Checked)
            {

                var sc = ct["改能力"];//.CeAutoAsm(ck.Checked);
                var str = string.Format(sc.AsmScript, decimal.ToInt32(numStr.Value), decimal.ToInt32(numDex.Value), decimal.ToInt32(numInt.Value), decimal.ToInt32(numLuk.Value));
                MapleProcess.CeAutoAsm(sc.Name, str, false);
                MapleProcess.ResetScript(sc.Name, str);
                MapleProcess.CeAutoAsm(sc.Name, str, ck.Enabled);
                //sc.CeAutoAsm(ck.Checked);
            }
        }

        private void Ck定點_CheckedChanged(object sender, EventArgs e)
        {
            if (!OnOff)
                return;
            var ck = sender as CheckBox;
            var ct = _componentEvents.GetCt();
            if (MapleProcess.IsOpen)
            {
                string name = Co生怪.Text + "生怪";

                var sc = ct[name];//.CeAutoAsm(ck.Checked);
                //var str = string.Format(sc.AsmScript, decimal.ToInt32(numHP.Value), decimal.ToInt32(numMP.Value));

                //MapleProcess.ResetScript("定點生怪", sc.AsmScript);
                MapleProcess.CeAutoAsm("定點生怪", sc.AsmScript, ck.Checked);
                //sc.CeAutoAsm(ck.Checked);
            }
        }

        private void Co生怪_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!OnOff)
                return;
            var ck = Ck定點;
            var ct = _componentEvents.GetCt();
            if (MapleProcess.IsOpen && ck.Checked)
            {
                string name = Co生怪.Text + "生怪";

                var sc = ct[name];//.CeAutoAsm(ck.Checked);
                //var str = string.Format(sc.AsmScript, decimal.ToInt32(numHP.Value), decimal.ToInt32(numMP.Value));

                //MapleProcess.ResetScript("定點生怪", sc.AsmScript);
                MapleProcess.CeAutoAsm("定點生怪", "", false);
                MapleProcess.ResetScript("定點生怪", sc.AsmScript);
                MapleProcess.CeAutoAsm("定點生怪", sc.AsmScript, ck.Checked);
                //sc.CeAutoAsm(ck.Checked);
            }
        }
    }

    public class PointAccesser
    {
        public bool Exit { get; set; } = false;
        public bool Enable { get; set; } = false;
        public int Interval { get; set; } = 100;
        public string Name { get; set; }
        public ProcessTools.CtTools.CtData CtData { get; set; }
        private readonly Thread thread;

        public EventHandler OnLoop { get; set; }
        public PointAccesser(ProcessTools.CtTools.CtData ctData, string name, EventHandler onLoop, int interval = 100)
        {
            CtData = ctData;
            Interval = interval;
            Name = name;
            OnLoop += onLoop;
            thread = new Thread(Thead_func);
            thread.IsBackground = true;
            thread.Name = name;
            thread.Start(this);
        }

        public static void Thead_func(object obj)
        {
            PointAccesser accesser = obj as PointAccesser;

            while (!accesser.Exit)
            {
                if(accesser.Enable)accesser.OnLoop?.Invoke(accesser, new EventArgs());
                Thread.Sleep(accesser.Interval);
            }
            accesser.CtData = null;
        }
    }
}
