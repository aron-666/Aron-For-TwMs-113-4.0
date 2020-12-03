using Aron_For_TwMs_113_4.Components;
using ProcessTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aron_For_TwMs_113_4
{
    public partial class Form1 : Form, IComponentEvents
    {
        private int curr_x;
        private int curr_y;
        private bool isWndMove;


        private List<UserControl> userControls = new List<UserControl>(5);
        private ProcessTools.CtTools.CtDataCollection ct;

        private readonly GameInfo GameInfo = new GameInfo();
        public Form1()
        {
            InitializeComponent();
            var s = File.ReadAllText("lib\\113new.CT");
            ct = ProcessTools.CtTools.CtFactory.Load(s);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            userControls.Add(new Components.AreaLock(this));
            userControls.Add(new Components.AreaKeys(this));
            userControls.Add(new Components.AreaInfo(this));
            userControls.Add(new Components.AreaControl(this));

            panelBody.Controls.Add(userControls[0], 1, 0);

            RegistHotKeys();
            btnStart.ToolTipText = "熱鍵：CTRL + ALT + J";
            btnStop.ToolTipText = "熱鍵：CTRL + ALT + K";

        }

        private void lbTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.curr_x = e.X;
                this.curr_y = e.Y;
                this.isWndMove = true;
            }
        }

        private void lbTitle_MouseUp(object sender, MouseEventArgs e)
        {
            this.isWndMove = false;
        }

        private void lbTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isWndMove)
                this.Location = new Point(this.Left + e.X - this.curr_x, this.Top + e.Y - this.curr_y);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnArea_Click(object sender, EventArgs e)
        {
            
            var con = sender as Control;

            int index = int.Parse(con.Tag.ToString());
            if (userControls[index].Equals(panelBody.Controls[1]))
                return;

            if (panelBody.Controls.Count > 1)
                panelBody.Controls.RemoveAt(1);
            panelBody.Controls.Add(userControls[index], 1, 0);
        }

        public EventHandler OnStatusChange { get; set; }

        public void SetStatusText(string text)
        {
            if(text != lbStatus.Text)
            {
                OnStatusChange?.Invoke(this, null);
                SendNotify(text);
                lbStatus.Text = text;
            }
        }

        public ProcessTools.CtTools.CtDataCollection GetCt() => ct;

        public GameInfo GetGameInfo() => GameInfo;

        public void SendNotify(string text)
        {
            if(Properties.Settings.Default.IsNotify)
                notifyIcon1.ShowBalloonTip(3000, Text, text, ToolTipIcon.Info);
        }

        public Models.Data StartResualt { get; set; }
        public string ExePath { get; set; }
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            SetTopLevel(true);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!File.Exists(ExePath))
            {
                MessageBox.Show($"找不到{ExePath}，請重新選擇程式", Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var res = StartResualt;
            ProcessTools.MapleProcess.Start(ExePath, StartResualt.Address, StartResualt.Port);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            MapleProcess.Kill();
        }

        public List<HotKeyRegister> HotKeys { get; set; }

        private void RegistHotKeys()
        {
            HotKeys = new List<HotKeyRegister>(10)
            {
                new HotKeyRegister(Handle, 1, (int)(KeyModifiers.Control | KeyModifiers.Alt), Keys.K)
                {
                    HotKeyPressed = (e, args) => MapleProcess.Kill()
                },
                new HotKeyRegister(Handle, 2, (int)(KeyModifiers.Control | KeyModifiers.Alt), Keys.J)
                {
                    HotKeyPressed = (e, args) => btnStart_Click(null, null)
                }
            };
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            HotKeys.ForEach(x => x.Dispose());
        }
    }
}
