using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

namespace Assets.Script {
    public interface IExchangeSlotContainer : IEventSystemHandler {

        void Exchange(SlotContainer slotContainer);

    }
}
