using Assets.Script.RecipeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Script.Interface {
    public interface ISlotContainerQueueList : IEventSystemHandler {

        void CraftDone(SlotContainerQueue slotContainerQueue);

        void DeleteQueue(Transform transform);

        void AddQueue(SlotContainerRecipe slotContainer);

        void Ping();

        /// <summary>
        /// Fügt die SlotContainerList zum Shipmanagement hinzu
        /// </summary>
        /// <param name=""></param>
        void AddToShipManager(ShipManager shipManager);
    }
}
