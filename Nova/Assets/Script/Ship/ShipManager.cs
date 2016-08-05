using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipManager : MonoBehaviour {

    public int Air {private set ; get; }
    public int Energy {private set ; get; }
    List<InventoryInterface> iceContainerList = new List<InventoryInterface>();
    List<InventoryInterface> organicContainerList = new List<InventoryInterface>();
    List<InventoryInterface> energyContainerList = new List<InventoryInterface>();
    List<InventoryInterface> rawContainerList = new List<InventoryInterface>();
    List<InventoryInterface> processedContainerList = new List<InventoryInterface>();
    List<InventoryInterface> ammoContainerList = new List<InventoryInterface>();
    List<InventoryInterface> generalContainerList = new List<InventoryInterface>();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addContainer(InventoryInterface container)
    {

    }
}
