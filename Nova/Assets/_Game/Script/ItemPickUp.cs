using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script
{
    public class ItemPickUp : MonoBehaviour, IHasItem
    {
        [SerializeField]
        private int pickUpTime;

        void Start() {

            Collider collider = GetComponent<Collider>();

            if (collider != null) {
                collider.isTrigger = true;
            }


            MonoBehaviour[] listMB = GetComponentsInChildren<MonoBehaviour>();
            foreach (MonoBehaviour mb in listMB)
                mb.enabled = false;

            this.enabled = true;

            transform.localScale = transform.localScale * 1f;

        }

        void Update(){

            transform.Rotate(0, 0, Time.deltaTime*45);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Player") {
                GameObject goSlotContainer = (GameObject)Resources.Load("Prefab/SlotContainer");

                GameObject gObject1 = Instantiate(goSlotContainer);
                gObject1.name = itemBase.name;

                SlotContainer slotContainer1 = gObject1.GetComponent<SlotContainer>();
                ItemBase itemBase1 = itemBase.Clone();
                itemBase1.stack = itemBase.stack;

                slotContainer1.ItemBase = itemBase1;

                PlayerInventory inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<PlayerInventory>();

                bool flag = inventory.TryAdd(slotContainer1);

                if (flag)
                    Destroy(gameObject);
            }      
        }

        private ItemBase itemBase;

        public void SetItem(ItemBase itemBase)
        {
            this.itemBase = itemBase;
        }        
    }
}
