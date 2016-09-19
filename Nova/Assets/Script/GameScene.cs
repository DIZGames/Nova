using UnityEngine;
using System.Collections;

public class GameScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    if(Global.saveData != null)
        {
            foreach (SaveGameObject sgo in Global.saveData.RootGameObjetcs)
                sgo.ToGameObject();
            Global.saveData = null;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
