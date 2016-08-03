using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;


public class CraftingSystemLogic : MonoBehaviour {

    private InterfaceManager interfaceManager;
    public Transform craftingScreen;

    void Start() {
        interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
    }

    void OnMouseDown() {

        interfaceManager.setChildOnUIContainer(craftingScreen);
    }

}
