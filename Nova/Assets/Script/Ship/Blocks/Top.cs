using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Ship.Blocks {
    public class Top : MonoBehaviour{

        ShipManager shipManager;

        void Start() {

            shipManager = transform.root.GetComponent<ShipManager>();
            shipManager.AddTopList(gameObject);
        }

    }
}
