using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.ItemSystem {
    public class ItemBlockValues : ItemValues {

        private ItemBlock itemBlock;
        public int currentHitPoints;

        public ItemBlockValues(ItemBase itemBase)
            : base(itemBase)
        {
        }
        

        public BlockPosition BlockPosition { get { return ((ItemBlock)itemBase).position; } }
        public bool CreatesNewShip { get { return ((ItemBlock)itemBase).createsNewShip; } }
        public int MaxHitPoints { get { return ((ItemBlock)itemBase).maxHitPoints; } }
    }
}
