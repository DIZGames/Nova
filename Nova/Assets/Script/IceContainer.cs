using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class IceContainer : MonoBehaviour, IInteractWithPlayerRaycast {

        [SerializeField]
        private SlotContainerList slotContainerList;

        private InterfaceManager interfaceManager;
        void Start() {
            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();

            slotContainerList.AddToShipManager(transform.root.GetComponent<ShipManager>());
        }

        public void RaycastAction() {
            interfaceManager.ShowUIWithBackpack(GetComponent<UI>(), "Ice Container");
        }

  

    }
}
