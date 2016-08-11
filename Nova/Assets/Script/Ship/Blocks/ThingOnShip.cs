using UnityEngine;
using System.Collections;

public class ThingOnShip: Block {

    
    public override BlockPosition Position
    {
        get { return BlockPosition.CENTER_TOP; }
    }

    public override bool CreatesNewShip
    {
        get { return false; }
    }
}
