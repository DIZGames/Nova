using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;
using Assets.Script;
using System;

[System.Serializable]
public class ToolLogic : MonoBehaviour, IEquippable {

    [SerializeField]
    private Transform firePoint;
    private ItemTool itemTool;
    private PlayerInventory inventory;

    public void SetItem(ItemBase itemBase) {
        itemTool = (ItemTool)itemBase;
        inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<PlayerInventory>();

        itemTool.inStock = inventory.Count(itemTool.Ammo.name);
    }

    public void RaycastAction1() {
        if (itemTool.loadedProjectiles > 0) {
            Vector2 mouseposition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

            // Berechnet Vektor vom Spieler zur Maus
            Vector2 vectornew = mouseposition - firePointPosition;
            // Normalisiert den Vektor
            vectornew.Normalize();

            GameObject t = GameObject.Instantiate(itemTool.Ammo.prefab);
            t.transform.position = firePoint.position;
            t.transform.rotation = firePoint.rotation;
            t.name = "Rocket";

            t.GetComponent<Rigidbody2D>().AddForce(vectornew * itemTool.BulletSpeed * Time.deltaTime, ForceMode2D.Impulse);
            //t.GetComponent<Projectile>().damage = currentDamage;

            itemTool.loadedProjectiles--;
        }
    }

    public void RaycastAction2() {
       
    }

    public void RaycastAction3() {
        if (itemTool.loadedProjectiles != itemTool.Ammo.ClipSize) {

            int count = inventory.Count(itemTool.Ammo.name);

            if (count > 0) {
                itemTool.loadedProjectiles = itemTool.Ammo.ClipSize;

                inventory.Decrease(itemTool.Ammo.name,1);
                inventory.UpdateLists();

                itemTool.inStock = count - 1;
            }
        }
    }
}
