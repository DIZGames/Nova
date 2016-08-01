using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;
using Assets.Script;

[System.Serializable]
public class ToolLogic : MonoBehaviour {

    [SerializeField]
    private Transform firePoint;

    private ItemToolValues itemToolValues;

    private Inventory inventory;


	void Start () {
        
    }

    public void setItemValues(ItemValues itemValues) {
        itemToolValues = (ItemToolValues)itemValues;
        inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<Inventory>();

        itemToolValues.inStock = inventory.Count(itemToolValues.Ammo.name);
    }

	void Update () {

        if (Input.GetButtonDown("Fire1")) {
            PrimaryFire();
        }

        if (Input.GetButtonDown("Reload")) {
            Reload();
        }
    }

    private void PrimaryFire() {

        if (itemToolValues.loadedProjectiles > 0) {
            Vector2 mouseposition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

            // Berechnet Vektor vom Spieler zur Maus
            Vector2 vectornew = mouseposition - firePointPosition;
            // Normalisiert den Vektor
            vectornew.Normalize();

            GameObject t = GameObject.Instantiate(itemToolValues.Ammo.prefab);
            t.transform.position = firePoint.position;
            t.transform.rotation = firePoint.rotation;
            t.name = "Rocket";

            t.GetComponent<Rigidbody2D>().AddForce(vectornew * itemToolValues.BulletSpeed * Time.deltaTime, ForceMode2D.Impulse);
            //t.GetComponent<Projectile>().damage = currentDamage;

            itemToolValues.loadedProjectiles--;
        }
    }

    private void Reload() {

        if (itemToolValues.loadedProjectiles != itemToolValues.Ammo.ClipSize) {

            int count = inventory.Count(itemToolValues.Ammo.name);

            if (count > 0) {
                itemToolValues.loadedProjectiles = itemToolValues.Ammo.ClipSize;

                inventory.ReduceStackOne(itemToolValues.Ammo.name);
                inventory.UpdateLists();

                itemToolValues.inStock = count - 1;
            }
        }

      
    }
    
}
