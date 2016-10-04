using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.ItemSystem {
    public class ItemConsumable : ItemBase{

        public int restoreHealth;
        public int restoreArmor;
        public int restoreEnergy;
        public int restoreOxygen;

        public override ItemBase Clone() {
            return Instantiate(this) as ItemConsumable;
        }
    }
}
