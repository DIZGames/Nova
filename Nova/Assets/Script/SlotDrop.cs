using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Script.ItemSystem;
using Assets.Script;
using System.Collections.Generic;

public class SlotDrop : MonoBehaviour, IDropHandler {

    public List<ItemType> allowedTypes;
    public List<ItemTypeMaterial> allowedMaterial;
    public List<ItemTypeClothing> allowedClothingTypes;

    public bool checkAllowedTypes(SlotContainer slotContainer) {

        if (slotContainer != null) {
            ItemType type = slotContainer.ItemBase.type;

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
                                ItemTypeClothing clothingType = ((ItemClothing)slotContainer.ItemBase).clothingType;

                                for (int j = 0; j < allowedClothingTypes.Count; j++) {
                                    if (allowedClothingTypes[i] == clothingType) {
                                        return true;
                                    }
                                }
                                return false;
                            }
                        }
                        //SPEZIALFALL: Material
                        if (type == ItemType.Material)
                        {
                            if (allowedMaterial.Count == 0)
                            {
                                return true;
                            }
                            else
                            {
                                ItemTypeMaterial materialType = ((ItemMaterial)slotContainer.ItemBase).materialType;

                                for (int j = 0; j < allowedMaterial.Count; j++)
                                {
                                    if (allowedMaterial[i] == materialType)
                                    {
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

                    if (draggedContainer.ItemBase.itemName== slotContainer.ItemBase.itemName && (slotContainer.ItemBase.stack != slotContainer.ItemBase.maxStack)) {
                        //Wenn beide Items den gleichen Namen haben und der Container auf dem Slot nicht MaxStack hat

                        int freeStackOnSlot = slotContainer.ItemBase.maxStack - slotContainer.ItemBase.stack;

                        if (draggedContainer.ItemBase.stack <= freeStackOnSlot) {
                            slotContainer.ItemBase.stack += draggedContainer.ItemBase.stack;
                            Destroy(eventData.pointerDrag);
                        }
                        else {
                            draggedContainer.ItemBase.stack -= freeStackOnSlot;
                            slotContainer.ItemBase.stack += freeStackOnSlot;
                        }
                    }
                    else { //SWAP

                        ItemBase itemBase = slotContainer.ItemBase;
                        int Stack = slotContainer.ItemBase.stack;

                        slotContainer.ItemBase = draggedContainer.ItemBase;
                        slotContainer.ItemBase.stack = draggedContainer.ItemBase.stack;

                        draggedContainer.ItemBase = itemBase;
                        draggedContainer.ItemBase.stack = Stack;
                    }
                }
            }

           
        }
    }
}
