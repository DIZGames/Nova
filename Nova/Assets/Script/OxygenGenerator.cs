using Assets.Script.Interface;
using Assets.Script.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class OxygenGenerator : MonoBehaviour, IInteractWithPlayerRaycast{

        public ShipManagerUnitType unitType;

        private ShipManager shipManager;
        private InterfaceManager interfaceManager;
        private TerminalManager terminalManager;

        private bool isPing;

        // Use this for initialization
        void Start() {
            terminalManager = transform.root.GetComponent<TerminalManager>();
            terminalManager.Add("OxygenGenerator", transform.parent.GetComponent<SpriteRenderer>().sprite, this.transform);

            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
            shipManager = transform.root.GetComponent<ShipManager>();
            shipManager.AddToPing(ProduceOxygen);

        }

        public void Toggle() {
            if (isPing) {
                isPing = false;
            }          
            else{
                isPing = true;
            }          
        }

        private void ProduceOxygen() {
            if (isPing) {
                if (shipManager.RemoveEnergy(1)) {
                    if (shipManager.Decrease("Ice", unitType, 1)) {
                        shipManager.AddOxygen(2);
                    }
                }
            }
        }

        public void RaycastAction() {
            //interfaceManager.setChildOnUIContainer(this.transform);
            interfaceManager.ShowUI(GetComponent<IUI>(), "Oxygen Generator");
        }
    }
}
