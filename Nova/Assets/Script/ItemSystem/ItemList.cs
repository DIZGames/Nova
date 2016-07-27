using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.ItemSystem;

public class ItemList : ScriptableObject {

    [SerializeField]
    private List<ItemTool> toolList;

    [SerializeField]
    private List<ItemAmmo> ammoList;

    public ItemBase getItemByName(string Name) {
        for (int i = 0; i < toolList.Count; i++)
        {
            if (Name == toolList[i].itemName)
            {
                return toolList[i];
            }
        }
        for (int i = 0; i < ammoList.Count; i++)
        {
            if (Name == ammoList[i].itemName)
            {
                return ammoList[i];
            }
        }

        return null;
    }

    public ItemBase getToolByIndex(int index)
    {
        return toolList[index];
    }
    public ItemBase getAmmoByIndex(int index)
    {
        return ammoList[index];
    }

    public int getCount() {
        return toolList.Count + ammoList.Count;
    }

    public int getToolsCount()
    {
        return toolList.Count;
    }

    public int getAmmoCount()
    {
        return ammoList.Count;
    }

}
