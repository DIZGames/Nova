using UnityEngine;
using System.Collections;
using Assets.Script.Ship;
using Assets.Script.Interface;
using System;
using Assets.Script;

public class Reaktor : MonoBehaviour, IOpenUI {

    public ShipManagerUnitType unitType;
    private ShipManager shipManager;
    private InterfaceManager interfaceManager;
    private TerminalManager terminalManager;

    private bool isPing;

    // Use this for initialization
    void Start () {
        shipManager = transform.root.GetComponent<ShipManager>();
        terminalManager = transform.root.GetComponent<TerminalManager>();
        shipManager.AddToPing(ProduceEnergy);

        terminalManager.Add("Reactor", transform.parent.GetComponent<SpriteRenderer>().sprite, this.transform);

        interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
    }

    public void Toggle() {
        if (isPing) {
            isPing = false;
            shipManager.RemoveMaxEnergy(2);
        }
         
        else{
            isPing = true;
            shipManager.AddMaxEnergy(2);        
        }  
    }

    private void ProduceEnergy() {
        if (isPing && shipManager.EnergyFull()) {
            if (shipManager.Decrease("Plutonium", unitType, 1)) {
                shipManager.AddEnergy(2);
            }
        }
    }

    void OnMouseDown() {

        int count = shipManager.Count("Plutonium", unitType);
        Debug.Log("Plutonium "+count);

    }

    public void OpenUI() {
        interfaceManager.setChildOnUIContainer(this.transform);
    }
}
