using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.ItemSystem {

    [System.Serializable]
    public class ItemAmmo : ItemBase{
        public int ClipSize;
        public int Damage;
    }
}
