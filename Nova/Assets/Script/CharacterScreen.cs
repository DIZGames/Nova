using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Assets.Script.ItemSystem;
using Assets.Script.Interface;
using Assets.Script;

public class CharacterScreen : MonoBehaviour, ISlotContainerList, IUI {

    private List<SlotContainer> characterScreenList;
    public Transform slotList;

    public Player player;

    [SerializeField]
    private GameObject uiObject;

    private Transform standardParent;

    // Use this for initialization
    void Start() {
        standardParent = transform.parent;
        characterScreenList = new List<SlotContainer>();
        UpdateList();
    }

    // Update is called once per frame
    void Update() {

    }

    public void UpdateList() {
        characterScreenList.Clear();
        player.resetMaxValues();

        for (int i = 0; i < slotList.childCount; i++) {
            if (slotList.GetChild(i).childCount != 0) {
                SlotContainer slotContainer = slotList.GetChild(i).GetChild(0).GetComponent<SlotContainer>();
                ItemClothing itemClothing = (ItemClothing)slotContainer.ItemBase;

                player.addToMaxValues(itemClothing.healthUpgrade, itemClothing.armorUpgrade, itemClothing.energyUpgrade, itemClothing.oxygenUpgrade);

                characterScreenList.Add(slotContainer);
            }
        }
        Debug.Log("characterScreenList " + characterScreenList.Count);
    }

    public int Count(string itemName) {
        int count = 0;

        for (int i = 0; i < characterScreenList.Count; i++) {
            if (itemName == characterScreenList[i].ItemBase.itemName) {
                count += characterScreenList[i].ItemBase.stack;
            }
        }
        return count;
    }

    public int Decrease(string itemName, int count) {
        for (int i = 0; i < characterScreenList.Count; i++) {
            if (itemName == characterScreenList[i].ItemBase.itemName && characterScreenList[i].ItemBase.stack != 0) {
                if (characterScreenList[i].ItemBase.stack >= count) {
                    characterScreenList[i].ItemBase.stack -= count;
                    return 0;
                }
                else {
                    count = count - characterScreenList[i].ItemBase.stack;
                    characterScreenList[i].ItemBase.stack = 0;
                }
            }
        }
        return count;
    }

    public void Add(SlotContainer slotContainer) {

        for (int i = 0; i < slotList.childCount; i++) {
                 
            SlotDrop slotDrop = slotList.GetChild(i).GetComponent<SlotDrop>();

            if (slotDrop.checkAllowedTypes(slotContainer)) {

                Transform parentSC = slotContainer.transform.parent;

                Transform parent = slotDrop.transform;

                if (slotDrop.transform.childCount != 0) {
                    slotDrop.transform.GetChild(0).SetParent(parentSC);
                }

                slotContainer.transform.SetParent(parent);

                UpdateList();      
            }     
        }
    }

    public void Hide() {
        uiObject.SetActive(false);
    }

    public bool IsActive() {
        return uiObject.activeSelf;
    }

    public void Move(Transform transform) {
        this.transform.SetParent(transform);
        this.transform.position = transform.position;
        this.transform.rotation = transform.rotation;
    }

    public void Show() {
        uiObject.SetActive(true);
    }

    public void ResetPosition() {
        Move(standardParent);
        uiObject.SetActive(false);
    }

    public bool FreeSlot() {
        for (int i = 0; i < slotList.childCount; i++) {
            if (slotList.GetChild(i).childCount == 0) {
                return true;
            }
        }
        return false;
    }

    public void AddToShipManager(ShipManager shipManager) {
        throw new NotImplementedException();
    }
}
