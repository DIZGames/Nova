using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Interface;

public class UIListVertical : MonoBehaviour {

    private RectTransform contentContainer;

	// Use this for initialization
	void Start () {
        contentContainer = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void AddList(List<ISlotContainerList> SlotContainer) {
        
    }   

    public void Clear() {

    }
}
