using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Script {
    public interface IHasChanged : IEventSystemHandler {

        void AddSlotContainerToList(GameObject slotContainer);

    }
}
