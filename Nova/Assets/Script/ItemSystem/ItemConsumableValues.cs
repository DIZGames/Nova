using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.ItemSystem {
    public class ItemConsumableValues : ItemValues {
        public int RestoreHealth {
            get {
                return ((ItemConsumable)base.itemBase).restoreHealth;
            }
        }
        public int RestoreArmor{
            get {
                return ((ItemConsumable)base.itemBase).restoreArmor;
            }
        }
        public int RestoreEnergy{
            get {
                return ((ItemConsumable)base.itemBase).restoreEnergy;
            }
        }
        public int RestoreOxygen{
            get {
                return ((ItemConsumable)base.itemBase).restoreOxygen;
            }
        }
    }
}
