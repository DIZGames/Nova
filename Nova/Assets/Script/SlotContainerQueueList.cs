using Assets.Script.Interface;
using Assets.Script.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class SlotContainerQueueList : MonoBehaviour, ISlotContainerQueueList {

        [SerializeField]
        private Transform queueList;

        private ShipManager shipManager;
        private GameObject goSlotQueue;
        private GameObject goSlotContainer;

        [SerializeField]
        private SlotContainerList slotContainerList;

        [SerializeField]
        private ShipManagerUnitType unitType;

        void Start() {
            this.shipManager = transform.root.GetComponent<ShipManager>();

            goSlotQueue = (GameObject)Resources.Load("Prefab/SlotContainerQueue");
            goSlotContainer = (GameObject)Resources.Load("Prefab/SlotContainer");
        }


        public void AddQueue(SlotContainerRecipe slotContainerRecipe) {

            for (int i = 0; i < queueList.childCount; i++) {
                if (queueList.GetChild(i).childCount == 0) {
                    GameObject gO = (GameObject)Instantiate(goSlotQueue);

                    gO.transform.SetParent(queueList.GetChild(i));

                    gO.GetComponent<SlotContainerQueue>().Recipe = slotContainerRecipe.Recipe;

                    break;
                }
            }
        }

        public void CraftDone(SlotContainerQueue slotContainerQueue) {

            GameObject go = Instantiate(goSlotContainer);

            SlotContainer slotContainer = go.GetComponent<SlotContainer>();

            ItemBase itemBase = slotContainerQueue.Recipe.result.item;
            itemBase.stack = slotContainerQueue.Recipe.result.count;

            slotContainer.ItemBase = itemBase;

            if (slotContainerList.TryAdd(slotContainer)) {
                Destroy(slotContainerQueue.gameObject);

                queueList.GetChild(0).SetAsLastSibling();
            }
        }

        public void DeleteQueue(Transform transform) {
            GameObject parentGO = transform.parent.gameObject;
            transform.parent.SetAsLastSibling();
            Destroy(transform.gameObject);
        }

        public void Ping() {

            if (queueList.GetChild(0).childCount != 0) {
                SlotContainerQueue slotContainerQueue = queueList.GetChild(0).GetChild(0).GetComponent<SlotContainerQueue>();

                ItemBase itemBase = slotContainerQueue.GetIngredientForProgress();

                if (itemBase != null) {
                    if (shipManager.Decrease(itemBase.name, unitType, 1)) {
                        slotContainerQueue.RemoveIngredientFromProgress(itemBase);   
                    }
                }
                slotContainerQueue.Progress();
            }
        }
    }
}
