using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Assets.Script.ItemSystem;
using Assets.Script.Interface;

public class CharacterScreen : MonoBehaviour, ISlotContainerList {

    private List<SlotContainer> characterScreenList;
    public Transform slotList;

    public Player player;

    // Use this for initialization
    void Start() {
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
                ItemClothingValues itemClothingValues = (ItemClothingValues)slotContainer.Item;

                player.addToMaxValues(itemClothingValues.healthUpgrade, itemClothingValues.armorUpgrade, itemClothingValues.energyUpgrade, itemClothingValues.oxygenUpgrade);

                characterScreenList.Add(slotContainer);
            }
        }
        Debug.Log("characterScreenList " + characterScreenList.Count);
    }

    public int Count(string itemName) {
        int count = 0;

        for (int i = 0; i < characterScreenList.Count; i++) {
            if (itemName == characterScreenList[i].Item.Name) {
                count += characterScreenList[i].Item.stack;
            }
        }
        return count;
    }

    public int Decrease(string itemName, int count) {
        for (int i = 0; i < characterScreenList.Count; i++) {
            if (itemName == characterScreenList[i].Item.Name && characterScreenList[i].Item.stack != 0) {
                if (characterScreenList[i].Item.stack >= count) {
                    characterScreenList[i].Item.stack -= count;
                    return 0;
                }
                else {
                    count = count - characterScreenList[i].Item.stack;
                    characterScreenList[i].Item.stack = 0;
                }
            }
        }
        return count;
    }
}
