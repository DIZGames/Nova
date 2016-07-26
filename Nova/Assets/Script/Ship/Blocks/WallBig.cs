using UnityEngine;
using System.Collections;

public class WallBig : MonoBehaviour, IBlock {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public BlockPosition position
    {
        get { return BlockPosition.CENTER; }
    }

    public bool floorLevel
    {
        get { return false; }
    }

    public bool createsNewShip
    {
        get { return false; }
    }
}
