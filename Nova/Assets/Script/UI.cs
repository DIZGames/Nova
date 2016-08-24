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

        private Transform standardParent;

        void Start() {
            standardParent = transform.parent;
        }

        public void Add(SlotContainer slotContainer) {

            for (int i = 0; i < slotList.childCount; i++) {
                if (slotList.GetChild(i).childCount == 0) {
                    SlotDrop slotDrop = slotList.GetChild(i).GetComponent<SlotDrop>();

                    if (slotDrop.checkAllowedTypes(slotContainer)) {
                        slotContainer.transform.SetParent(slotDrop.transform);
                        break;
                    }
                }
            }
        }

        public void Hide() {
            uiObject.SetActive(false);
        }

        public bool IsActive() {
            return uiObject.activeSelf;
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

        public bool FreeSlot() {
            for (int i = 0; i < slotList.childCount; i++) {
                if (slotList.GetChild(i).childCount == 0) {
                    return true;
                }
            }
            return false;
        }
    }
}
