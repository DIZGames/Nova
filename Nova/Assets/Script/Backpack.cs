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
            if (itemName == backPackList[i].Item.Name) {
                count += backPackList[i].Item.stack;
            }
        }
        return count;
    }

    public int Decrease(string itemName, int count) {

        for (int i = 0; i < backPackList.Count; i++) {
            if (itemName == backPackList[i].Item.Name && backPackList[i].Item.stack != 0) {
                if (backPackList[i].Item.stack >= count) {
                    backPackList[i].Item.stack -= count;
                    return 0;
                }
                else {
                    count = count - backPackList[i].Item.stack;
                    backPackList[i].Item.stack = 0;
                }
            }
        }
        return count;
    }
}



