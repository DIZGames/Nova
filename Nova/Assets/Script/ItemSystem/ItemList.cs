using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.ItemSystem;

public class ItemList : ScriptableObject {

    [SerializeField]
    private List<ItemBase> itemList;

    public ItemBase getItemByName(string Name) {
        for (int i = 0; i < itemList.Count; i++) {
            if (Name == itemList[i].name) {
                return itemList[i];
            }
        }

        return null;
    }

    public ItemBase getItemByIndex(int index)
    {
        return itemList[index];
    }

    public int getCount() {
        return itemList.Count;
    }



}
