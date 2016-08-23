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
    public class Refinery : MonoBehaviour, IInteractWithPlayerRaycast {

        private ShipManager shipManager;

        [SerializeField]
        private Transform recipeList;
        [SerializeField]
        private Transform queueList;
        [SerializeField]
        private Transform slotList;

        [SerializeField]
        private Toggle toggle;

        private InterfaceManager interfaceManager;

        private ISlotContainerList slotContainerList;
        private ISlotContainerQueueList slotContainerQueueList;
        private ISlotContainerRecipeList slotContainerRecipeList;
        

        // Use this for initialization
        void Start() {

            shipManager = transform.root.GetComponent<ShipManager>();
            shipManager.AddToPing(UpdateQueue);

            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();

            slotContainerList = slotList.GetComponent<ISlotContainerList>();
            slotContainerQueueList = queueList.GetComponent<ISlotContainerQueueList>();
            slotContainerRecipeList = recipeList.GetComponent<ISlotContainerRecipeList>();

            slotContainerList.AddToShipManager(shipManager);
            slotContainerQueueList.AddToShipManager(shipManager);
            slotContainerRecipeList.AddToShipManager(shipManager);

        }

        private void UpdateQueue() {
            if (toggle.isOn) {
                slotContainerQueueList.Ping();
            }        
        }

        public void RaycastAction() {
            interfaceManager.ShowUI(GetComponent<IUI>(), "Raffinerie");
        }

    }
}
