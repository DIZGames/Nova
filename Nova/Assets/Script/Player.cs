using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int Health;
    public int MaxHealth;
    public int Armor;
    public int MaxArmor;
    public int Energy;
    public int MaxEnergy;
    public int Oxygen;
    public int MaxOxygen;

    public GameObject EquipmentPoint;
    public GameObject Head;
    public GameObject Body;

	void Start () {
	
	}
	
	void Update () {
	
	}

    public void setOnEquipment(GameObject equippedObject) {       
        equippedObject.transform.SetParent(EquipmentPoint.transform);
    }

    public void clearOnEquipment() {
        if (EquipmentPoint.transform.childCount != 0)
            Destroy(EquipmentPoint.transform.GetChild(0).gameObject);
    }
}
