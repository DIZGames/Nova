using Assets.Script.Interface;
using Assets.Script.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script {
    public class Battery : MonoBehaviour, IInteractWithPlayerRaycast, ITest {

        private ShipManager shipManager;
        private InterfaceManager interfaceManager;

        [SerializeField]
        private Text textStoredEnergy;
        [SerializeField]
        private Text textStoredEnergyMax;

        [SerializeField]
        private Toggle toggle;

        [SerializeField]
        private Text textStorageSwitch; 

        [SerializeField]
        private Toggle toggleStorageSwitch;
       
        private int storedEnergy = 0;
        private int storedEnergyMax = 20;

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

        void Start() {
            shipManager = transform.root.GetComponent<ShipManager>();
            shipManager.AddToStorageList(this);

            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
            textStoredEnergyMax.text = storedEnergyMax.ToString();
        }

        void Update() {
            //textStoredEnergy.text = storedEnergy.ToString();       
        }

        public void RaycastAction() {
            interfaceManager.ShowUI(GetComponent<UI>(), "Battery");
        }

        public void Ping() {

            if (toggle.isOn) {
                if (toggleStorageSwitch.isOn ) { // Aufladen
                    if (shipManager.isProducing == false) {
                        if (storedEnergyMax > storedEnergy) {
                            int en = storedEnergyMax - storedEnergy;
                            int en1 = shipManager.Energy;
                            if (en1 >= en) {
                                shipManager.RemoveEnergy(en);
                                storedEnergy += en;
                            }
                            else {
                                shipManager.RemoveEnergy(en1);
                                storedEnergy += en1;
                            }
                        }
                    }
                }
                else { //Entladen
                    if (shipManager.isProducing == true) {
                        if (storedEnergy >= 2) {
                            shipManager.AddEnergy(2);
                            storedEnergy -= 2;
                        }    
                        else if (storedEnergy == 1) {
                            shipManager.AddEnergy(1);
                            storedEnergy -= 1;
                        }
                    }      
                }
                textStoredEnergy.text = storedEnergy.ToString();
            }
        }

        public void ToggleStorage() {
            if (toggleStorageSwitch.isOn)
                textStorageSwitch.text = "wird geladen";
            else
                textStorageSwitch.text = "wird entladen";
        }
    }
}
