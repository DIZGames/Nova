using Assets.Script.Interface;
using Assets.Script.RecipeSystem;
using Assets.Script.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script {
    public class Refinery : MonoBehaviour, IInteractWithPlayerRaycast, ITest {

        private ShipManager shipManager;

        [SerializeField]
        private Toggle toggle;

        private InterfaceManager interfaceManager;

        private ISlotContainerList slotContainerList;
        private ISlotContainerQueueList slotContainerQueueList;
        private ISlotContainerRecipeList slotContainerRecipeList;

        public bool Power {
            get {
                return toggle.isOn;
            }

            set {
                toggle.isOn = value;
            }
        }

        public GameObject gameObject1 {
            get {
                return this.gameObject;
            }
        }

        public IUI iUI {
            get {
                return GetComponent<IUI>();
            }
        }

        void Start() {

            shipManager = transform.root.GetComponent<ShipManager>();
            shipManager.AddToConsumerPrio2List(this);

            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();

            slotContainerList = GetComponent<ISlotContainerList>();
            slotContainerQueueList = GetComponent<ISlotContainerQueueList>();
            slotContainerRecipeList = GetComponent<ISlotContainerRecipeList>();


        }

        public void RaycastAction() {
            interfaceManager.ShowUIWithBackpack(GetComponent<IUI>(), "Raffinerie");
        }

        public void Ping() {
            if (toggle.isOn) {
                if (shipManager.RemoveEnergy(1)) {
                    slotContainerQueueList.Ping();
                }     
            }
        }
    }
}
