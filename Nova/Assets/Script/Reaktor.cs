using UnityEngine;
using System.Collections;
using Assets.Script.Ship;
using Assets.Script.Interface;
using System;
using Assets.Script;

public class Reaktor : MonoBehaviour, IInteractWithPlayerRaycast {

    public ShipManagerUnitType unitType;
    private ShipManager shipManager;
    private InterfaceManager interfaceManager;
    private TerminalManager terminalManager;

    private bool isPing;
    private int ticksForProcessing = 0;


    // Use this for initialization
    void Start () {
        shipManager = transform.root.GetComponent<ShipManager>();       
        shipManager.AddToPing(ProduceEnergy);

        //terminalManager = transform.root.GetComponent<TerminalManager>();
        //terminalManager.Add("Reactor", transform.parent.GetComponent<SpriteRenderer>().sprite, this.transform);

        interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
    }

    public void Toggle() {
        if (isPing) {
            isPing = false;
        }      
        else{
            isPing = true;   
        }  
    }

    private void ProduceEnergy() {
        if (isPing) {
            if (ticksForProcessing != 0) {
                ticksForProcessing++;
                shipManager.AddEnergy(2);

                if (ticksForProcessing == 5)
                    ticksForProcessing = 0;
            }
            else {
                if (shipManager.Decrease("Plutonium", unitType, 1)) {
                    ticksForProcessing++;
                    shipManager.AddEnergy(2);
                }         
            } 
        } 
    }

    public void RaycastAction() {
        interfaceManager.ShowUI(GetComponent<IUI>(), "Reaktor");
    }
}
