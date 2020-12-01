using Aron_For_TwMs_113_4.Components;
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
            var s = File.ReadAllText("113new.CT");
            ct = ProcessTools.CtTools.CtFactory.Load(s);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            userControls.Add(new Components.AreaLock(this));
            userControls.Add(new Components.AreaKeys(this));
            userControls.Add(new Components.AreaInfo(this));

            panelBody.Controls.Add(userControls[0], 1, 0);
            

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

        public void SetStatusText(string text)
        {
            lbStatus.Text = text;
        }

        public ProcessTools.CtTools.CtDataCollection GetCt() => ct;

        public GameInfo GetGameInfo() => GameInfo;
        
    }
}
