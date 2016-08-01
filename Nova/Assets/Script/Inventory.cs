using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {

    public class Inventory : MonoBehaviour {

        public List<GameObject> List;

        private List<InventoryInterface> interfaceList;

        void Start() {

            interfaceList = new List<InventoryInterface>();

            for (int i = 0; i < List.Count; i++) {
                interfaceList.Add(List[i].GetComponent<InventoryInterface>());
            }

        }

        public int Count(string name) {
            int count = 0;

            for (int i = 0; i < interfaceList.Count; i++) {
                count += interfaceList[i].Count(name);
            }

            return count;

        }

        public void ReduceStackOne(string name) {
            for (int i = 0; i < interfaceList.Count; i++) {
                if (interfaceList[i].ReduceStackOne(name))
                    break;               
            }
        }

        public void UpdateLists() {
            for (int i = 0; i < interfaceList.Count; i++) {
                interfaceList[i].UpdateList();
            }
        }

    }
}
