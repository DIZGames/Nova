using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Assets.Script.ItemSystem;

public class CharacterScreen : MonoBehaviour, InventoryInterface {

    private List<SlotContainer> characterScreenList;

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

        for (int i = 0; i < transform.GetChild(0).childCount; i++) {
            if (transform.GetChild(0).GetChild(i).childCount != 0) {
                SlotContainer slotContainer = transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<SlotContainer>();
                ItemClothingValues itemClothingValues = (ItemClothingValues)slotContainer.Item;

                player.addToMaxValues(itemClothingValues.healthUpgrade, itemClothingValues.armorUpgrade, itemClothingValues.energyUpgrade, itemClothingValues.oxygenUpgrade);

                characterScreenList.Add(slotContainer);
            }
        }
        Debug.Log("characterScreenList " + characterScreenList.Count);
    }

    public int Count(string name) {
        int count = 0;

        for (int i = 0; i < characterScreenList.Count; i++) {
            if (name == characterScreenList[i].Item.Name) {
                count += characterScreenList[i].Item.stack;
            }
        }
        return count;
    }

    public bool ReduceStackOne(string name) {
        for (int i = 0; i < characterScreenList.Count; i++) {
            if (name == characterScreenList[i].Item.Name && characterScreenList[i].Item.stack != 0) {
                characterScreenList[i].Item.stack--;
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
