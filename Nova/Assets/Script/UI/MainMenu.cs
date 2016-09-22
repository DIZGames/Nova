using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GameObject saveLoadMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ClickLoad()
    {
        saveLoadMenu.SetActive(true);
    }
}
