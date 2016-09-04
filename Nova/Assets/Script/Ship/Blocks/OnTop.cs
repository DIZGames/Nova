using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Ship.Blocks {
    public class OnTop : Block{

        ShipManager shipManager;

        void Start() {
            shipManager = transform.root.GetComponent<ShipManager>();
            shipManager.AddTopList(gameObject);
        }

    }
}
