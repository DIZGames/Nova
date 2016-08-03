using UnityEngine;
using System.Collections;

public class StorageLogic : MonoBehaviour {

    private InterfaceManager interfaceManager;
    public Transform storage;

    void Start() {
        interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
    }

    void OnMouseDown() {

        interfaceManager.setChildOnUIContainer(storage);

    }
}
