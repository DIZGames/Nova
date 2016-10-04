using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.ItemSystem;

[System.Serializable]
public abstract class ItemBase : ScriptableObject{

    public string itemName;
    public int stack;
    public int maxStack;
    public ItemType type;
    public Sprite icon;
    public GameObject prefab;
    public string description;

    public abstract ItemBase Clone();

}
