using Assets.Script.ItemSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class SlotList : MonoBehaviour {

        private GameObject SlotContainer;

        public void addItemToNextFreeSlot(ItemBase item, int stack) {
            SlotContainer = (GameObject)Resources.Load("Prefab/SlotContainer");

            for (int i = 0; i < transform.childCount; i++) {
                if (transform.GetChild(i).childCount == 0) {
                    transform.SetParent(transform.GetChild(i));

                    GameObject gObject = Instantiate(SlotContainer);
                    gObject.transform.SetParent(transform.GetChild(i));
                    gObject.transform.position = transform.GetChild(i).position;

                    SlotContainer slotContainer = gObject.GetComponent<SlotContainer>();

                    ItemValues itemValues = null; 

                    switch (item.type)
                    {
                        case ItemType.Ammo:
                            itemValues = ScriptableObject.CreateInstance<ItemAmmoValues>();
                            break;
                        case ItemType.Tool:
                            itemValues = ScriptableObject.CreateInstance<ItemToolValues>();
                            break;

                    }

                    itemValues.itemBase = item;
                    slotContainer.Item = itemValues;
                    slotContainer.Item.stack = stack;

                    break;

                }


            }
        }

        public Transform getNextFreeSlot() {
            for (int i = 0; i < transform.childCount; i++) {
                if (transform.GetChild(i).childCount == 0) {
                    return transform.GetChild(i);
                }
            }
            return null;
        }

        public void DeleteAllItems() {
            for (int i = 0; i < transform.childCount; i++) {
                if (transform.GetChild(i).childCount != 0) {
                    DestroyImmediate(transform.GetChild(i).GetChild(0).gameObject);
                }
            }
        }



    }
}
