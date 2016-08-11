using UnityEngine;
using Assets.Script.ItemSystem;

public abstract class Block : MonoBehaviour{

    public abstract BlockPosition Position { get; }
    public abstract bool CreatesNewShip { get; } // if true BlockPosition should be set to Center

    public ItemBlockValues ItemBlockValues { set; get; }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
