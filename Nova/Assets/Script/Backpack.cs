using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Assets.Script;
using System.Collections.Generic;
using Assets.Script.Interface;

public class Backpack : MonoBehaviour, ISlotContainerList {

    private List<SlotContainer> backPackList = new List<SlotContainer>();
    public Transform slotList;

    void Start () {
        backPackList = new List<SlotContainer>();
        UpdateList();
    }

    public void AddToShipManager(ShipManager shipManager) {
        throw new NotImplementedException();
    }


    public void UpdateList() {
        backPackList.Clear();

        for (int i = 0; i < slotList.childCount; i++) {
            if (slotList.GetChild(i).childCount != 0) {
                backPackList.Add(slotList.GetChild(i).GetChild(0).GetComponent<SlotContainer>());
            }
        }

        Debug.Log("backPackList " + backPackList.Count);
    }

    public int Count(string itemName) {
        int count = 0;

        for (int i = 0; i < backPackList.Count; i++) {
            if (itemName == backPackList[i].ItemBase.itemName) {
                count += backPackList[i].ItemBase.stack;
            }
        }
        return count;
    }

    public int Decrease(string itemName, int count) {

        for (int i = 0; i < backPackList.Count; i++) {
            if (itemName == backPackList[i].ItemBase.itemName && backPackList[i].ItemBase.stack != 0) {
                if (backPackList[i].ItemBase.stack >= count) {
                    backPackList[i].ItemBase.stack -= count;
                    return 0;
                }
                else {
                    count = count - backPackList[i].ItemBase.stack;
                    backPackList[i].ItemBase.stack = 0;
                }
            }
        }
        return count;
    }

  
}



