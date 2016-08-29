using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Script;
using Assets.Script.ItemSystem;
using Assets.Script.Interface;

public class SlotContainerSplit : MonoBehaviour, IPointerDownHandler {

    public void OnPointerDown(PointerEventData eventData) {

        if (eventData.button == PointerEventData.InputButton.Right) {

            SlotContainer SlotContainer = transform.GetComponent<SlotContainer>();

            if (SlotContainer.ItemBase.stack > 1) {
                int modulo = 0;

                if (SlotContainer.ItemBase.stack % 2 == 1) {
                    modulo = 1;
                }

                int result = SlotContainer.ItemBase.stack / 2;
                SlotContainer.ItemBase.stack = result + modulo;                

                ItemBase itemBaseNew = SlotContainer.ItemBase.Clone();
                itemBaseNew.stack = result;

                Transform newParent = nextFreeSlot();

                if (newParent != null) {
                    GameObject gObject = Instantiate(gameObject);

                    gObject.name = itemBaseNew.itemName;
                    gObject.GetComponent<SlotContainer>().ItemBase = itemBaseNew;
                    gObject.transform.SetParent(newParent);
                    gObject.transform.position = newParent.position;
                }
                else {
                    SlotContainer.ItemBase.stack = 2 * result + modulo;
                }
                ExecuteEvents.ExecuteHierarchy<ISlotContainerList>(gameObject, null, (x, y) => x.Refresh());
            }
        }
    }

    private Transform nextFreeSlot() {

        Transform slotList = transform.parent.parent;

        for (int i = 0; i < slotList.childCount; i++) {
            if (slotList.GetChild(i).childCount == 0) {
                return slotList.GetChild(i);
            }
        }
        return null;

    }



}
