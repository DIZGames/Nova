﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.ItemSystem {
    public class ItemClothing : ItemBase{

        public int healthUpgrade;
        public int armorUpgrade;
        public int energyUpgrade;
        public int oxygenUpgrade;

        public ItemTypeClothing clothingType;

        public override ItemBase Clone() {
            return Instantiate(this) as ItemClothing;
        }
    }
}
