using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;

public class InterfaceManager : MonoBehaviour {

    public WeaponInterface weaponInterface;

    public GameObject backPack;
    public GameObject characterScreen;
    public GameObject hotBar;

    public GameObject playerStat;
    public GameObject weaponStat;

    public GameObject UIContainer;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("BackPack")) {
            if (backPack.activeSelf)
                backPack.SetActive(false);
            else
                backPack.SetActive(true);

        }
        if (Input.GetButtonDown("CharacterScreen")) {
            if (characterScreen.activeSelf)
                characterScreen.SetActive(false);
            else
                characterScreen.SetActive(true);
        }
    }

    public void setItemToolOnWeaponStat(ItemValues itemValues) {

        weaponStat.GetComponent<WeaponInterface>().ItemToolValues = (ItemToolValues)itemValues;

    }

    public void showWeaponStat(bool visible) {
        weaponStat.SetActive(visible);
    }

    public void setChildOnUIContainer(Transform child) {
        UIContainer.GetComponent<UIContainer>().setChild(child);
    }
}
