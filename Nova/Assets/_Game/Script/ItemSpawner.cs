using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform spawnPoint;

        void Start() {
            if (spawnPoint == null) {
                spawnPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().transform;
            }
        }


        public void SpawnItem(ItemBase itemBase) {

            GameObject go = Instantiate(itemBase.prefab);
            go.transform.position = spawnPoint.position + spawnPoint.up;
            go.name = itemBase.itemName;

            ItemPickUp itemPickUp = go.AddComponent<ItemPickUp>();
            itemPickUp.SetItem(itemBase);

        }

    }
}
