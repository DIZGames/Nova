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

                    slotContainer.prefab = item.prefab;
                    slotContainer.Stack = stack;

                    ItemList itemDataBase = (ItemList)Resources.Load("ItemDataBase");


                    ItemTool itemTool = (ItemTool)item;
                    ItemAmmo itemAmmo = (ItemAmmo)itemDataBase.getItemByName("Rocket");

                    ToolData toolData = new ToolData();
                    toolData.Ammo = itemAmmo;
                    toolData.BulletSpeed = itemTool.BulletSpeed;
                    toolData.FireRate = itemTool.FireRate;
                    toolData.icon = itemTool.icon;
                    toolData.maxStack = itemTool.maxStack;
                    toolData.name = itemTool.itemName;
                    toolData.type = itemTool.type;

                    slotContainer.ItemData = toolData;

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
