using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour, IBlock {

    
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public BlockPosition Position
    {
        get { return BlockPosition.CENTER_BOTTOM; }
    }

    public bool CreatesNewShip
    {
        get { return true; }
    }
}
