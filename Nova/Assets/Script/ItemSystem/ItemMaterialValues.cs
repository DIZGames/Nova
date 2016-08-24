using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.ItemSystem {
    public class ItemMaterialValues : ItemValues {
        public MaterialType MaterialType { get { return ((ItemMaterial)itemBase).materialType; } }
    }
}
