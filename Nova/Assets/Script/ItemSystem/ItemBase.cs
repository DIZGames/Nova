using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.ItemSystem;

[System.Serializable]
public class ItemBase : ScriptableObject{

    public string name;
    public int maxStack;
    public ItemType type;
    public Sprite icon;
    public GameObject prefab;
}
