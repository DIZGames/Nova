using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

namespace Assets.Script.Interface {
    public interface ISlotContainerRecipeList : IEventSystemHandler {

        void ButtonPress(SlotContainerRecipe slotContainerRecipe);

        /// <summary>
        /// Fügt die SlotContainerList zum Shipmanagement hinzu
        /// </summary>
        /// <param name=""></param>
        void AddToShipManager(ShipManager shipManager);
    }
}
