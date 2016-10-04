using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Interface;
using Assets.Script.Ship;
using UnityEngine.Events;
using Assets.Script;

public class ShipManager : MonoBehaviour {

    void Start() {
        InvokeRepeating("Ping", 2, 2);
    }


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
            tempCount = iSlotContainerList[i].Remove(itemName, count);
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

    public List<ItemBase> GetInventar() {

        //ItemList itemDataBase = (ItemList)Resources.Load("ItemDataBase");

        //Dictionary<string, ItemBase> dictIce = new Dictionary<string, ItemBase>();

        //for (int k = 0; k < 1000; k++) {
        //    for (int i = 0; i < iceContainerList.Count; i++) {
        //        for (int j = 0; j < iceContainerList[i].Count(); j++) {
        //            if (dictIce.ContainsKey(iceContainerList[i].SCByIndex(j).ItemBase.itemName)) {
        //                ItemBase itemBase = dictIce[iceContainerList[i].SCByIndex(j).ItemBase.itemName];
        //                itemBase.stack += iceContainerList[i].SCByIndex(j).ItemBase.stack;

        //            }
        //            else {
        //                dictIce.Add(iceContainerList[i].SCByIndex(j).ItemBase.itemName, iceContainerList[i].SCByIndex(j).ItemBase.Clone());
        //            }
        //        }
        //    }

           
        //}

        

        //foreach (KeyValuePair<string, ItemBase> entry in dictIce) {
        //    Debug.Log("NAME: " + entry.Key + " STACK: " + entry.Value.stack);
        //}


        return null;
    }


    #endregion

    #region Energy and Oxygen Management

    private int energy;
    private int oxygen;

    public int Energy {
        get { return energy; }
    }
    public int Oxygen {
        get { return oxygen; }
    }

    public void AddEnergy(int energy) {
        this.energy += energy;
    }

    public bool RemoveEnergy(int energy) {
        if (this.energy >= energy) {
            this.energy -= energy;
            return true;
        }
        else {
            return false;
        }
    }

    public void AddOxygen(int oxygen) {
        this.oxygen += oxygen;
    }

    public bool RemoveOxygen(int oxygen) {
        if (this.oxygen >= oxygen) {
            this.oxygen -= oxygen;
            return true;
        }
        else {
            return false;
        }
    }

    public bool isProducing;

    private void Ping() {

        isProducing = true;

        //Energy and Oxygen Storages //Entladen
        for (int i = 0; i < _iTestListStorage.Count; i++) {
            _iTestListStorage[i].Ping();
        }


        // Energy Production
        for (int i = 0; i < _iTestListEnergy.Count; i++) {
            _iTestListEnergy[i].Ping();
        }

        int producedEnergy = energy;

        // Oxygen Production
        for (int i = 0; i < _itTestListOxygen.Count; i++) {
            _itTestListOxygen[i].Ping();
        }

        int producedOxygen = oxygen;

        isProducing = false;

        // Consumer in Prio-Reihenfolge
        for (int i = 0; i < _iTestListConsumerPrio1.Count; i++) {
            _iTestListConsumerPrio1[i].Ping();
        }
        for (int i = 0; i < _iTestListConsumerPrio2.Count; i++) {
            _iTestListConsumerPrio2[i].Ping();
        }
        for (int i = 0; i < _iTestListConsumerPrio3.Count; i++) {
            _iTestListConsumerPrio3[i].Ping();
        }

        int consumedEnergy = producedEnergy - energy;
        int consumedOxygen = producedOxygen - oxygen;

        int tempEnergy = energy;
        int tempOyxgen = oxygen;

        //Energy and Oxygen Storages //Laden
        for (int i = 0; i < _iTestListStorage.Count; i++) {
            _iTestListStorage[i].Ping();
        }

        int savedEnergy = tempEnergy - energy;
        int savedOxygen = tempOyxgen - oxygen;

        Debug.Log(producedEnergy+" "+ consumedEnergy+ " "+ producedOxygen+ " "+ consumedOxygen+ " "+ energy + " " + oxygen);

        energy = 0;
        oxygen = 0;
    }
    
    private List<ITest>  _iTestListEnergy = new List<ITest>();
    private List<ITest> _itTestListOxygen = new List<ITest>();
    private List<ITest> _iTestListStorage = new List<ITest>();
    private List<ITest> _iTestListConsumerPrio1 = new List<ITest>();
    private List<ITest> _iTestListConsumerPrio2 = new List<ITest>();
    private List<ITest> _iTestListConsumerPrio3 = new List<ITest>();
    private List<ITest> _iTestListContainerEnergy = new List<ITest>();

    public List<ITest> iTestListEnergy {
        get {
            return _iTestListEnergy;
        }
    }
    public List<ITest> itTestListOxygen {
        get {
            return _itTestListOxygen;
        }
    }
    public List<ITest> iTestListStorage {
        get {
            return _iTestListStorage;
        }
    }
    public List<ITest> iTestListConsumerPrio1 {
        get {
            return _iTestListConsumerPrio1;
        }
    }
    public List<ITest> iTestListConsumerPrio2 {
        get {
            return _iTestListConsumerPrio2;
        }
    }
    public List<ITest> iTestListConsumerPrio3 {
        get {
            return _iTestListConsumerPrio3;
        }
    }
    public List<ITest> iTestListContainerEnergy {
        get {
            return _iTestListContainerEnergy;
        }
    }

    public void AddToEnergyList(ITest itest) {
        _iTestListEnergy.Add(itest);
    }
    public void AddToOxygenList(ITest itest) {
        _itTestListOxygen.Add(itest);
    }
    public void AddToStorageList(ITest itest) {
        _iTestListStorage.Add(itest);
    }
    public void AddToConsumerPrio1List(ITest itest) {
        _iTestListConsumerPrio1.Add(itest);
    }
    public void AddToConsumerPrio2List(ITest itest) {
        _iTestListConsumerPrio2.Add(itest);
    }
    public void AddToConsumerPrio3List(ITest itest) {
        _iTestListConsumerPrio3.Add(itest);
    }
    public void AddToContainerEnergyList(ITest itest) {
        _iTestListContainerEnergy.Add(itest);
    }

    #endregion

    #region BlockManagement

    public List<GameObject> TopList = new List<GameObject>();

    public void AddTopList(GameObject gO) {
        TopList.Add(gO);
    }

    public void HideTopList() {
        for (int i = 0; i < TopList.Count; i++) {
            TopList[i].SetActive(false);
        }

    }

    public void ShowTopList() {
        for (int i = 0; i < TopList.Count; i++) {
            TopList[i].SetActive(true);
        }

    }

    #endregion


}
