using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Interface;
using Assets.Script.Ship;
using UnityEngine.Events;

public class ShipManager : MonoBehaviour {

    void Start() {
        InvokeRepeating("Ping", 2, 2);
    }

    #region Energy and Oxygen Management

    private UnityEvent unityEventPing = new UnityEvent();

    public void AddToPing(UnityAction unityActionPing) {
        unityEventPing.AddListener(unityActionPing);
    }

    void Ping() {

        Energy = producedEnergy;
        showProducedEnergy = producedEnergy;
        producedEnergy = 0;

        showConsumedEnergy = consumedEnergy;
        consumedEnergy = 0;

        Oxygen = producedOxygen;
        showProducedOxygen = producedOxygen;
        producedOxygen = 0;

        showConsumedOxygen = consumedOxygen;
        consumedOxygen = 0;

        unityEventPing.Invoke();

        Debug.Log("ProducedEnergy: "+ showProducedEnergy + " ConsumedEnergy: "+ showConsumedEnergy + " ProducedOxygen: "+ showProducedOxygen + " ConsumedOxygen: "+ showConsumedOxygen);

    }

    private int Energy;
    private int producedEnergy;    
    private int consumedEnergy; 
     
    public int showProducedEnergy;
    public int showConsumedEnergy;

    private int Oxygen;
    private int producedOxygen;
    private int consumedOxygen;

    public int showProducedOxygen;
    public int showConsumedOxygen;

    public void AddEnergy(int energy) {
        producedEnergy += energy;
    }

    public bool RemoveEnergy(int energy) {
        if (this.Energy >= energy) {
            this.Energy -= energy;
            consumedEnergy += energy;
            return true;
        }
        else {
            return false;       
        }
    }

    public void AddOxygen(int oxygen) {
        producedOxygen += oxygen;
    }

    public bool RemoveOxygen(int oxygen) {
        if (this.Oxygen>= oxygen) {
            this.Oxygen -= oxygen;
            consumedOxygen += oxygen;
            return true;
        }
        else {
            return false;
        }
    }

    #endregion

    #region SlotContainerList

    List<ISlotContainerList> iceContainerList = new List<ISlotContainerList>();
    List<ISlotContainerList> organicContainerList = new List<ISlotContainerList>();
    List<ISlotContainerList> energyContainerList = new List<ISlotContainerList>();
    List<ISlotContainerList> rawContainerList = new List<ISlotContainerList>();
    List<ISlotContainerList> processedContainerList = new List<ISlotContainerList>();
    List<ISlotContainerList> ammoContainerList = new List<ISlotContainerList>();
    List<ISlotContainerList> generalContainerList = new List<ISlotContainerList>();

    public void addContainer(ISlotContainerList container, ShipManagerUnitType shipManagerUnitType) {
        List<ISlotContainerList> iSlotContainerList = ChooseContainerList(shipManagerUnitType);

        iSlotContainerList.Add(container);

        Debug.Log("Add List");
    }
    public int Count(string itemName, ShipManagerUnitType shipManagerUnitType) {

        List<ISlotContainerList> iSlotContainerList = ChooseContainerList(shipManagerUnitType);

        int count = 0;

        for (int i = 0; i < iSlotContainerList.Count; i++) {
            count += iSlotContainerList[i].Count(itemName);
        }

        return count;
    }
    public bool Decrease(string itemName, ShipManagerUnitType shipManagerUnitType, int count) {

        if (count > Count(itemName, shipManagerUnitType)) {
            return false;
        }

        List<ISlotContainerList> iSlotContainerList = ChooseContainerList(shipManagerUnitType);

        int tempCount = 0;

        for (int i = 0; i < iSlotContainerList.Count; i++) {
            tempCount = iSlotContainerList[i].Decrease(itemName, count);
            if (tempCount == 0) {
                return true;
            }
        }
        return false;
    }
    private List<ISlotContainerList> ChooseContainerList(ShipManagerUnitType shipManagertUnitType) {
        List<ISlotContainerList> iSlotContainerList = new List<ISlotContainerList>();

        switch (shipManagertUnitType) {
            case ShipManagerUnitType.ICE:
                iSlotContainerList = iceContainerList;
                break;
            case ShipManagerUnitType.ORGANIC:
                iSlotContainerList = organicContainerList;
                break;
            case ShipManagerUnitType.ENERGY:
                iSlotContainerList = energyContainerList;
                break;
            case ShipManagerUnitType.RAW:
                iSlotContainerList = rawContainerList;
                break;
            case ShipManagerUnitType.PROCESSED:
                iSlotContainerList = processedContainerList;
                break;
            case ShipManagerUnitType.AMMO:
                iSlotContainerList = ammoContainerList;
                break;
            case ShipManagerUnitType.GENERAL:
                iSlotContainerList = generalContainerList;
                break;
            default:
                break;
        }

        return iSlotContainerList;
    }

    #endregion

}
