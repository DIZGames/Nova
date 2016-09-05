using Assets.Script.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class UI : MonoBehaviour, IUI {

        [SerializeField]
        private Transform slotList;
        [SerializeField]
        private GameObject uiObject;

        [SerializeField]
        private Transform shield;

        private Transform standardParent;

        public bool IsActive {
            get {
                return uiObject.activeSelf;
            }
        }

        public ISlotContainerList ISlotContainerList {
            get {
                return GetComponent<ISlotContainerList>();
            }
        }

        void Start() {
            standardParent = transform.parent;
        }

        public void Hide() {
            uiObject.SetActive(false);
        }

        public void Move(Transform transform) {
            this.transform.SetParent(transform);
            this.transform.position = transform.position;
            this.transform.rotation = transform.rotation;
        }

        public void Show() {
            uiObject.SetActive(true);
        }

        public void ResetPosition() {
            Move(standardParent);
            uiObject.SetActive(false);
        }
    }
}
