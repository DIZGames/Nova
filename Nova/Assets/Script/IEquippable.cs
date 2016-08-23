using Assets.Script.ItemSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script {
    public interface IEquippable {

        void RaycastAction1();
        void RaycastAction2();
        void RaycastAction3();
        void SetItem(ItemBase itemBase);

    }
}
