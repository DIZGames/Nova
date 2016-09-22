using UnityEngine;
using Assets.Script.ItemSystem;
using Assets.Script;
using System;
using System.Collections.Generic;

public class Block : MonoBehaviour, IHasItem
{
    [SerializeField]
    private ItemBase itemBase; 

    //[SerializeField]
    //private List<IHasItem> listIHasItem;

    public void SetItem(ItemBase itemBase)
    {
        this.itemBase = itemBase;

        throw new NotImplementedException();
    }
}
