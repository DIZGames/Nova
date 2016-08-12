﻿using UnityEngine;
using System.Collections;

public class UIContainer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    private Transform parentTransform;
    private Transform childTransform;

	// Update is called once per frame
	void Update () {
	
	}

    public void setChild(Transform newchildTransform) {

        if (newchildTransform.GetChild(0).gameObject.activeSelf) {
            if (parentTransform != null) {
                childTransform.GetChild(0).gameObject.SetActive(false);
                //childTransform.gameObject.SetActive(false);
                childTransform.SetParent(parentTransform);
            }

        }
        else {
            if (parentTransform != null) {
                childTransform.GetChild(0).gameObject.SetActive(false);
                //childTransform.gameObject.SetActive(false);
                childTransform.SetParent(parentTransform);
            }

            //Informationen speichern über kind und vater
            childTransform = newchildTransform;
            parentTransform = newchildTransform.parent;

            //An UIContainer hängen
            newchildTransform.position = transform.position;
            newchildTransform.rotation = transform.rotation;
            newchildTransform.SetParent(transform);
            newchildTransform.GetChild(0).gameObject.SetActive(true);
            //newchildTransform.gameObject.SetActive(true);
  
        }
    }
}
