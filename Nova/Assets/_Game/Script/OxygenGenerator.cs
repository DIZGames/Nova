using Assets.Script.Interface;
using Assets.Script.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script {
    public class OxygenGenerator : MonoBehaviour, IInteractWithPlayerRaycast, ITest{

        public ShipManagerUnitType unitType;

        private ShipManager shipManager;
        private InterfaceManager interfaceManager;

        [SerializeField]
        private Toggle toggle;

        private bool isPing;

        public GameObject gameObject1 {
            get {
                return gameObject;
            }
        }

        public bool Power {
            get {
                return toggle.isOn;
            }

            set {
                toggle.isOn = value;
            }
        }

        public IUI iUI {
            get {
                return GetComponent<IUI>();
            }
        }

        // Use this for initialization
        void Start() {
            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
            shipManager = transform.root.GetComponent<ShipManager>();
            //shipManager.AddToPing(ProduceOxygen);
            shipManager.AddToOxygenList(this);

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

        public void Ping() {
            if (toggle.isOn) {
                if (shipManager.RemoveEnergy(1)) {
                    if (shipManager.Decrease("Ice", unitType, 1)) {
                        shipManager.AddOxygen(2);
                    }
                }   
            }
        }

        public void Consume() {
            throw new NotImplementedException();
        }
    }
}
