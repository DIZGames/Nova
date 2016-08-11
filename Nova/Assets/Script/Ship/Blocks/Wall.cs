using UnityEngine;
using System.Collections;

public class Wall : Block{

    
    public override BlockPosition Position
    {
        get { return BlockPosition.BETWEEN_TOP; }
    }

    public override bool CreatesNewShip
    {
        get { return false; }
    }
}
