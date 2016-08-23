﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script {
    public class CharacterUI : MonoBehaviour{

        public ContainerDock containerDock1;
        public ContainerDock containerDock2;
        public ContainerDock containerDock3;
        public Text title;

        public void ShowUI(IUI dock1, IUI dock2, IUI dock3, string title) {


            if (gameObject.activeSelf) {
                this.title.text = "";
                containerDock1.ResetUI();
                containerDock2.ResetUI();
                containerDock3.ResetUI();

                gameObject.SetActive(false);
            }
            else {
                this.title.text = title;

                containerDock1.SetIUI(dock1);
                containerDock2.SetIUI(dock2);
                containerDock3.SetIUI(dock3);

                gameObject.SetActive(true);
            }
        }

        public void ResetUI() {
            this.title.text = "";
            containerDock1.ResetUI();
            containerDock2.ResetUI();
            containerDock3.ResetUI();

            gameObject.SetActive(false);
        }

    }
}
