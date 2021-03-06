﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Assets.Script.ItemSystem;
using Assets.Script.Interface;
using Assets.Script;

public class CharacterScreen : MonoBehaviour, ISlotContainerList, IUI {

    private List<SlotContainer> characterScreenList;
    public Transform slotList;

    private Player _player;

    [SerializeField]
    private GameObject uiObject;

    private Transform standardParent;

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

    // Use this for initialization
    void Start() {
        standardParent = transform.parent;
        characterScreenList = new List<SlotContainer>();
        Refresh();
    }

    public void Refresh() {
        if(Player != null && characterScreenList != null)
        {
            characterScreenList.Clear();
            Player.resetMaxValues();

            for (int i = 0; i < slotList.childCount; i++) {
                if (slotList.GetChild(i).childCount != 0) {
                    SlotContainer slotContainer = slotList.GetChild(i).GetChild(0).GetComponent<SlotContainer>();
                    ItemClothing itemClothing = (ItemClothing)slotContainer.ItemBase;

                    Player.addToMaxValues(itemClothing.healthUpgrade, itemClothing.armorUpgrade, itemClothing.energyUpgrade, itemClothing.oxygenUpgrade);

                    characterScreenList.Add(slotContainer);
                }
            }
        }
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

    public int Remove(string itemName, int count) {
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

    public void Hide() {
        uiObject.SetActive(false);
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

    public bool TryAdd(SlotContainer slotContainer) {
        for (int i = 0; i < slotList.childCount; i++) {

            SlotDrop slotDrop = slotList.GetChild(i).GetComponent<SlotDrop>();

            if (slotDrop.checkAllowedTypes(slotContainer)) {

                Transform parentSC = slotContainer.transform.parent;

                Transform parent = slotDrop.transform;

                if (slotDrop.transform.childCount != 0) {
                    slotDrop.transform.GetChild(0).SetParent(parentSC);
                }

                slotContainer.transform.SetParent(parent);
                return true;
            }
        }
        return false;
    }

    public Player Player
    {
        set
        {
            _player = value;
            Refresh();
        }
        get { return _player; }
    }
}
