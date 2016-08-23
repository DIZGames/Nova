using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script {
    public class ToolTip : MonoBehaviour{

        public Text title;
        public Text text;

        public void SetToolTip(string title, string text) {
            this.title.text = title;
            this.text.text = text;

        }

        void Update() {

            transform.position = Input.mousePosition;
        }


    }
}
