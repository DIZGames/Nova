using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.ItemSystem {
    public class ItemMaterial : ItemBase{
        public MaterialType materialType;

        public override ItemBase Clone() {
            return Instantiate(this) as ItemMaterial;
        }
    }
}
