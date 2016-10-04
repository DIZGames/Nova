using UnityEngine;
using System.Collections;
using Assets.Script.Ship;
using Assets.Script.Interface;
using System;
using Assets.Script;
using UnityEngine.UI;

public class Reaktor : MonoBehaviour, IInteractWithPlayerRaycast, ITest {

    public ShipManagerUnitType unitType;
    private ShipManager shipManager;
    private InterfaceManager interfaceManager;

    [SerializeField]
    private Toggle toggle;

    private bool isPing;
    private int ticksForProcessing = 0;


    public GameObject gameObject1 {
        get {
            return gameObject;
        }
    }

    public bool Power {
        get {
            return toggle.isOn;
        }

        set {
            toggle.isOn = value;
        }
    }

    public IUI iUI {
        get {
            return GetComponent<IUI>();
        }
    }

    // Use this for initialization
    void Start () {
        shipManager = transform.root.GetComponent<ShipManager>();       
        if(shipManager != null)
            shipManager.AddToEnergyList(this);

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

    public void Ping() {
        if (toggle.isOn) {
            if (shipManager.Decrease("Plutonium", unitType, 1)) {
                shipManager.AddEnergy(2);
            }
        }
    }

    public void Consume() {
        if (toggle.isOn) {

        }
    }
}
