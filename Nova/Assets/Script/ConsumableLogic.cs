using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;
using Assets.Script;
using System;

public class ConsumableLogic : MonoBehaviour, IEquippable {

    private Player player;
    private ItemConsumable itemConsumable;

	void Start () {
	
	}

    public void SetItem(ItemBase itemBase) {
        itemConsumable = (ItemConsumable)itemBase;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void RaycastAction1() {
        if (itemConsumable.stack > 0) {
            player.addToCurrentValues(itemConsumable.restoreHealth, itemConsumable.restoreArmor, itemConsumable.restoreEnergy, itemConsumable.restoreOxygen);

            if (itemConsumable.stack == 1)
                Destroy(gameObject);

            itemConsumable.stack--;
        }
    }

    public void RaycastAction2() {
       
    }

    public void RaycastAction3() {
        
    }
}
