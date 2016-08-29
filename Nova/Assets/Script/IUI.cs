using Assets.Script.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public interface IUI {

        /// <summary>
        /// Gibt einen Boolean zurück, ob die UI aktiv bzw. inaktiv ist
        /// </summary>
        /// <returns></returns>
        bool IsActive {
            get;
        }

        /// <summary>
        /// Gibt den SlotContainer - falls vorhanden - zurück
        /// </summary>
        ISlotContainerList ISlotContainerList {
            get;
        }

        /// <summary>
        /// Setzt die UI auf inaktiv
        /// </summary>
        void Hide();

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
