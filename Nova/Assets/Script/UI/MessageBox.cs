using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script {
    public class MessageBox : MonoBehaviour{

        public Text text;

        public void ShowMessage(string text, int time) {

            this.text.text = text;
            Invoke("Close", time);
            gameObject.SetActive(true);
        }


        private void Close() {

            CancelInvoke();
            gameObject.SetActive(false);

        }


    }
}
