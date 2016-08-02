using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour, IBlock{

    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public BlockPosition Position
    {
        get { return BlockPosition.BETWEEN_TOP; }
    }

    public bool floorLevel
    {
        get { return false; }
    }

    public bool CreatesNewShip
    {
        get { return false; }
    }
}
