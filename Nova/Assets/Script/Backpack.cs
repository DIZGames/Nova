using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Assets.Script;

public class Backpack : MonoBehaviour, IHasChanged {

    private SlotList slotList;

    void Start () {
        slotList = transform.GetChild(0).GetComponent<SlotList>();	

	}

    void Update() {

    }

    public void AddSlotContainerToList(GameObject slotContainer) {

        for (int i = 0; i < transform.GetChild(0).childCount; i++) {
            if (transform.GetChild(0).GetChild(i).childCount == 0) {

                slotContainer.transform.SetParent(transform.GetChild(0).GetChild(i));
                slotContainer.transform.position = transform.GetChild(0).GetChild(i).position;

                break;

            }
        }
    }

}



