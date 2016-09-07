using UnityEngine;
using System.Collections;

public class SaveMe : MonoBehaviour {

    public bool saveChildren = true;

	// Use this for initialization
	void Start () {
        SaveManager.Add(gameObject);
	}

    void OnDestroy(){
        SaveManager.Remove(gameObject);
    }

}
