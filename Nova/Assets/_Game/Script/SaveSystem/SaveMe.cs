using UnityEngine;
using System.Collections;
using System;

public class SaveMe : MonoBehaviour {

    public bool saveChildren = true;
    public string prefabId;
    public string uid;

    bool isChildOfSaveMe = false; 

    void Reset()
    {
        #if UNITY_EDITOR
        if(string.IsNullOrEmpty(prefabId))
            prefabId = Guid.NewGuid().ToString();
        #endif
    }

    void Start () {
        uid = Guid.NewGuid().ToString();
        var saveMes = GetComponentsInParent(typeof(SaveMe));
        isChildOfSaveMe = saveMes.Length > 1; // the current object is included in the list
        if(!isChildOfSaveMe)
            SaveManager.Add(gameObject);
	}

    void OnDestroy(){
        if (!isChildOfSaveMe)
            SaveManager.Remove(gameObject);
    }

}
