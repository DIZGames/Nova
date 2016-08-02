using UnityEngine;
using System.Collections;

public class ThingOnShip: MonoBehaviour, IBlock {

    
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public BlockPosition Position
    {
        get { return BlockPosition.CENTER_TOP; }
    }

    public bool CreatesNewShip
    {
        get { return false; }
    }
}
