using Assets.Script.ItemSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class BlockLogic : MonoBehaviour, IEquippable {

        ItemBlockValues itemBlockValues;

        void Start() {

        }

        public void setItemValues(ItemValues itemValues) {
            itemBlockValues = (ItemBlockValues)itemValues;
        }

        public void Action1() {
            if (itemBlockValues.stack > 0) {

                GameObject go = Instantiate(itemBlockValues.Prefab);

                go.transform.position = transform.position;
                go.transform.rotation = transform.rotation;

                go.name = itemBlockValues.Name;

                go.GetComponent<BlockLogic>().setItemValues(itemBlockValues); // Problem??

                if (itemBlockValues.stack == 1) {
                    Destroy(gameObject);
                }

                itemBlockValues.stack--;

            }
        }

        public void Action2() {
           
        }

        public void Action3() {
            
        }

    }
}
