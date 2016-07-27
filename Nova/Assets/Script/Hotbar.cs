using UnityEngine;
using System.Collections;
using Assets.Script;
using System;

public class Hotbar : MonoBehaviour, IHasChanged {

    public Player player;

    void Start() {

    }

    void Update() {
       
        if (Input.GetButtonDown("Hotbar1")) {
            selectHotbar(0);
        }
        if (Input.GetButtonDown("Hotbar2")) {
            selectHotbar(1);
        }
        if (Input.GetButtonDown("Hotbar3")) {
            selectHotbar(2);
        }
        if (Input.GetButtonDown("Hotbar4")) {
            selectHotbar(3);
        }
        if (Input.GetButtonDown("Hotbar5")) {
            selectHotbar(4);
        }
        if (Input.GetButtonDown("Hotbar6")) {
            selectHotbar(5);
        }
        if (Input.GetButtonDown("Hotbar7")) {
            selectHotbar(6);
        }
        if (Input.GetButtonDown("Hotbar8")) {
            selectHotbar(7);
        }
        if (Input.GetButtonDown("Hotbar9")) {
            selectHotbar(8);
        }
        if (Input.GetButtonDown("Hotbar0")) {
            selectHotbar(9);
        }
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

    private void selectHotbar(int HotbarID) {

        SlotContainer slotContainer = getSlotContainer(HotbarID);

        if (slotContainer != null) {

            GameObject go = Instantiate(slotContainer.Item.prefab);

            //go.transform.position = transform.position;
            //go.transform.rotation = transform.rotation;
            go.name = slotContainer.Item.prefab.name;
            go.transform.SetParent(player.EquipmentPoint.transform);

        }

    }

    private SlotContainer getSlotContainer(int HotbarID) {

        for (int i = 0; i < transform.GetChild(0).childCount; i++) {
            if (transform.GetChild(0).GetChild(i).childCount != 0) {

                SlotContainer slotContainer = transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<SlotContainer>();
                return slotContainer;
            }
            else {
                return null;
            }
        }
        return null;
    }


}
