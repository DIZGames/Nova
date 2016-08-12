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
        void UpdateList();

        /// <summary>
        /// Gibt die Anzahl des Items in der virtuellen Liste zurück
        /// </summary>
        /// <param name="itemName"></param>
        int Count(string itemName);

        /// <summary>
        /// Reduziert den Stack des Items um Count und gibt die Anzahl des Restes zurück
        /// </summary>
        /// <param name="itemname"></param>
        int Decrease(string itemName, int count);

    }
}
