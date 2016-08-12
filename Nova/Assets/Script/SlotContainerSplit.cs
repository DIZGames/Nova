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

            if (SlotContainer.Item.stack > 1) {
                int modulo = 0;

                if (SlotContainer.Item.stack % 2 == 1) {
                    modulo = 1;
                }

                int result = SlotContainer.Item.stack / 2;
                SlotContainer.Item.stack = result + modulo;

                

                ItemValues itemValues = null;

                switch (SlotContainer.Item.Type) {
                    case ItemType.Ammo:
                        itemValues = ScriptableObject.CreateInstance<ItemAmmoValues>();
                        break;
                    case ItemType.Tool:
                        itemValues = ScriptableObject.CreateInstance<ItemToolValues>();
                        break;
                    case ItemType.Clothing:
                        itemValues = ScriptableObject.CreateInstance<ItemClothingValues>();
                        break;
                    case ItemType.Material:
                        itemValues = ScriptableObject.CreateInstance<ItemMaterialValues>();
                        break;
                    case ItemType.Consumable:
                        itemValues = ScriptableObject.CreateInstance<ItemConsumableValues>();
                        break;
                    case ItemType.Block:
                        itemValues = ScriptableObject.CreateInstance<ItemBlockValues>();
                        break;
                }

                itemValues.itemBase = SlotContainer.Item.itemBase;
                itemValues.stack = result;
              
                Transform newParent = nextFreeSlot();

                if (newParent != null) {
                    GameObject gObject = Instantiate(gameObject);

                    gObject.name = itemValues.Name;
                    gObject.GetComponent<SlotContainer>().Item = itemValues;
                    gObject.transform.SetParent(newParent);
                    gObject.transform.position = newParent.position;
                }

                ExecuteEvents.ExecuteHierarchy<ISlotContainerList>(gameObject, null, (x, y) => x.UpdateList());

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
