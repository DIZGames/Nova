using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Interface;
using Assets.Script.Ship;
using UnityEngine.Events;

public class ShipManager : MonoBehaviour {

    private UnityEvent unityEventPing = new UnityEvent();

    public void AddToPing(UnityAction pingAction) {
        unityEventPing.AddListener(pingAction);
    }

    private int energy = 0;
    private int oxygen = 0;
    private int maxEnergy = 0;
    private int maxOxygen = 0;

    public int Energy {
        get {
            return energy;
        }
    }
    public int Oxygen {
        get {
            return oxygen;
        }
    }
    public int MaxEnergy {
        get {
            return maxEnergy;
        }
    }
    public int MaxOxygen {
        get {
            return maxOxygen;
        }
    }

    List<ISlotContainerList> iceContainerList = new List<ISlotContainerList>();
    List<ISlotContainerList> organicContainerList = new List<ISlotContainerList>();
    List<ISlotContainerList> energyContainerList = new List<ISlotContainerList>();
    List<ISlotContainerList> rawContainerList = new List<ISlotContainerList>();
    List<ISlotContainerList> processedContainerList = new List<ISlotContainerList>();
    List<ISlotContainerList> ammoContainerList = new List<ISlotContainerList>();
    List<ISlotContainerList> generalContainerList = new List<ISlotContainerList>();

    void Start () {
        InvokeRepeating("Ping", 2, 2);
    }

    void Ping() {
        unityEventPing.Invoke();
    }

    public void addContainer(ISlotContainerList container, ShipManagerUnitType shipManagerUnitType)
    {
        List<ISlotContainerList> iSlotContainerList = ChooseContainerList(shipManagerUnitType);

        iSlotContainerList.Add(container);          
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

    public void AddEnergy(int energy) {
        if (maxEnergy <= (this.energy + energy)) {
            this.energy = maxEnergy;
        }
        else {
            this.energy += energy;
        }
    }
    public void AddOxygen(int oxygen) {
        if (maxOxygen <= (this.oxygen + oxygen)) {
            this.oxygen = maxOxygen;
        }
        else {
            this.oxygen += oxygen;
        }
    }
    public bool RemoveEnergy(int energy) {
        if (this.energy >= energy) {
            this.energy -= energy;
            return true;
        }
        return false;
        
    }
    public bool RemoveOxygen(int oxygen) {
        if (this.oxygen >= oxygen) {
            this.oxygen -= oxygen;
            return true;
        }
        return false;
    }
    public bool EnergyFull() {
        if (this.energy == this.maxEnergy) {
            return false;
        }
        return true;
    }
    public bool OxygenFull() {
        if (this.oxygen == this.maxOxygen) {
            return false;
        }
        return true;
    }

    public void AddMaxOxygen(int maxOxygen) {
        this.maxOxygen += maxOxygen;
    }
    public void AddMaxEnergy(int maxEnergy) {
        this.maxEnergy += maxEnergy;
    }
    public void RemoveMaxOxygen(int maxOxygen) {
        this.maxOxygen -= maxOxygen;
    }
    public void RemoveMaxEnergy(int maxEnergy) {
        this.maxEnergy -= maxEnergy;
    }

}
