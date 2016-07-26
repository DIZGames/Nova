using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour, IBlock {

    
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public BlockPosition position
    {
        get { return BlockPosition.CENTER_FLOOR; }
    }

    public bool createsNewShip
    {
        get { return true; }
    }
}
