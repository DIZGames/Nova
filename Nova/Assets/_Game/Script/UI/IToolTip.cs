using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

namespace Assets.Script.Interface {
    public interface IToolTip : IEventSystemHandler{

        void Message(string title, string text);
        void Close();

    }
}
