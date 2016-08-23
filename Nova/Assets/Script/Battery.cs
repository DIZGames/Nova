using Assets.Script.Interface;
using Assets.Script.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script {
    public class Battery : MonoBehaviour, IInteractWithPlayerRaycast {

        private ShipManager shipManager;
        private InterfaceManager interfaceManager;
        private TerminalManager terminalManager;

        [SerializeField]
        private Text textStoredEnergy;
        [SerializeField]
        private Text textStoredEnergyMax;

        [SerializeField]
        private Toggle toggleLoad;
        [SerializeField]
        private Toggle toggleUnLoad;
       
        private int storedEnergy = 0;
        private int storedEnergyMax = 20;

        // Use this for initialization
        void Start() {
            shipManager = transform.root.GetComponent<ShipManager>();
            shipManager.AddToPing(Ping);

            //terminalManager = transform.root.GetComponent<TerminalManager>();
            //terminalManager.Add("Battery", transform.parent.GetComponent<SpriteRenderer>().sprite, this.transform);

            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
            textStoredEnergyMax.text = storedEnergyMax.ToString();
        }

        void Update() {
            textStoredEnergy.text = storedEnergy.ToString();       
        }

        private void Ping() {
            if (toggleLoad.isOn) {
                if (shipManager.RemoveEnergy(2)) {
                    if ((storedEnergy + 2) >= storedEnergyMax) {
                        storedEnergy = storedEnergyMax;
                        toggleLoad.isOn = false;
                    }
                    else {
                        storedEnergy += 2;
                    }
                }
            }

            if (toggleUnLoad.isOn) {
                if (storedEnergy >= 2) {
                    shipManager.AddEnergy(2);
                    storedEnergy -= 2;
                }
                else {
                    if (storedEnergy == 1) {
                        shipManager.AddEnergy(1);
                        storedEnergy -= 1;
                    }
                    else {
                        //0
                        toggleUnLoad.isOn = false;
                    }
                }
                
            }
        }

        public void RaycastAction() {
            interfaceManager.ShowUI(GetComponent<UI>(), "Battery");
        }
    }
}
