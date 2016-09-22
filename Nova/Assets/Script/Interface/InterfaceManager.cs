using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;
using Assets.Script;
using Assets.Script.Interface;
using System;
using Assets.Script.Ship;

public class InterfaceManager : MonoBehaviour {

    public WeaponInterface weaponInterface;

    public GameObject backPack;
    public GameObject characterScreen;
    public GameObject hotBar;

    public GameObject saveLoadMenu;

    public GameObject playerStat;
    public GameObject weaponStat;

    public UIContainer UIContainer;
    public ContainerSplit containerSplit;
    public CharacterUI characterUI;

    public ShipInterface shipInterface;
 
    public MessageBox MessageBox;

	void Update () {

        if (Input.GetButtonDown("BackPack")) {
            ShowCharacterUI(backPack.GetComponent<UI>(),characterScreen.GetComponent<IUI>(), hotBar.GetComponent<IUI>(), "Character");
        } else if(Input.GetButtonDown("Cancel")) {
            backPack.SetActive(false);
            saveLoadMenu.SetActive(!saveLoadMenu.activeSelf);
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

    public void ShowShipInterface(GameObject ship) {
        if (shipInterface.gameObject.activeSelf) {
            shipInterface.gameObject.SetActive(false);
            this.ShowPlayerInterface();
        } else {
            shipInterface.SetShip(ship);
            shipInterface.gameObject.SetActive(true);
            this.HidePlayerInterface();
        }
       
    }

    public void HidePlayerInterface() {
        backPack.SetActive(false);
        characterScreen.SetActive(false);
        hotBar.SetActive(false);
        playerStat.SetActive(false);
    }

    public void ShowPlayerInterface() {
        backPack.SetActive(true);
        characterScreen.SetActive(true);
        hotBar.SetActive(true);
        playerStat.SetActive(true);
    }

}
