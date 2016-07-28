using UnityEngine;
using System.Collections;
using Assets.Script;
using System;
using Assets.Script.ItemSystem;

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

        if (player.EquipmentPoint.transform.childCount != 0)
            Destroy(player.EquipmentPoint.transform.GetChild(0).gameObject);

        if (slotContainer != null && slotContainer.Item != null) {


            GameObject go = Instantiate(slotContainer.Item.Prefab);

            go.transform.position = player.EquipmentPoint.transform.position;
            go.transform.rotation = player.EquipmentPoint.transform.rotation;

            go.name = slotContainer.Item.Name;

            go.transform.SetParent(player.EquipmentPoint.transform);
            go.GetComponent<ToolLogic>().setItemValues(slotContainer.Item);

        }

    }

    private SlotContainer getSlotContainer(int hotbarID) {

        Transform slot = transform.GetChild(0).GetChild(hotbarID);
        if (slot.childCount > 0)
            return slot.GetChild(0).GetComponent<SlotContainer>();
        else
            return null;
    }


}
