using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int currentHealth;
    public int currentArmor;
    public int currentEnergy;   
    public int currentOxygen;
    
    public int maxHealth;
    public int maxArmor;
    public int maxEnergy;
    public int maxOxygen;

    public GameObject EquipmentPoint;
    public GameObject Head;
    public GameObject Body;


	void Start () {
        currentHealth = 100;
        currentArmor = 100;
        currentEnergy = 100;
        currentOxygen = 100;

        maxHealth = 100;
        maxArmor = 100;
        maxEnergy = 100;
        maxOxygen = 100;
	}
	
	void Update () {
	
	}

    public void addToCurrentValues(int health, int armor, int energy, int oxygen) {

        if ((currentHealth + health) > maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += health;

        if ((currentArmor + armor) > maxArmor)
            currentArmor = maxArmor;
        else
            currentArmor += armor;

        if ((currentEnergy + energy) > maxEnergy)
            currentEnergy = maxEnergy;
        else
            currentEnergy += energy;

        if ((currentOxygen + oxygen) > maxOxygen)
            currentOxygen = maxOxygen;
        else
            currentOxygen += oxygen;
    }

    public void addToMaxValues(int health, int armor, int energy, int oxygen) {
        maxHealth += health;
        maxArmor += armor;
        maxEnergy += energy;
        maxOxygen += oxygen;
    }

    public void resetMaxValues() {

        if (currentHealth >= 100)
            currentHealth = 100;
        maxHealth = 100;

        if (currentArmor >= 100)
            currentArmor = 100;
        maxArmor = 100;

        if (currentEnergy >= 100)
            currentEnergy = 100;
        maxEnergy = 100;

        if (currentOxygen >= 100)
            currentOxygen = 100;
        maxOxygen = 100;
    }

    public void setOnEquipment(GameObject equippedObject) {       
        equippedObject.transform.SetParent(EquipmentPoint.transform);
    }

    public void clearOnEquipment() {
        for (int i = 0; i < EquipmentPoint.transform.childCount; i++) {
            Destroy(EquipmentPoint.transform.GetChild(i).gameObject);
        }


            
    }


}
