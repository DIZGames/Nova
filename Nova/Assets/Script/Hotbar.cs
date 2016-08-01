using UnityEngine;
using System.Collections;
using Assets.Script;
using System;
using Assets.Script.ItemSystem;
using System.Collections.Generic;

public class Hotbar : MonoBehaviour, InventoryInterface {

    public Player player;
    public InterfaceManager interfaceManager;

    private List<SlotContainer> hotBarList;

    void Start() {
        hotBarList = new List<SlotContainer>();
        UpdateList();
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

    private void selectHotbar(int HotbarID) {

        SlotContainer slotContainer = getSlotContainer(HotbarID);

        player.clearOnEquipment();
        interfaceManager.showWeaponStat(false);

        if (slotContainer != null && slotContainer.Item != null) {

            if (slotContainer.Item.Type == ItemType.Tool) {
                GameObject go = Instantiate(slotContainer.Item.Prefab);

                go.transform.position = player.EquipmentPoint.transform.position;
                go.transform.rotation = player.EquipmentPoint.transform.rotation;

                go.name = slotContainer.Item.Name;

             
                go.GetComponent<ToolLogic>().setItemValues(slotContainer.Item);

                player.setOnEquipment(go);

                interfaceManager.setItemToolOnWeaponStat(slotContainer.Item);
                interfaceManager.showWeaponStat(true);
            }
            if (slotContainer.Item.Type == ItemType.Consumable) {

            }
            if (slotContainer.Item.Type == ItemType.Block) {

            }
        }

    }

    private SlotContainer getSlotContainer(int hotbarID) {

        Transform slot = transform.GetChild(0).GetChild(hotbarID);
        if (slot.childCount > 0)
            return slot.GetChild(0).GetComponent<SlotContainer>();
        else
            return null;
    }

    public void UpdateList() {
        hotBarList.Clear();

        for (int i = 0; i < transform.GetChild(0).childCount; i++) {
            if (transform.GetChild(0).GetChild(i).childCount != 0) {
                hotBarList.Add(transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<SlotContainer>());
            }
        }

        Debug.Log("hotbarList "+hotBarList.Count);
    }

    public int Count(string name) {
        int count = 0;

        for (int i = 0; i < hotBarList.Count; i++) {
            if (name == hotBarList[i].Item.Name) {
                count += hotBarList[i].Item.stack;
            }
        }
        return count;
    }

    public bool ReduceStackOne(string name) {
        for (int i = 0; i < hotBarList.Count; i++) {
            if (name == hotBarList[i].Item.Name && hotBarList[i].Item.stack != 0) {
                hotBarList[i].Item.stack--;
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
