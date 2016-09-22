using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script
{
    public class ItemPickUp : MonoBehaviour, IEquippable
    {
        [SerializeField]
        private int pickUpTime;

        void Start() {

            Collider collider = GetComponent<Collider>();

            if (collider != null) {
                collider.isTrigger = true;
            }

            transform.localScale = transform.localScale * 1f;

        }

        void Update(){

            transform.Rotate(0, 0, Time.deltaTime*45);
        }

        void OnTriggerEnter(Collider other)
        {
            GameObject goSlotContainer = (GameObject)Resources.Load("Prefab/SlotContainer");

            GameObject gObject1 = Instantiate(goSlotContainer);
            gObject1.name = itemBase.name;

            SlotContainer slotContainer1 = gObject1.GetComponent<SlotContainer>();
            ItemBase itemBase1 = itemBase.Clone();
            itemBase1.stack = itemBase.stack;

            slotContainer1.ItemBase = itemBase1;

            //Add(slotContainer1);

            PlayerInventory inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<PlayerInventory>();

            bool flag = inventory.TryAdd(slotContainer1);

            if (flag) {
                Destroy(gameObject);
            }

            //inventory

            //Code für aufnahme ins inventory
            Debug.Log("PICKUP");
        }

        private ItemBase itemBase;

        public void SetItem(ItemBase itemBase)
        {
            this.itemBase = itemBase;
        }

        public void RaycastAction1()
        {
            throw new NotImplementedException();
        }

        public void RaycastAction2()
        {
            throw new NotImplementedException();
        }

        public void RaycastAction3()
        {
            throw new NotImplementedException();
        }

        
    }
}
