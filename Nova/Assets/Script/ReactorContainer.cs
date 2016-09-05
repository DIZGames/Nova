using Assets.Script.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class ReactorContainer : MonoBehaviour, IInteractWithPlayerRaycast, ITest {

        [SerializeField]
        private ISlotContainerList slotContainerList;

        private InterfaceManager interfaceManager;
        private ShipManager shipManager;

        public bool Power {
            get {
                return true;
            }
            set {

            }
        }

        public GameObject gameObject1 {
            get {
                return gameObject;
            }
        }

        public IUI iUI {
            get {
                return GetComponent<IUI>();
            }
        }

        void Start() {
            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();

            shipManager = transform.root.GetComponent<ShipManager>();
            shipManager.AddToContainerEnergyList(this);

        }

        public void RaycastAction() {
            interfaceManager.ShowUIWithBackpack(GetComponent<UI>(), "Reactor Container");
        }

        public void Ping() {
      
        }
    }
}
