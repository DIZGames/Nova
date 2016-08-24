using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.ItemSystem {
    public class ItemBlockValues : ItemValues {

        public int currentHitPoints;
        private ItemBlock itemBlock;

        public BlockPosition BlockPosition { get { return ((ItemBlock)itemBase).position; } }
        public bool CreatesNewShip { get { return ((ItemBlock)itemBase).createsNewShip; } }
        public int MaxHitPoints { get { return ((ItemBlock)itemBase).maxHitPoints; } }

        private ItemBlockValues() { }

        public static ItemBlockValues CreateNew(ItemBlock itemBlock){
            ItemBlockValues itemBlockValue = CreateInstance<ItemBlockValues>();
            itemBlockValue.itemBase = itemBlock;
            itemBlockValue.itemBlock = itemBlock;
            return itemBlockValue;
        }

        public bool IsCenter()
        {
            ItemBlock itemBlock = (ItemBlock)itemBase;
            return itemBlock.position == BlockPosition.CENTER || itemBlock.position == BlockPosition.CENTER_BOTTOM
                || itemBlock.position == BlockPosition.CENTER_MIDDLE || itemBlock.position == BlockPosition.CENTER_TOP;
        }

        public bool IsBetween()
        {
            ItemBlock itemBlock = (ItemBlock)itemBase;
            return itemBlock.position == BlockPosition.BETWEEN || itemBlock.position == BlockPosition.BETWEEN_FLOOR
                || itemBlock.position == BlockPosition.BETWEEN_MIDDLE || itemBlock.position == BlockPosition.BETWEEN_TOP;
        }
    }
}
