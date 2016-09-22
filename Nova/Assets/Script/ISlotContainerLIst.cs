using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

namespace Assets.Script.Interface {
    public interface ISlotContainerList : IEventSystemHandler{

        /// <summary>
        /// Updatet die virtuelle Liste
        /// </summary>
        void Refresh();

        /// <summary>
        /// Gibt die Anzahl des Items in der virtuellen Liste zurück
        /// </summary>
        /// <param name="itemName"></param>
        int Count(string itemName);

        /// <summary>
        /// Reduziert den Stack des Items um Count und gibt die Anzahl des Restes zurück
        /// </summary>
        /// <param name="itemname"></param>
        int Remove(string itemName, int count);

        /// <summary>
        /// Versucht einen SlotContainer in die echte Liste zu hängen
        /// </summary>
        /// <param name="slotContainer"></param>
        bool TryAdd(SlotContainer slotContainer);


    }
}
