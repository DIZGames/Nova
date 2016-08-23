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
        //Changeable
        public int loadedProjectiles;
        public int inStock;

        public override ItemBase Clone() {
            return Instantiate(this) as ItemTool;
        }
    }
}
