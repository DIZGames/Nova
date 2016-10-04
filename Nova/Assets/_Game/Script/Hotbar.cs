using UnityEngine;
using System.Collections;
using Assets.Script;
using System;
using Assets.Script.ItemSystem;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Script.Interface;
using UnityEngine.EventSystems;

public class Hotbar : MonoBehaviour, ISlotContainerList, IUI {

    private Player _player;
    public InterfaceManager interfaceManager;

    public GameObject blockBuilderPrefab;
    private Transform standardParent;

    [SerializeField]
    private Transform slotList;
    [SerializeField]
    private GameObject uiObject;

    private Transform selectedSlot;
    public IEquippable selectedGameObject;
    private List<SlotContainer> hotBarList;

    public bool IsActive {
        get {
            return uiObject.activeSelf;
        }
    }

    public ISlotContainerList ISlotContainerList {
        get {
            return this;
        }
    }

    void Start() {
        hotBarList = new List<SlotContainer>();
        Refresh();
        standardParent = transform.parent;
    }

    void Update() {
       
        if (Input.GetButtonDown("Hotbar1")) {
            selectHotbarID(0);
        }
        if (Input.GetButtonDown("Hotbar2")) {
            selectHotbarID(1);
        }
        if (Input.GetButtonDown("Hotbar3")) {
            selectHotbarID(2);
        }
        if (Input.GetButtonDown("Hotbar4")) {
            selectHotbarID(3);
        }
        if (Input.GetButtonDown("Hotbar5")) {
            selectHotbarID(4);
        }
        if (Input.GetButtonDown("Hotbar6")) {
            selectHotbarID(5);
        }
        if (Input.GetButtonDown("Hotbar7")) {
            selectHotbarID(6);
        }
        if (Input.GetButtonDown("Hotbar8")) {
            selectHotbarID(7);
        }
        if (Input.GetButtonDown("Hotbar9")) {
            selectHotbarID(8);
        }
        if (Input.GetButtonDown("Hotbar0")) {
            selectHotbarID(9);
        }

        if (!EventSystem.current.IsPointerOverGameObject()) {
            if (Input.GetButtonDown("Fire1")) {
                if (selectedGameObject != null)
                    selectedGameObject.Action1();
            }

            if (Input.GetButtonDown("Fire2")) {
                if (selectedGameObject != null)
                    selectedGameObject.Action2();
            }
        }

        if (Input.GetButtonDown("Reload")) {
            if (selectedGameObject != null)
                selectedGameObject.Action3();
        }
    }

    private void EquipSlotContainer(SlotContainer slotContainer) {
        Player.clearOnEquipment();
        interfaceManager.showWeaponStat(false);
        selectedGameObject = null;

        if (slotContainer != null && slotContainer.ItemBase != null) {

            if (slotContainer.ItemBase.type == ItemType.Tool) {
                GameObject go = Instantiate(slotContainer.ItemBase.prefab);

                go.transform.position = Player.EquipmentPoint.transform.position;
                go.transform.rotation = Player.EquipmentPoint.transform.rotation;
                go.name = slotContainer.ItemBase.itemName;

                IHasItem iHasItem = go.GetComponent<IHasItem>();
                iHasItem.SetItem(slotContainer.ItemBase);
                selectedGameObject = go.GetComponent<IEquippable>();

                //IEquippable iEquippable = go.GetComponent<IEquippable>();
                //iEquippable.SetItem(slotContainer.ItemBase);
                //selectedGameObject = iEquippable;

                Player.setOnEquipment(go);

                interfaceManager.SetItemTool(slotContainer.ItemBase);
                interfaceManager.showWeaponStat(true);
            }
            else if (slotContainer.ItemBase.type == ItemType.Consumable) {
                GameObject go = Instantiate(slotContainer.ItemBase.prefab);

                go.transform.position = Player.EquipmentPoint.transform.position;
                go.transform.rotation = Player.EquipmentPoint.transform.rotation;
                go.name = slotContainer.ItemBase.itemName;

                IHasItem iHasItem = go.GetComponent<IHasItem>();
                iHasItem.SetItem(slotContainer.ItemBase);
                selectedGameObject = go.GetComponent<IEquippable>();

                //IEquippable iEquippable = go.GetComponent<IEquippable>();
                //iEquippable.SetItem(slotContainer.ItemBase);
                //selectedGameObject = iEquippable;

                Player.setOnEquipment(go);
            }
            else if (slotContainer.ItemBase.type == ItemType.Block) {
                GameObject go = Instantiate(blockBuilderPrefab);

                go.transform.position = Player.transform.position;
                go.transform.rotation = Player.transform.rotation;

                IHasItem iHasItem = go.GetComponent<IHasItem>();
                iHasItem.SetItem(slotContainer.ItemBase);
                selectedGameObject = go.GetComponent<IEquippable>();

                //IEquippable iEquippable = go.GetComponent<IEquippable>();
                //iEquippable.SetItem(slotContainer.ItemBase);
                //selectedGameObject = iEquippable;

                Player.setOnEquipment(go);
            }
        }
    }

    private void selectHotbarID(int HotbarID) {
        Transform slot = GetSlot(HotbarID);
        selectedSlot = slot;
        SlotContainer slotContainer = selectSlotContainer(slot);
        EquipSlotContainer(slotContainer);
    }

    public Transform GetSlot(int hotbarID) {        
        return slotList.GetChild(hotbarID);
    }

    private SlotContainer selectSlotContainer(Transform slot) {     

        for (int i = 0; i < slotList.childCount; i++) {
            if (slotList.GetChild(i).childCount != 0) {
                ToggleScriptOfSlotContainer(slotList.GetChild(i).GetChild(0), true);
            }
            slotList.GetChild(i).GetComponent<Image>().color = Color.white;
        }

        slot.GetComponent<Image>().color = Color.cyan;

        if (slot.childCount != 0) {

            GameObject gameObject = slot.GetChild(0).gameObject;

            ToggleScriptOfSlotContainer(gameObject.transform, false);

            return gameObject.GetComponent<SlotContainer>();
        }           
        else
            return null;
    }

    private void ToggleScriptOfSlotContainer(Transform slotContainer, bool flag) {
        slotContainer.GetComponent<SlotContainerDrag>().enabled = flag;
        slotContainer.GetComponent<SlotContainerSplit>().enabled = flag;
        slotContainer.GetComponent<SlotLefftClick>().enabled = flag;
        slotContainer.GetComponent<SlotContainerTooltip>().enabled = flag;
    }

    public void Refresh() {
        hotBarList.Clear();

        for (int i = 0; i < slotList.childCount; i++) {
            if (slotList.GetChild(i).childCount != 0) {
                hotBarList.Add(slotList.GetChild(i).GetChild(0).GetComponent<SlotContainer>());
            }
        }
    }

    public int Count(string itemName) {
        int count = 0;

        for (int i = 0; i < hotBarList.Count; i++) {
            if (itemName == hotBarList[i].ItemBase.itemName) {
                count += hotBarList[i].ItemBase.stack;
            }
        }
        return count;
    }

    public int Remove(string itemName, int count) {
        for (int i = 0; i < hotBarList.Count; i++) {
            if (itemName == hotBarList[i].ItemBase.itemName && hotBarList[i].ItemBase.stack != 0) {
                if (hotBarList[i].ItemBase.stack >= count) {
                    hotBarList[i].ItemBase.stack -= count;
                    return 0;
                }
                else {
                    count = count - hotBarList[i].ItemBase.stack;
                    hotBarList[i].ItemBase.stack = 0;
                }
            }
        }
        return count;
    }

    public void Hide() {

    }

    public void Move(Transform transform) {
        this.transform.SetParent(transform);
    }

    public void Show() {

    }

    public void ResetPosition() {
        Move(standardParent);
    }

    public bool TryAdd(SlotContainer slotContainer) {
        if (selectedSlot != null) {
            SlotDrop slotDrop = selectedSlot.GetComponent<SlotDrop>();

            if (slotDrop.checkAllowedTypes(slotContainer)) {
                Transform slotContainerParent = slotContainer.transform.parent;
                Transform currentContainer;

                if (slotDrop.transform.childCount != 0) {
                    currentContainer = slotDrop.transform.GetChild(0);
                    ToggleScriptOfSlotContainer(currentContainer, true);
                    currentContainer.SetParent(slotContainerParent);
                }


                slotContainer.transform.SetParent(slotDrop.transform);
                ToggleScriptOfSlotContainer(slotContainer.transform, false);
                EquipSlotContainer(slotContainer);

                return true;
            }
        }

        return false;

    }

    public Player Player
    {
        set { _player = (Player)value; }
        get { return _player; }
    }
}
