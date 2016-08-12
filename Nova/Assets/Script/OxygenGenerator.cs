using Assets.Script.Interface;
using Assets.Script.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class OxygenGenerator : MonoBehaviour, IOpenUI{

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
                shipManager.RemoveMaxOxygen(1);
            }          
            else{
                isPing = true;
                shipManager.AddMaxOxygen(1);
            }
                
        }

        private void ProduceOxygen() {
            if (isPing && shipManager.OxygenFull()) {
                if (shipManager.RemoveEnergy(1)) {
                    if (shipManager.Decrease("Ice", unitType, 1)) {
                        shipManager.AddOxygen(1);
                    }
                }
            }
        }

        void OnMouseDown() {

            int count = shipManager.Count("Plutonium", unitType);
            Debug.Log("Plutonium " + count);

        }

        public void OpenUI() {
            interfaceManager.setChildOnUIContainer(this.transform);
        }
    }
}
