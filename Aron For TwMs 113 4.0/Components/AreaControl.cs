using ProcessTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aron_For_TwMs_113_4.Components
{
    public partial class AreaControl : UserControl
    {

        private readonly IComponentEvents _componentEvents;
        private bool OnOff = true;
        public AreaControl(IComponentEvents componentEvents)
        {
            InitializeComponent();
            _componentEvents = componentEvents;

            OnOff = false;
            foreach (Control i in groupBox6.Controls)
            {
                if (i.Tag != null && i is CheckBox ck)
                {
                    ck.CheckedChanged += onSimpleClick;
                }
            }

            foreach (Control i in groupBox5.Controls)
            {
                if (i.Tag != null && i is CheckBox ck)
                {
                    ck.CheckedChanged += onSimpleClick;
                }
            }
            OnOff = true;
        }

        private void AreaControl_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;


            _componentEvents.OnStatusChange += (s, a) => 
            {
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
            };
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
    }
}
