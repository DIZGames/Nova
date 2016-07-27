using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.ItemSystem;

[System.Serializable]
public class ItemBase {

    public string itemName;
    public int maxStack;
    public ItemType type;
    public Sprite icon;
    public GameObject prefab;
}
