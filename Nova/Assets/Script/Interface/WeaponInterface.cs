using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Script.ItemSystem;

public class WeaponInterface : MonoBehaviour {

    public Text projectilesText;
    public Text magazinesText;
    public Text ammoText;

    ItemToolValues toolValues;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(toolValues != null){
            projectilesText.text = toolValues.loadedProjectiles.ToString();
	        magazinesText.text = "0";
            ammoText.text = toolValues.Ammo.name;
        }
        else
        {
            projectilesText.text = "0";
            magazinesText.text = "0";
            ammoText.text = "";
        }
	}

    public void setToolValues(ItemToolValues toolValues){
        this.toolValues = toolValues;
    }
}
