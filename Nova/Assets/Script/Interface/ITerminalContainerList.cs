using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

namespace Assets.Script.Interface {
    interface ITerminalContainerList : IEventSystemHandler {

        void OpenInTerminal(ScrollViewContainer scrollViewContainer);

    }
}
