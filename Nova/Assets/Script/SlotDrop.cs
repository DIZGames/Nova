using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Script.ItemSystem;

public class SlotDrop : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData) {

        if (eventData.pointerDrag != null) {
            if (transform.childCount == 0) { //Wenn Slot leer ist
                eventData.pointerDrag.transform.SetParent(transform);
            }
            else { //Wenn Slot bereits einen SlotContainer besitzt

                SlotContainer draggedContainer = eventData.pointerDrag.GetComponent<SlotContainer>();
                SlotContainer slotContainer = transform.GetChild(0).GetComponent<SlotContainer>();

                if (draggedContainer.Item.Name == slotContainer.Item.Name && (slotContainer.Item.stack != slotContainer.Item.MaxStack)) {
                    //Wenn beide Items den gleichen Namen haben und der Container auf dem Slot nicht MaxStack hat

                    int freeStackOnSlot = slotContainer.Item.MaxStack - slotContainer.Item.stack;

                     if (draggedContainer.Item.stack <= freeStackOnSlot) {
                        slotContainer.Item.stack += draggedContainer.Item.stack;
                        Destroy(eventData.pointerDrag);
                    }
                    else {
                        draggedContainer.Item.stack -= freeStackOnSlot;
                        slotContainer.Item.stack += freeStackOnSlot;
                    }
                }
                else { //SWAP
      
                    ItemValues itemValues = slotContainer.Item;
                    int Stack = slotContainer.Item.stack;

                    slotContainer.Item = draggedContainer.Item;
                    slotContainer.Item.stack = draggedContainer.Item.stack;

                    draggedContainer.Item = itemValues;
                    draggedContainer.Item.stack = Stack;


                }

            }

        }


    }

}
