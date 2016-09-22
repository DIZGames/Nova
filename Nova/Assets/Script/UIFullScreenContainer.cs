using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script
{
    public class UIFullScreenContainer : MonoBehaviour
    {
        public ContainerDock containerDock1;

        public void ShowUI(IUI dock1)
        {

            if (gameObject.activeSelf)
            {
                containerDock1.ResetUI();
                gameObject.SetActive(false);
            }
            else
            {
                containerDock1.SetIUI(dock1);
                gameObject.SetActive(true);
            }
        }

        public void ResetUI()
        {
            containerDock1.ResetUI();
            gameObject.SetActive(false);
        }

    }
}
