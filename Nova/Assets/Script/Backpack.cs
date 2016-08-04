using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Assets.Script;
using System.Collections.Generic;

public class Backpack : MonoBehaviour, InventoryInterface {

    private List<SlotContainer> backPackList;

    void Start () {
        backPackList = new List<SlotContainer>();
        UpdateList();
        Debug.Log("start called");
    }

    public void UpdateList() {
        backPackList.Clear();

        for (int i = 0; i < transform.GetChild(0).childCount; i++) {
            if (transform.GetChild(0).GetChild(i).childCount != 0) {
                backPackList.Add(transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<SlotContainer>());
            }
        }

        Debug.Log("backPackList " + backPackList.Count);
    }

    public int Count(string name) {
        int count = 0;

        for (int i = 0; i < backPackList.Count; i++) {
            if (name == backPackList[i].Item.Name) {
                count += backPackList[i].Item.stack;
            }
        }
        return count;
    }

    public bool ReduceStackOne(string name) {
        for (int i = 0; i < backPackList.Count; i++) {
            if (name == backPackList[i].Item.Name && backPackList[i].Item.stack != 0) {
                backPackList[i].Item.stack--;
                return true;
            }
        }
        return false;
    }

    public void Add(GameObject gOContainer) {
        for (int i = 0; i < transform.GetChild(0).childCount; i++) {
            if (transform.GetChild(0).GetChild(i).childCount == 0) {

                gOContainer.transform.SetParent(transform.GetChild(0).GetChild(i));
                gOContainer.transform.position = transform.GetChild(0).GetChild(i).position;

                break;
            }
        }
    }
}



