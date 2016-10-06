using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;
using Assets.Script;
using Assets.Script.Interface;
using System;
using Assets.Script.Ship;

public class InterfaceManager : MonoBehaviour {

    //Player UIs
    public GameObject backPack;
    public GameObject characterScreen;
    public GameObject hotBar;

    public GameObject playerStat;
    public GameObject weaponStat;

    //Save Load Mena
    public GameObject saveLoadMenu;

    public UIContainer uiContainer;
    public UIContainerSplit uiContainerSplit;
    public UIContainerCharacter uiContainerCharacter;

    //Ship UI
    public ShipInterface shipInterface;
 
    //Misc
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
        if (uiContainer.gameObject.activeSelf) {
            uiContainer.ResetUI();
        } else {
            uiContainerSplit.ResetUI();
            uiContainerCharacter.ResetUI();
            uiContainer.ShowUI(dock1, title);
        }
    }

    public void ShowUIWithBackpack(IUI dock, string title) {

        if (uiContainerSplit.gameObject.activeSelf) {
            uiContainerSplit.ResetUI();
        }
        else {
            uiContainerCharacter.ResetUI();
            uiContainer.ResetUI();
            uiContainerSplit.ShowUI(backPack.GetComponent<UI>(), dock, title);
        }
    }

    public void ShowCharacterUI(IUI dock1, IUI dock2, IUI dock3, string title) {

        if (uiContainerCharacter.gameObject.activeSelf) {
            uiContainerCharacter.ResetUI();
        }
        else {
            uiContainerSplit.ResetUI();
            uiContainer.ResetUI();
            uiContainerCharacter.ShowUI(dock1, dock2, dock3, title);
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

    public void SetPlayer(Player player)
    {
        ((CharacterScreen)characterScreen.GetComponent<CharacterScreen>()).Player = player;
        ((Hotbar)hotBar.GetComponent<Hotbar>()).Player = player;
        ((PlayerInterface)playerStat.GetComponent<PlayerInterface>()).Player = player;
    }

}
