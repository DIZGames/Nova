using Assets.Script.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class PlayerInventory : MonoBehaviour, ISlotContainerList{

        public List<GameObject> List;
        private List<ISlotContainerList> interfaceList;

        void Start() {
            interfaceList = new List<ISlotContainerList>();

            for (int i = 0; i < List.Count; i++) {
                interfaceList.Add(List[i].GetComponent<ISlotContainerList>());
            }

        }

        public int Count(string name) {
            int count = 0;

            for (int i = 0; i < interfaceList.Count; i++) {
                count += interfaceList[i].Count(name);
            }

            return count;
        }

        //public bool Decrease(string name, int count) {
        //    if (count > Count(name)){
        //        return false;
        //    }

        //    int tempCount = 0;

        //    for (int i = 0; i < interfaceList.Count; i++) {
        //        tempCount = interfaceList[i].Remove(name, count);
        //        if (tempCount == 0) {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //public void UpdateLists() {
        //    for (int i = 0; i < interfaceList.Count; i++) {
        //        interfaceList[i].Refresh();
        //    }
        //}

        public void Refresh()
        {
            for (int i = 0; i < interfaceList.Count; i++)
            {
                interfaceList[i].Refresh();
            }
        }

        public int Remove(string itemName, int count)
        {
            int rest = 0;

            for (int i = 0; i < interfaceList.Count; i++)
            {
                rest = interfaceList[i].Remove(itemName, count);

                if (rest == 0)
                    break;
               
            }
            return rest;
        }

        public bool TryAdd(SlotContainer slotContainer)
        {
            for (int i = 0; i < interfaceList.Count; i++)
            {
                bool flag = interfaceList[i].TryAdd(slotContainer);
                if (flag) {
                    return true;
                }
            }
            return false;    
        }
    }
}
