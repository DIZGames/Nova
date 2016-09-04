using Assets.Script.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Script {
    public class InteractWithPlayerRaycast : MonoBehaviour{

        [SerializeField]
        private Transform transformImplementingInterface;
        private IInteractWithPlayerRaycast iInteractWithPlayerRaycast;

        void Start() {
            iInteractWithPlayerRaycast = transformImplementingInterface.GetComponent<IInteractWithPlayerRaycast>();
        }

        public void RaycastAction() {
            iInteractWithPlayerRaycast.RaycastAction();
        }
    }
}
