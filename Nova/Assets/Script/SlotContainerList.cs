using Assets.Script.Interface;
using Assets.Script.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class SlotContainerList : MonoBehaviour, ISlotContainerList, IOpenUI{

        private List<SlotContainer> slotContainerList = new List<SlotContainer>();
        private ShipManager shipManager;
        public Transform slotList;
        public ShipManagerUnitType unitType;
        private InterfaceManager interfaceManager;
        private TerminalManager terminalManager;

        void Start() {
            terminalManager = transform.root.GetComponent<TerminalManager>();
            terminalManager.Add(transform.name, transform.parent.GetComponent<SpriteRenderer>().sprite, this.transform);

            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
            slotContainerList = new List<SlotContainer>();
            UpdateList();

            shipManager = transform.root.GetComponent<ShipManager>();
            shipManager.addContainer(this, unitType); 
        }

        public int Count(string itemName) {
            int count = 0;

            for (int i = 0; i < slotContainerList.Count; i++) {
                if (itemName == slotContainerList[i].Item.Name) {
                    count += slotContainerList[i].Item.stack;
                }
            }
            return count;
        }

        public int Decrease(string itemName, int count) {

            for (int i = 0; i < slotContainerList.Count; i++) {
                if (itemName == slotContainerList[i].Item.Name && slotContainerList[i].Item.stack != 0) {
                    if (slotContainerList[i].Item.stack >= count) {
                        slotContainerList[i].Item.stack -= count;
                        return 0;
                    }
                    else {
                        count = count - slotContainerList[i].Item.stack;
                        slotContainerList[i].Item.stack = 0;
                    }
                }
            }
            return count;
        }

        public void UpdateList() {
            slotContainerList.Clear();

            for (int i = 0; i < slotList.childCount; i++) {
                if (slotList.GetChild(i).childCount != 0) {
                    slotContainerList.Add(slotList.GetChild(i).GetChild(0).GetComponent<SlotContainer>());
                }
            }

            Debug.Log("slotContainerList " + slotContainerList.Count);
        }

        public void OpenUI() {
            interfaceManager.setChildOnUIContainer(this.transform);
        }
    }
}
