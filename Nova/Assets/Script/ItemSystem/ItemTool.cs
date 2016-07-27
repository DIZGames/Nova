using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.ItemSystem {

    [System.Serializable]
    public class ItemTool : ItemBase {

        //Fix
        public int BulletSpeed;
        public int FireRate;
        public ItemAmmo Ammo;
    }
}
