using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public interface IUI {

        /// <summary>
        /// Versucht einen SlotContainer zu der UI hinzuzufügen
        /// </summary>
        /// <param name="slotContainer"></param>
        void Add(SlotContainer slotContainer);

        /// <summary>
        /// Gibt true zurück, wenn noch ein Slot frei ist
        /// </summary>
        /// <returns></returns>
        bool FreeSlot();

        /// <summary>
        /// Setzt die UI auf inaktiv
        /// </summary>
        void Hide();

        /// <summary>
        /// Gibt einen Boolean zurück, ob die UI aktiv bzw. inaktiv ist
        /// </summary>
        /// <returns></returns>
        bool IsActive();

        /// <summary>
        /// Bewegt die UI zu dem übergeben Transform
        /// </summary>
        /// <param name="transform"></param>
        void Move(Transform transform);

        /// <summary>
        /// Setzt die UI auf aktiv
        /// </summary>
        void Show();

        /// <summary>
        /// Setzt die UI auf ihre Ursprungsposition zurück
        /// </summary>
        void ResetPosition();


    }
}
