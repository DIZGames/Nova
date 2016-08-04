using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.ItemSystem {
    public class ItemBlock : ItemBase{
        public BlockPosition position;
        public bool createsNewShip; // if true ShipPartPosiion should be set to Center
        public int maxHitPoints;
    }
}
