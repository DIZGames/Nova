using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;
using Assets.Script;
using System;

public class ConsumableLogic : MonoBehaviour, IEquippable, IHasItem {

    private Player player;
    private ItemConsumable itemConsumable;

	void Start () {
	
	}

    public void SetItem(ItemBase itemBase) {
        itemConsumable = (ItemConsumable)itemBase;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Action1() {
        if (itemConsumable.stack > 0) {
            player.addToCurrentValues(itemConsumable.restoreHealth, itemConsumable.restoreArmor, itemConsumable.restoreEnergy, itemConsumable.restoreOxygen);

            if (itemConsumable.stack == 1)
                Destroy(gameObject);

            itemConsumable.stack--;
        }
    }

    public void Action2() {
       
    }

    public void Action3() {
        
    }
}
