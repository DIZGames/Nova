using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class SlotDrop : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData) {

        if (eventData.pointerDrag != null) {
            if (transform.childCount == 0) { //Wenn Slot leer ist
                eventData.pointerDrag.transform.SetParent(transform);
            }
            else { //Wenn Slot bereits einen SlotContainer besitzt

                SlotContainer draggedContainer = eventData.pointerDrag.GetComponent<SlotContainer>();
                SlotContainer slotContainer = transform.GetChild(0).GetComponent<SlotContainer>();

                if (draggedContainer.prefab.name == slotContainer.prefab.name && (slotContainer.Stack != slotContainer.ItemData.maxStack)) {
                    //Wenn beide Items den gleichen Namen haben und der Container auf dem Slot nicht MaxStack hat

                    int freeStackOnSlot = slotContainer.ItemData.maxStack - slotContainer.Stack;

                     if (draggedContainer.Stack <= freeStackOnSlot) {
                        slotContainer.Stack += draggedContainer.Stack;
                        Destroy(eventData.pointerDrag);
                    }
                    else {
                        draggedContainer.Stack -= freeStackOnSlot;
                        slotContainer.Stack += freeStackOnSlot;
                    }
                }
                else { //SWAP
      
                    //ItemBase itemBase = slotContainer.prefab;
                    //int Stack = slotContainer.Stack;

                    //slotContainer.prefab = draggedContainer.prefab;
                    //slotContainer.Stack = draggedContainer.Stack;

                    //draggedContainer.prefab = itemBase;
                    //draggedContainer.Stack = Stack;


                }

            }

        }


    }

}
