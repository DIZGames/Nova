using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.ItemSystem;

public class ItemList : ScriptableObject {

    [SerializeField]
    private List<ItemBase> itemList;

    public ItemBase ItemByName(string Name) {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (Name == itemList[i].name)
            {
                ItemBase itemBase = itemList[i].Clone();
                itemBase.itemName = itemList[i].itemName;
                return itemBase;
            }
        }
        return null;
    }

    public ItemBase ItemByIndex(int index)
    {
        ItemBase itemBase = itemList[index].Clone();
        itemBase.itemName = itemList[index].itemName;
        return itemBase;

    }

    public int Count() {
        return itemList.Count;
    }


}
