using UnityEngine;
using System.Collections;

public class Floor : Block {

    
    public override bool CreatesNewShip
    {
        get { return true; }
    }

    public override BlockPosition Position
    {
        get { return BlockPosition.CENTER_BOTTOM; }
    }

}
