using UnityEngine;
using System.Collections;

public class ShipBuildingUI : MonoBehaviour
{

    public GameObject shipPrefab;
    public GameObject floorPrefab;
    public GameObject wallBigPrefab;
    public GameObject wallSmallPrefab;
    public GameObject thingOnShipPrefab;


    void Start(){
        Global.shipPrefab = shipPrefab;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ClickNewFloor();
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ClickNewSmallWall();
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ClickNewBigWall();
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ClickNewThingOnShip();
    }

    public void ClickNewFloor()
    {
        GameObject newFloor = Instantiate(floorPrefab, Input.mousePosition, Quaternion.identity) as GameObject;
        //newFloor.AddComponent<MovingBlock>();
        Global.objectToMove = newFloor.transform;
    }

    public void ClickNewSmallWall()
    {
        GameObject newWall = Instantiate(wallSmallPrefab, Input.mousePosition, Quaternion.identity) as GameObject;
        //newWall.AddComponent<MovingBlock>();
        Global.objectToMove = newWall.transform;
    }

    public void ClickNewBigWall()
    {
        GameObject newWallB = Instantiate(wallBigPrefab, Input.mousePosition, Quaternion.identity) as GameObject;
        //newWallB.AddComponent<MovingBlock>();
        Global.objectToMove = newWallB.transform;
    }

    public void ClickNewThingOnShip()
    {
        GameObject newThing = Instantiate(thingOnShipPrefab, Input.mousePosition, Quaternion.identity) as GameObject;
        //newThing.AddComponent<MovingBlock>();
        Global.objectToMove = newThing.transform;
    }
}
