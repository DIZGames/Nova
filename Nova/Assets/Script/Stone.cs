using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class Stone : MonoBehaviour{

        void OnTriggerStay(Collider other) {
            Debug.Log("On Trigger Stay");

        }

    }
}
