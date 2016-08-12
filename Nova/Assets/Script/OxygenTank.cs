using Assets.Script.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class OxygenTank : MonoBehaviour, IOpenUI{
        private ShipManager shipManager;
        private InterfaceManager interfaceManager;
        private TerminalManager terminalManager;

        private bool isPing;

        // Use this for initialization
        void Start() {
            shipManager = transform.root.GetComponent<ShipManager>();

            terminalManager = transform.root.GetComponent<TerminalManager>();
            terminalManager.Add("OxygenTank", transform.parent.GetComponent<SpriteRenderer>().sprite, this.transform);

            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
        }

        public void Toggle() {
            if (isPing) {
                isPing = false;
                shipManager.RemoveMaxOxygen(40);
            }
            else {
                isPing = true;
                shipManager.AddMaxOxygen(40);
            }
        }

        public void OpenUI() {
            interfaceManager.setChildOnUIContainer(this.transform);
        }
    }
}
