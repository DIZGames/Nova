using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;
using Assets.Script;
using System;

public class ConsumableLogic : MonoBehaviour, IEquippable {

    private Player player;
    private ItemConsumableValues itemConsumableValues;

	void Start () {
	
	}

    public void setItemValues(ItemValues itemValues) {
        itemConsumableValues = (ItemConsumableValues)itemValues;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Action1() {
        if (itemConsumableValues.stack > 0) {
            player.addToCurrentValues(itemConsumableValues.RestoreHealth, itemConsumableValues.RestoreArmor, itemConsumableValues.RestoreEnergy, itemConsumableValues.RestoreOxygen);

            if (itemConsumableValues.stack == 1)
                Destroy(gameObject);

            itemConsumableValues.stack--;
        }
    }

    public void Action2() {
       
    }

    public void Action3() {
        
    }
}
