using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class Stone : MonoBehaviour, IInteractWithToolBeam, IInteractWithToolProjectile
    {
        [SerializeField]
        private ItemSpawner itemSpawner;
        [SerializeField]
        private ItemBase itemBase;
        [SerializeField]
        private int itemCount;


        public void ToolBeamAction()
        {
            if (itemCount > 0) {

                itemBase.stack = 1;
                itemCount--;

                itemSpawner.SpawnItem(itemBase);
            }
        }

        public void ToolProjectileAction()
        {
            Destroy(gameObject);
                
        }
    }
}
