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
