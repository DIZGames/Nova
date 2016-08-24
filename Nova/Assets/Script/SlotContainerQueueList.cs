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

        [SerializeField]
        private ShipManagerUnitType unitType;
        [SerializeField]
        private SlotContainerCreator slotContainerCreator;

        public void AddToShipManager(ShipManager shipManager) {

            this.shipManager = shipManager;

            goSlotQueue = (GameObject)Resources.Load("Prefab/SlotContainerQueue");
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

            slotContainerCreator.CreateSlotContainer(slotContainerQueue.Recipe.result.item, slotContainerQueue.Recipe.result.count);

            Destroy(slotContainerQueue.gameObject);

            queueList.GetChild(0).SetAsLastSibling();

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
                        slotContainerQueue.Progress();
                    }
                }
            }
        }
    }
}
