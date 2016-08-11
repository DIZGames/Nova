using UnityEngine;
using System.Collections;

public class WallBig : Block {

    public override BlockPosition Position
    {
        get { return BlockPosition.CENTER; }
    }

    public override bool CreatesNewShip
    {
        get { return false; }
    }
}
