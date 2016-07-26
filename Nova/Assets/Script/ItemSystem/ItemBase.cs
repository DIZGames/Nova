using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.ItemSystem;

public abstract class ItemBase : ScriptableObject{

    public string Name;
    public int MaxStack;
    public ItemType Type;
    public Sprite Icon;
    public GameObject Prefab;
}
