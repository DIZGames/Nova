using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.ItemSystem
{
    //[System.Serializable]
    public class ItemValues : ScriptableObject
    {
        public ItemBase itemBase;
        public int stack;

        public ItemValues(ItemBase itemBase) {
            this.itemBase = itemBase;
        }

        public string Name
        {
            get
            {
                return itemBase.name;
            }
        }

        public int MaxStack
        {
            get
            {
                return itemBase.maxStack;
            }
        }

        public Sprite Icon
        {
            get
            {
                return itemBase.icon;
            }
        }
        public ItemType Type
        {
            get
            {
                return itemBase.type;
            }
        }

        public GameObject Prefab
        {
            get
            {
                return itemBase.prefab;
            }
        }

    }
}
