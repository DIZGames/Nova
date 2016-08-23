using Assets.Script.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script {
    public class OxygenTank : MonoBehaviour, IInteractWithPlayerRaycast{

        private ShipManager shipManager;
        private InterfaceManager interfaceManager;
        private TerminalManager terminalManager;

        [SerializeField]
        private Text textStoredOxygen;
        [SerializeField]
        private Text textStoredOxygenMax;

        [SerializeField]
        private Toggle toggleLoad;
        [SerializeField]
        private Toggle toggleUnLoad;

        private int storedOxygen = 0;
        private int storedOxygenMax = 20;

        void Start() {
            shipManager = transform.root.GetComponent<ShipManager>();
            shipManager.AddToPing(Ping);

            terminalManager = transform.root.GetComponent<TerminalManager>();
            terminalManager.Add("OxygenTank", transform.parent.GetComponent<SpriteRenderer>().sprite, this.transform);

            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();

            textStoredOxygenMax.text = storedOxygenMax.ToString();
        }

        void Update() {
            textStoredOxygen.text = storedOxygen.ToString();
        }

        private void Ping() {
            if (toggleLoad.isOn) {
                if (shipManager.RemoveOxygen(2)) {
                    if ((storedOxygen + 2) >= storedOxygenMax) {
                        storedOxygen = storedOxygenMax;
                        toggleLoad.isOn = false;
                    }
                    else {
                        storedOxygen += 2;
                    }
                }
            }

            if (toggleUnLoad.isOn) {
                if (storedOxygen >= 2) {
                    shipManager.AddOxygen(2);
                    storedOxygen -= 2;
                }
                else {
                    if (storedOxygen == 1) {
                        shipManager.AddOxygen(1);
                        storedOxygen -= 1;
                    }
                    else {
                        //0
                        toggleUnLoad.isOn = false;
                    }
                }

            }
        }

        public void RaycastAction() {
            interfaceManager.ShowUI(GetComponent<IUI>(), "Oxygen Tank");
        }
    }
}
