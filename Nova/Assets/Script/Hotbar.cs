using UnityEngine;
using System.Collections;
using Assets.Script;
using System;
using Assets.Script.ItemSystem;
using System.Collections.Generic;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour, InventoryInterface {

    public Player player;
    public InterfaceManager interfaceManager;
    public GameObject blockBuilderPrefab;

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

        for (int i = 0; i < transform.GetChild(0).childCount; i++) {
            if (transform.GetChild(0).GetChild(i).childCount > 0) {
                transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<SlotContainerDrag>().enabled = true;
                transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<SlotContainerSplit>().enabled = true;
                transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().color = Color.white;
            }
        }

        Transform slot = transform.GetChild(0).GetChild(hotbarID);

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
