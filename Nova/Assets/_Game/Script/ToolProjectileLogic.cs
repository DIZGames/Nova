using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;
using Assets.Script;
using System;
using Assets;

[System.Serializable]
public class ToolProjectileLogic : MonoBehaviour, IEquippable, IHasItem{

    [SerializeField]
    private Transform firePoint;
    private ItemTool itemTool;
    private PlayerInventory inventory;

    public void SetItem(ItemBase itemBase) {
        itemTool = (ItemTool)itemBase;
        inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<PlayerInventory>();

        itemTool.inStock = inventory.Count(itemTool.Ammo.name);
    }

    public void Action1() {
        if (itemTool.loadedProjectiles > 0) {
            Vector2 mouseposition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

            // Berechnet Vektor vom Spieler zur Maus
            Vector2 vectornew = mouseposition - firePointPosition;
            // Normalisiert den Vektor
            vectornew.Normalize();

            GameObject t = GameObject.Instantiate(itemTool.Ammo.prefab);
            t.GetComponent<ProjectileLogic>().enabled = true;

            t.transform.position = firePoint.position;
            t.transform.rotation = firePoint.rotation;
            t.name = "Rocket";

            t.GetComponent<Rigidbody>().AddForce(vectornew * itemTool.BulletSpeed * Time.deltaTime, ForceMode.Impulse);

            itemTool.loadedProjectiles--;
        }
    }

    public void Action2() {
       
    }

    public void Action3() {
        if (itemTool.loadedProjectiles != itemTool.Ammo.ClipSize) {

            int count = inventory.Count(itemTool.Ammo.name);

            if (count > 0) {
                itemTool.loadedProjectiles = itemTool.Ammo.ClipSize;

                inventory.Remove(itemTool.Ammo.name, 1);
                inventory.Refresh();

                itemTool.inStock = count - 1;
            }
        }
    }
}
