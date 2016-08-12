using UnityEngine;
using System.Collections;
using Assets.Script;
using System;
using Assets.Script.ItemSystem;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Script.Interface;

public class Hotbar : MonoBehaviour, ISlotContainerList {

    public Player player;
    public InterfaceManager interfaceManager;

    public GameObject blockBuilderPrefab;

    public Transform slotList;


    public IEquippable selectedGameObject;

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

        if (Input.GetButtonDown("Fire1")) {
            if (selectedGameObject != null)
                selectedGameObject.Action1();
        }

        if (Input.GetButtonDown("Fire2")) {
            if (selectedGameObject != null)
                selectedGameObject.Action2();
        }

        if (Input.GetButtonDown("Reload")) {
            if (selectedGameObject != null)
                selectedGameObject.Action3();
        }
    }

    private void selectHotbar(int HotbarID) {

        SlotContainer slotContainer = selectSlotContainer(HotbarID);

        player.clearOnEquipment();
        interfaceManager.showWeaponStat(false);
        selectedGameObject = null;

        if (slotContainer != null && slotContainer.Item != null) {        

            if (slotContainer.Item.Type == ItemType.Tool) {
                GameObject go = Instantiate(slotContainer.Item.Prefab);

                go.transform.position = player.EquipmentPoint.transform.position;
                go.transform.rotation = player.EquipmentPoint.transform.rotation;
                go.name = slotContainer.Item.Name;

                IEquippable iEquippable = go.GetComponent<IEquippable>();
                iEquippable.setItemValues(slotContainer.Item);
                selectedGameObject = iEquippable;

                player.setOnEquipment(go);

                interfaceManager.setItemToolOnWeaponStat(slotContainer.Item);
                interfaceManager.showWeaponStat(true);
            }
            else if (slotContainer.Item.Type == ItemType.Consumable) {
                GameObject go = Instantiate(slotContainer.Item.Prefab);

                go.transform.position = player.EquipmentPoint.transform.position;
                go.transform.rotation = player.EquipmentPoint.transform.rotation;
                go.name = slotContainer.Item.Name;

                IEquippable iEquippable = go.GetComponent<IEquippable>();
                iEquippable.setItemValues(slotContainer.Item);
                selectedGameObject = iEquippable;

                player.setOnEquipment(go);

            }
            else if (slotContainer.Item.Type == ItemType.Block) {
                GameObject blockBuilder = Instantiate(blockBuilderPrefab);
                GameObject buildingPoint = blockBuilder.transform.FindChild("BlockBuildingPoint").gameObject;

                float spawnDistance = 1f;

                blockBuilder.transform.position = player.transform.position + player.transform.up * spawnDistance;
                blockBuilder.transform.rotation = player.transform.rotation;

                EquippedBlockLogic iEquippable = buildingPoint.GetComponent<EquippedBlockLogic>();
                iEquippable.init();
                iEquippable.setItemValues(slotContainer.Item);

                ItemBlockValues itemBlockValues = (ItemBlockValues)slotContainer.Item;
                itemBlockValues.currentHitPoints = itemBlockValues.MaxHitPoints;

                selectedGameObject = iEquippable;

                SpriteRenderer sr = iEquippable.dummyBlock.GetComponent<SpriteRenderer>();
                sr.sprite = slotContainer.Item.Prefab.GetComponent<SpriteRenderer>().sprite;

                player.setOnEquipment(blockBuilder);

            }
        }
    }

    private SlotContainer selectSlotContainer(int hotbarID) {

        for (int i = 0; i < slotList.childCount; i++) {
            if (slotList.GetChild(i).childCount > 0) {
                slotList.GetChild(i).GetChild(0).GetComponent<SlotContainerDrag>().enabled = true;
                slotList.GetChild(i).GetChild(0).GetComponent<SlotContainerSplit>().enabled = true;
                slotList.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.white;
            }
        }

        Transform slot = slotList.GetChild(hotbarID);

        if (slot.childCount > 0) {

            GameObject gameObject = slot.GetChild(0).gameObject;

            gameObject.GetComponent<SlotContainerDrag>().enabled = false;
            gameObject.GetComponent<SlotContainerSplit>().enabled = false;
            gameObject.GetComponent<Image>().color = Color.cyan;

            return gameObject.GetComponent<SlotContainer>();
        }
            
        else
            return null;
    }

    public void UpdateList() {
        hotBarList.Clear();

        for (int i = 0; i < slotList.childCount; i++) {
            if (slotList.GetChild(i).childCount != 0) {
                hotBarList.Add(slotList.GetChild(i).GetChild(0).GetComponent<SlotContainer>());
            }
        }

        Debug.Log("hotBarList " + hotBarList.Count);
    }

    public int Count(string itemName) {
        int count = 0;

        for (int i = 0; i < hotBarList.Count; i++) {
            if (itemName == hotBarList[i].Item.Name) {
                count += hotBarList[i].Item.stack;
            }
        }
        return count;
    }

    public int Decrease(string itemName, int count) {
        for (int i = 0; i < hotBarList.Count; i++) {
            if (itemName == hotBarList[i].Item.Name && hotBarList[i].Item.stack != 0) {
                if (hotBarList[i].Item.stack >= count) {
                    hotBarList[i].Item.stack -= count;
                    return 0;
                }
                else {
                    count = count - hotBarList[i].Item.stack;
                    hotBarList[i].Item.stack = 0;
                }
            }
        }
        return count;
    }
}
