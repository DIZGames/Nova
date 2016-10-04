using UnityEngine;
using System.Collections;

public class SaveMe : MonoBehaviour {

    public bool saveChildren = true;
    public GameObject Prefab { get; set; }

    void Reset()
    {
        #if UNITY_EDITOR
        Prefab = (GameObject)UnityEditor.PrefabUtility.GetPrefabParent(gameObject);
        Debug.Log(gameObject);
        #endif
    }

    void Start () {
        SaveManager.Add(gameObject);
	}

    void OnDestroy(){
        SaveManager.Remove(gameObject);
    }

}
