
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.ItemSystem {
    public class ItemBlock : ItemBase{

        public BlockPosition position;
        public bool createsNewShip; 
        public int maxHitPoints;
        public int Energy;
        public int Oxygen;

        public int currentHitPoints;

        public override ItemBase Clone() {
            return Instantiate(this) as ItemBlock;
        }
    }
}
