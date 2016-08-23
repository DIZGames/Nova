using Assets.Script.ItemSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class SlotList : MonoBehaviour {

        [SerializeField]
        private IUI uI;


        public void addItemToNextFreeSlot(ItemBase item, int stack) {
            GameObject SlotContainer = (GameObject)Resources.Load("Prefab/SlotContainer");

            for (int i = 0; i < transform.childCount; i++) {
                if (transform.GetChild(i).childCount == 0) {
                    transform.SetParent(transform.GetChild(i));

                    GameObject gObject = Instantiate(SlotContainer);
                    gObject.transform.SetParent(transform.GetChild(i));
                    gObject.transform.position = transform.GetChild(i).position;
                    gObject.name = item.name;
                    
                    //ItemValues itemValues = null;

                    //SlotContainer slotContainer = gObject.GetComponent<SlotContainer>();

                    //switch (item.type) {
                    //    case ItemType.Ammo:
                    //        itemValues = ScriptableObject.CreateInstance<ItemAmmoValues>();
                    //        break;
                    //    case ItemType.Tool:
                    //        itemValues = ScriptableObject.CreateInstance<ItemToolValues>();
                    //        break;
                    //    case ItemType.Clothing:
                    //        itemValues = ScriptableObject.CreateInstance<ItemClothingValues>();
                    //        break;
                    //    case ItemType.Material:
                    //        itemValues = ScriptableObject.CreateInstance<ItemMaterialValues>();
                    //        break;
                    //    case ItemType.Consumable:
                    //        itemValues = ScriptableObject.CreateInstance<ItemConsumableValues>();
                    //        break;
                    //    case ItemType.Block:
                    //        itemValues = ScriptableObject.CreateInstance<ItemBlockValues>();
                    //        break;
                    //}

                    //itemValues.itemBase = item;

                    //if (stack > item.maxStack) {
                    //    itemValues.stack = item.maxStack;
                    //    stack -= item.maxStack;
                    //}
                    //else {
                    //    itemValues.stack = stack;
                    //    stack = 0;
                    //}
     
                    ////slotContainer.Item = itemValues;

                    //if (stack == 0) {
                    //    break;
                    //}
                }
            }
        }

        public void addItemToItemStack(ItemBase item, int stack) {
            //GameObject SlotContainer = (GameObject)Resources.Load("Prefab/SlotContainer");

            //for (int i = 0; i < transform.childCount; i++) {
            //    if (transform.GetChild(i).childCount != 0) {
            //        SlotContainer slotContainer1 = transform.GetChild(i).GetChild(0).GetComponent<SlotContainer>();

            //        if (slotContainer1.Item.Name == item.name) {
            //            if (item.maxStack > (slotContainer1.Item.stack + stack)) {
            //                slotContainer1.Item.stack += stack;
            //                stack = 0;
            //            }
            //            else {
            //                int a = item.maxStack - slotContainer1.Item.stack;
            //                stack -= a;
            //                slotContainer1.Item.stack = item.maxStack;
            //            }
                        
                        

            //        }

            //    }

            //    if (stack == 0) {
            //        break;
            //    }
            //}
            
            //if (stack > 0) {

            //    addItemToNextFreeSlot(item, stack);
            //}
                  
            
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
