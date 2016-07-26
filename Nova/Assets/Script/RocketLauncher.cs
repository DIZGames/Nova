using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour {

	// Use this for initialization
	void Start () {

        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
        //transform.SetParent(transform);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
