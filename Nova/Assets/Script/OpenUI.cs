using Assets.Script.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class OpenUI : MonoBehaviour{

        public Transform transformUI;

        private IOpenUI iOpenUI;

        void Start() {
            iOpenUI = transformUI.GetComponent<IOpenUI>();
        }

        void OnMouseDown() {
            iOpenUI.OpenUI();
        }
    }
}
