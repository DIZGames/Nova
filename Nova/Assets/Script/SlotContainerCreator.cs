using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class SlotContainerCreator : MonoBehaviour{

        public Transform slotList;

        public void CreateSlotContainer(ItemBase itemBase, int stack) {
            GameObject goSlotContainer = (GameObject)Resources.Load("Prefab/SlotContainer");

            while (itemBase.maxStack <= stack) {

                stack -= itemBase.maxStack;

                GameObject gObject1 = Instantiate(goSlotContainer);
                gObject1.name = itemBase.name;

                SlotContainer slotContainer1 = gObject1.GetComponent<SlotContainer>();
                ItemBase itemBase1 = itemBase.Clone();
                itemBase1.stack = itemBase.maxStack;

                slotContainer1.ItemBase = itemBase1;


                Add(slotContainer1);
            }

            if (stack > 0 ) {

                GameObject gObject = Instantiate(goSlotContainer);
                gObject.name = itemBase.name;

                SlotContainer slotContainer = gObject.GetComponent<SlotContainer>();
                ItemBase itemBase1 = itemBase.Clone();
                itemBase1.stack = stack;

                slotContainer.ItemBase = itemBase1;

                Add(slotContainer);
            }
        }

        public void DeleteAll() {
            for (int i = 0; i < slotList.childCount; i++) {
                if (slotList.GetChild(i).childCount != 0) {
                    DestroyImmediate(slotList.GetChild(i).GetChild(0).gameObject);
                }
            }
        }

        private void Add(SlotContainer slotContainer) {
            bool flag = false;

            for (int i = 0; i < slotList.childCount; i++) {
                if (slotList.GetChild(i).childCount == 0) {
                    SlotDrop slotDrop = slotList.GetChild(i).GetComponent<SlotDrop>();

                    if (slotDrop.checkAllowedTypes(slotContainer)) {
                        slotContainer.transform.SetParent(slotDrop.transform);
                        flag = true;
                        break;
                    }
                    

                }
            }

            if (flag == false) {
                DestroyImmediate(slotContainer.gameObject);
            }
        }
    }
}
