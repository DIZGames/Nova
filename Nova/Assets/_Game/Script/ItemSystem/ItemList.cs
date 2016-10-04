using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.ItemSystem;

public class ItemList : ScriptableObject {

    [SerializeField]
    private List<ItemTool> listItemTool;
    [SerializeField]
    private List<ItemAmmo> listItemAmmo;
    [SerializeField]
    private List<ItemMaterial> listItemMaterial;
    [SerializeField]
    private List<ItemBlock> listItemBlock;
    [SerializeField]
    private List<ItemClothing> listItemClothing;
    [SerializeField]
    private List<ItemConsumable> listItemConsumable;

    public ItemBase ItemByName(string Name) {
        for (int i = 0; i < listItemTool.Count; i++)
        {
            if (Name == listItemTool[i].name)
            {
                ItemBase itemBase = listItemTool[i].Clone();
                itemBase.itemName = listItemTool[i].itemName;
                return itemBase;
            }
        }
        for (int i = 0; i < listItemAmmo.Count; i++)
        {
            if (Name == listItemAmmo[i].name)
            {
                ItemBase itemBase = listItemAmmo[i].Clone();
                itemBase.itemName = listItemAmmo[i].itemName;
                return itemBase;
            }
        }
        for (int i = 0; i < listItemMaterial.Count; i++)
        {
            if (Name == listItemMaterial[i].name)
            {
                ItemBase itemBase = listItemMaterial[i].Clone();
                itemBase.itemName = listItemMaterial[i].itemName;
                return itemBase;
            }
        }
        for (int i = 0; i < listItemBlock.Count; i++)
        {
            if (Name == listItemBlock[i].name)
            {
                ItemBase itemBase = listItemBlock[i].Clone();
                itemBase.itemName = listItemBlock[i].itemName;
                return itemBase;
            }
        }
        for (int i = 0; i < listItemClothing.Count; i++)
        {
            if (Name == listItemClothing[i].name)
            {
                ItemBase itemBase = listItemClothing[i].Clone();
                itemBase.itemName = listItemClothing[i].itemName;
                return itemBase;
            }
        }
        for (int i = 0; i < listItemConsumable.Count; i++)
        {
            if (Name == listItemConsumable[i].name)
            {
                ItemBase itemBase = listItemConsumable[i].Clone();
                itemBase.itemName = listItemConsumable[i].itemName;
                return itemBase;
            }
        }
        return null;
    }

    public ItemBase ItemByIndex(int index)
    {

        ItemBase itemBase = null;

        int c1 = listItemTool.Count;
        int c2 = listItemTool.Count + listItemAmmo.Count;
        int c3 = listItemTool.Count + listItemAmmo.Count + listItemMaterial.Count;
        int c4 = listItemTool.Count + listItemAmmo.Count + listItemMaterial.Count + listItemBlock.Count;
        int c5 = listItemTool.Count + listItemAmmo.Count + listItemMaterial.Count + listItemBlock.Count + listItemClothing.Count;
        int c6 = listItemTool.Count + listItemAmmo.Count + listItemMaterial.Count + listItemBlock.Count + listItemClothing.Count + listItemConsumable.Count;

        if (index < c1) {
            itemBase = listItemTool[index].Clone();
            itemBase.itemName = listItemTool[index].itemName;
        }
        if (index >= c1 && index < c2) {
            itemBase = listItemAmmo[index- c1].Clone();
            itemBase.itemName = listItemAmmo[index - c1].itemName;
        }
        if (index >= c2 && index < c3)
        {
            itemBase = listItemMaterial[index - c2].Clone();
            itemBase.itemName = listItemMaterial[index - c2].itemName;
        }
        if (index >= c3 && index < c4)
        {
            itemBase = listItemBlock[index - c3].Clone();
            itemBase.itemName = listItemBlock[index - c3].itemName;
        }
        if (index >= c4 && index < c5)
        {
            itemBase = listItemClothing[index - c4].Clone();
            itemBase.itemName = listItemClothing[index - c4].itemName;
        }
        if (index >= c5 && index < c6)
        {
            itemBase = listItemConsumable[index - c5].Clone();
            itemBase.itemName = listItemConsumable[index - c5].itemName;
        }

        return itemBase;
    }

    public string ItemDescByIndex(int index)
    {
        string itemName = null;

        int c1 = listItemTool.Count;
        int c2 = listItemTool.Count + listItemAmmo.Count;
        int c3 = listItemTool.Count + listItemAmmo.Count + listItemMaterial.Count;
        int c4 = listItemTool.Count + listItemAmmo.Count + listItemMaterial.Count + listItemBlock.Count;
        int c5 = listItemTool.Count + listItemAmmo.Count + listItemMaterial.Count + listItemBlock.Count + listItemClothing.Count;
        int c6 = listItemTool.Count + listItemAmmo.Count + listItemMaterial.Count + listItemBlock.Count + listItemClothing.Count + listItemConsumable.Count;

        if (index < c1)
        {
            itemName = "[Tool] " + listItemTool[index].itemName;
        }
        if (index >= c1 && index < c2)
        {
            itemName = "[Ammo] " + listItemAmmo[index - c1].itemName;
        }
        if (index >= c2 && index < c3)
        {
            itemName = "[Material] " + listItemMaterial[index - c2].itemName;
        }
        if (index >= c3 && index < c4)
        {
            itemName = "[Block] " + listItemBlock[index - c3].itemName;
        }
        if (index >= c4 && index < c5)
        {
            itemName = "[Clothing] | " + listItemClothing[index - c4].itemName;
        }
        if (index >= c5 && index < c6)
        {
            itemName = "[Consumable] | " + listItemConsumable[index - c5].itemName;
        }

        return itemName;
    }

    public int Count() {
        int count = listItemTool.Count + listItemAmmo.Count + listItemMaterial.Count + listItemBlock.Count + listItemClothing.Count + listItemConsumable.Count;
        return count;
    }

}
