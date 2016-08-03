using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Script.ItemSystem;
using Assets.Script;
using System.Collections.Generic;

public class SlotDrop : MonoBehaviour, IDropHandler {

    public List<ItemType> allowedTypes;
    public List<ClothingType> allowedClothingTypes;

    private bool checkAllowedTypes(SlotContainer slotContainer) {

        if (slotContainer != null) {
            ItemType type = slotContainer.Item.Type;

            if (allowedTypes.Count == 0) {
                return true;
            }
            else {
                for (int i = 0; i < allowedTypes.Count; i++) {
                    if (allowedTypes[i] == type) {

                        //SPEZIALFALL: Clothing
                        if (type == ItemType.Clothing) {
                            if (allowedClothingTypes.Count == 0) {
                                return true;
                            }
                            else {
                                ClothingType clothingType = ((ItemClothingValues)slotContainer.Item).clothingType;

                                for (int j = 0; j < allowedClothingTypes.Count; j++) {
                                    if (allowedClothingTypes[i] == clothingType) {
                                        return true;
                                    }
                                }
                                return false;
                            }
                        }
                        return true;
                    }
                }
            }
        }   
        return false;
    }

    public void OnDrop(PointerEventData eventData) {

        if (eventData.pointerDrag != null) {
            if (checkAllowedTypes(eventData.pointerDrag.GetComponent<SlotContainer>())) {
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
}
