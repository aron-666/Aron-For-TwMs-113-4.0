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
        CtDataCollection GetCt();
        GameInfo GetGameInfo();
        void SetStatusText(string text);
    }
}
