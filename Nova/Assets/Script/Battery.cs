using Assets.Script.Interface;
using Assets.Script.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class Battery : MonoBehaviour, IOpenUI {

        private ShipManager shipManager;
        private InterfaceManager interfaceManager;
        private TerminalManager terminalManager;

        private bool isPing;

        // Use this for initialization
        void Start() {
            shipManager = transform.root.GetComponent<ShipManager>();

            terminalManager = transform.root.GetComponent<TerminalManager>();
            terminalManager.Add("Battery", transform.parent.GetComponent<SpriteRenderer>().sprite, this.transform);

            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
        }

        public void Toggle() {
            if (isPing) {
                isPing = false;
                shipManager.RemoveMaxEnergy(40);
            }  
            else{
                isPing = true;
                shipManager.AddMaxEnergy(40);
            }
        }

        public void OpenUI() {
            interfaceManager.setChildOnUIContainer(this.transform);
        }
    }
}
