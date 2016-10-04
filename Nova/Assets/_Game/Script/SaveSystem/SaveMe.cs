using UnityEngine;
using System.Collections;
using System;

public class SaveMe : MonoBehaviour {

    public bool saveChildren = true;
    public string prefabId;

    void Reset()
    {
        #if UNITY_EDITOR
        if(string.IsNullOrEmpty(prefabId))
            prefabId = Guid.NewGuid().ToString();
        #endif
    }

    void Start () {
        SaveManager.Add(gameObject);
	}

    void OnDestroy(){
        SaveManager.Remove(gameObject);
    }

}
