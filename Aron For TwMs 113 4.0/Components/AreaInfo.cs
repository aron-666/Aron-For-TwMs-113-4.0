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
using ProcessTools;

namespace Aron_For_TwMs_113_4.Components
{
    public partial class AreaInfo : UserControl
    {
        private readonly IComponentEvents _componentEvents;
        public AreaInfo(IComponentEvents componentEvents)
        {
            InitializeComponent();
            _componentEvents = componentEvents;
        }

        private void AreaInfo_Load(object sender, EventArgs e)
        {
            foreach (var i in groupBox3.Controls)
            {
                if (i is Label l)
                {
                    l.Tag = l.Text;
                }
            }
            t_Info.Enabled = true;

            Thread t = new Thread(() =>
            {
                var ct = _componentEvents.GetCt();

                while (true)
                {
                    ct["hp_lock"].ScriptObject.WriteOfData = 20;
                    ct["hp_lock"].ScriptObject.IsRead = false;
                    ct["hp_lock"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["hp_lock"].Address, out _);

                    ct["mp_lock"].ScriptObject.WriteOfData = 20;
                    ct["mp_lock"].ScriptObject.IsRead = false;
                    ct["mp_lock"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["mp_lock"].Address, out _);
                    Thread.Sleep(500);
                }
                
            });
            t.IsBackground = true;
            t.Start();

            Thread t2 = new Thread(() =>
            {
                var ct = _componentEvents.GetCt();
                var info = _componentEvents.GetGameInfo();

                while (true)
                {
                    lock (ct["人物名"].ScriptObject)
                    {
                        ct["人物名"].ScriptObject.IsRead = true;
                        ct["人物名"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["人物名"].Address, out var name);
                        info.Name = name.ToString();
                        if (CkReName.Checked)
                        {
                            if(name.ToString() != TeReName.Text)
                            {
                                ct["人物名"].ScriptObject.IsRead = false;
                                ct["人物名"].ScriptObject.WriteOfData = TeReName.Text + '\0';

                                ct["人物名"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["人物名"].Address, out _);
                            }
                        }
                    }
                    

                    ct["system_time"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["system_time"].Address, out var time);
                    info.SystemTime = (int)time;

                    ct["hp"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["hp"].Address, out var hp);
                    info.HP = (int)hp;

                    ct["mp"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["mp"].Address, out var mp);
                    info.MP = (int)mp;

                    ct["紅點數量"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["紅點數量"].Address, out var redDots);
                    info.RedDots = (int)redDots;

                    ct["怪物數量"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["怪物數量"].Address, out var mobs);
                    info.Mobs = (int)mobs;

                    lock (ct["攻擊不停"].ScriptObject)
                    {
                        ct["攻擊不停"].ScriptObject.IsRead = true;
                        ct["攻擊不停"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["攻擊不停"].Address, out var att);
                        info.AttCount = (int)att;
                    }


                    lock (ct["停止呼吸"].ScriptObject)
                    {
                        ct["停止呼吸"].ScriptObject.IsRead = true;
                        ct["停止呼吸"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["停止呼吸"].Address, out var br);
                        info.Breathe = (int)br;
                    }
                    

                    ct["X"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["X"].Address, out var x);
                    info.playerX = (int)x;

                    ct["Y"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["Y"].Address, out var y);
                    info.playerY = (int)y;

                    ct["滑鼠X"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["滑鼠X"].Address, out var mx);
                    info.MouseX = (int)mx;

                    ct["滑鼠Y"].ScriptObject.DoScript(MapleProcess.MemoryControl, ct["滑鼠Y"].Address, out var my);
                    info.MouseY = (int)my;
                    Thread.Sleep(10);
                }

            });
            t2.IsBackground = true;
            t2.Start();

            toolTip1.SetToolTip(CkReName, "CS 啟用後換圖生效，用於拍攝不讓別人得知遊戲真實名子。");
        }

        private void t_Info_Tick(object sender, EventArgs e)
        {
            var ct = _componentEvents.GetCt();
            var info = _componentEvents.GetGameInfo();
            
            LName.Text = $"{LName.Tag} {info.Name}";

            LST.Text = $"{LST.Tag} {info.SystemTime}";

            LHP.Text = $"{LHP.Tag} {info.HP}";

            LMP.Text = $"{LMP.Tag} {info.MP}";

            LRed.Text = $"{LRed.Tag} {info.RedDots}";

            LMOB.Text = $"{LMOB.Tag} {info.Mobs}";

            LAC.Text = $"{LAC.Tag} {info.AttCount}";

            LBT.Text = $"{LBT.Tag} {info.Breathe}";

            LPX.Text = $"{LPX.Tag} {info.playerX}";

            LPY.Text = $"{LPY.Tag} {info.playerY}";

            LMX.Text = $"{LMX.Tag} {info.MouseX}";

            LMY.Text = $"{LMY.Tag} {info.MouseY}";
        }

        private void CkReName_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
