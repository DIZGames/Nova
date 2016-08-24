using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;
using Assets.Script;
using Assets.Script.Interface;
using System;

public class InterfaceManager : MonoBehaviour {

    public WeaponInterface weaponInterface;

    public GameObject backPack;
    public GameObject characterScreen;
    public GameObject hotBar;

    public GameObject playerStat;
    public GameObject weaponStat;

   

    public GameObject reactorBox;

    public UIContainer UIContainer;
    public ContainerSplit containerSplit;
    public CharacterUI characterUI;

    public MessageBox MessageBox;

	void Update () {

        if (Input.GetButtonDown("BackPack")) {
            ShowCharacterUI(backPack.GetComponent<UI>(),characterScreen.GetComponent<IUI>(), hotBar.GetComponent<IUI>(), "Character");
        }

    }

    public void SetItemTool(ItemBase itemBase) {

        weaponStat.GetComponent<WeaponInterface>().ItemTool = (ItemTool)itemBase;

    }

    public void showWeaponStat(bool visible) {
        weaponStat.SetActive(visible);
    }

    public void ShowUI(IUI dock1, string title) {
        if (UIContainer.gameObject.activeSelf) {
            UIContainer.ResetUI();
        } else {
            containerSplit.ResetUI();
            characterUI.ResetUI();
            UIContainer.ShowUI(dock1, title);
        }
    }

    public void ShowUIWithBackpack(IUI dock, string title) {

        if (containerSplit.gameObject.activeSelf) {
            containerSplit.ResetUI();
        }
        else {
            characterUI.ResetUI();
            UIContainer.ResetUI();
            containerSplit.ShowUI(backPack.GetComponent<UI>(), dock, title);
        }
    }

    public void ShowCharacterUI(IUI dock1, IUI dock2, IUI dock3, string title) {

        if (characterUI.gameObject.activeSelf) {
            characterUI.ResetUI();
        }
        else {
            containerSplit.ResetUI();
            UIContainer.ResetUI();
            characterUI.ShowUI(dock1, dock2, dock3, title);
        }    
    }

    public void ShowMessageBox(string text) {
        MessageBox.ShowMessage(text, 3);
    }

}
