using Assets.Script;
using Assets.Script.ItemSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class ToolBeamLogic : MonoBehaviour, IEquippable, IHasItem
    {
        [SerializeField]
        private Transform firePoint;
        private ItemTool itemTool;
        private PlayerInventory inventory;

        public void SetItem(ItemBase itemBase){
            itemTool = (ItemTool)itemBase;
            inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<PlayerInventory>();
            itemTool.inStock = inventory.Count(itemTool.Ammo.name);
        }

        public void Action1(){
            if (true)
            {
                Debug.DrawRay(firePoint.position + transform.up / 4, transform.up, Color.yellow, 1);

                RaycastHit hit;
                Physics.Raycast(firePoint.position + transform.up / 4, transform.up, out hit, 1);

                if (hit.collider != null)
                {
                    IInteractWithToolBeam interactWithToolBeam = hit.collider.gameObject.GetComponent<IInteractWithToolBeam>();
                    if (interactWithToolBeam != null)
                        interactWithToolBeam.ToolBeamAction();
                }

                itemTool.loadedProjectiles--;
            }
        }

        public void Action2(){

        }

        public void Action3(){
            if (itemTool.loadedProjectiles != itemTool.Ammo.ClipSize)
            {

                int count = inventory.Count(itemTool.Ammo.name);

                if (count > 0)
                {
                    itemTool.loadedProjectiles = itemTool.Ammo.ClipSize;

                    inventory.Remove(itemTool.Ammo.name, 1);
                    inventory.Refresh();

                    itemTool.inStock = count - 1;
                }
            }
        }


    }
}
