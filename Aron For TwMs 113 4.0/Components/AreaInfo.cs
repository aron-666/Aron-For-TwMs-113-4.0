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

        }

        private void t_Info_Tick(object sender, EventArgs e)
        {

        }
    }
}
