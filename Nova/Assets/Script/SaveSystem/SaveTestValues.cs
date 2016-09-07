using UnityEngine;
using System.Collections;

public class SaveTestValues : MonoBehaviour {

    [SaveThis]
    public string newName;
    [SaveThis]
    int i = 99;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
