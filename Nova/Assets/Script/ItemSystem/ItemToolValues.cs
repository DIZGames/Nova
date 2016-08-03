using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.ItemSystem
{
    [System.Serializable]
    public class ItemToolValues : ItemValues
    {
        public ItemToolValues(ItemBase itemBase) : base(itemBase)
        {
        }

        public ItemAmmo Ammo
        {
            get {
                return ((ItemTool)base.itemBase).Ammo;
                }
        }

        public int FireRate
        {
            get
            {
                return ((ItemTool)base.itemBase).FireRate;
            }
        }


        public int BulletSpeed
        {
            get
            {
                return ((ItemTool)base.itemBase).BulletSpeed;
            }
        }


        public int loadedProjectiles;
        public int inStock;
    }
}
