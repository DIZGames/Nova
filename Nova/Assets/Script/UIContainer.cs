using UnityEngine;
using System.Collections;
using Assets.Script;
using UnityEngine.UI;

public class UIContainer : MonoBehaviour {

    public ContainerDock containerDock1;
    public Text title;

    public void ShowUI(IUI dock1, string title) {

        if (gameObject.activeSelf) {
            this.title.text = "";
            containerDock1.ResetUI();

            gameObject.SetActive(false);
        }
        else {
            this.title.text = title;

            containerDock1.SetIUI(dock1);

            gameObject.SetActive(true);
        }
    }

    public void ResetUI() {
        this.title.text = "";
        containerDock1.ResetUI();

        gameObject.SetActive(false);
    }
}
