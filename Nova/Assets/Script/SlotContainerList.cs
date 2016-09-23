using Assets.Script.Interface;
using Assets.Script.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class SlotContainerList : MonoBehaviour, ISlotContainerList{

        private List<SlotContainer> slotContainerList = new List<SlotContainer>();

        private ShipManager shipManager;
        public Transform slotList;
        public ShipManagerUnitType unitType;

        void Start() {
            this.shipManager = transform.root.GetComponent<ShipManager>();
            if (shipManager != null) {
                this.shipManager.addContainer(this, unitType);
            }

            slotContainerList = new List<SlotContainer>();
            Refresh();
        }

        public int Count(string itemName) {
            int count = 0;

            for (int i = 0; i < slotContainerList.Count; i++) {
                if (itemName == slotContainerList[i].ItemBase.itemName) {
                    count += slotContainerList[i].ItemBase.stack;
                }
            }
            return count;
        }

        public int Remove(string itemName, int count) {

            for (int i = 0; i < slotContainerList.Count; i++) {
                if (itemName == slotContainerList[i].ItemBase.itemName && slotContainerList[i].ItemBase.stack != 0) {
                    if (slotContainerList[i].ItemBase.stack >= count) {
                        slotContainerList[i].ItemBase.stack -= count;
                        return 0;
                    }
                    else {
                        count = count - slotContainerList[i].ItemBase.stack;
                        slotContainerList[i].ItemBase.stack = 0;
                    }
                }
            }
            return count;
        }

        public void Refresh() {
            slotContainerList.Clear();

            for (int i = 0; i < slotList.childCount; i++) {
                if (slotList.GetChild(i).childCount != 0) {
                    slotContainerList.Add(slotList.GetChild(i).GetChild(0).GetComponent<SlotContainer>());
                }
            }

            Debug.Log(gameObject.name +" "+slotContainerList.Count);
        }

        public bool TryAdd(SlotContainer slotContainer) {
            for (int i = 0; i < slotList.childCount; i++) {
                if (slotList.GetChild(i).childCount == 0) {
                    SlotDrop slotDrop = slotList.GetChild(i).GetComponent<SlotDrop>();

                    if (slotDrop.checkAllowedTypes(slotContainer)) {
                        slotContainer.transform.SetParent(slotDrop.transform);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
