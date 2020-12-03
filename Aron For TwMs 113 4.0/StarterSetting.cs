using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aron_For_TwMs_113_4
{
    public partial class StarterSetting : Form
    {
        public StarterSetting()
        {
            InitializeComponent();
        }

        private void StarterSetting_Load(object sender, EventArgs e)
        {
            var res = Properties.Settings.Default.UserStarter;
            var data = JsonConvert.DeserializeObject<Models.Data>(res);
            txName.Text = data.Name;
            txAddr.Text = data.Address;
            txPort.Text = data.Port.ToString();
        }

        void Num_Only(KeyPressEventArgs e)
        {
            int a = Convert.ToInt16(e.KeyChar);
            e.Handled = true;
            if (a >= 48 && a <= 57) e.Handled = false;
            if (a == 13) e.Handled = false;
            if (a == 127) e.Handled = false;
            if (a == 8) e.Handled = false;
        }

        private void txPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            Num_Only(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var s = Properties.Settings.Default;
            Models.Data data = new Models.Data();
            data.Name = txName.Text;
            data.Address = txAddr.Text;
            data.Port = int.Parse(txPort.Text);
            s.UserStarter = JsonConvert.SerializeObject(data);
            s.Save();
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
