using Aron_For_TwMs_113_4.Models;
using ProcessTools.CtTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aron_For_TwMs_113_4.Components
{
    public interface IComponentEvents
    {
        string ExePath { get; set; }
        Data StartResualt { get; set; }
        List<HotKeyRegister> HotKeys { get; set; }
        EventHandler OnStatusChange { get; set; }

        CtDataCollection GetCt();
        GameInfo GetGameInfo();
        void SendNotify(string text);
        void SetStatusText(string text);
    }
}
