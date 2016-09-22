using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Ship
{
    public class ShipInterface : MonoBehaviour
    {

        private ShipController shipController;
        private ShipManager shipManager;
        private GameObject ship;

        [SerializeField]
        private Text textEnergy;
        [SerializeField]
        private Text textOxygen;
        [SerializeField]
        private Toggle toggleDamper;

        public void SetShip(GameObject goShip) {

            ship = goShip;

            shipController = ship.GetComponent<ShipController>();
            shipManager = ship.GetComponent<ShipManager>();
        }

        void Update() {
            textEnergy.text = shipManager.Energy.ToString();
            textOxygen.text = shipManager.Oxygen.ToString();

            toggleDamper.isOn = shipController.isDamperOn;

        }


    }
}
