using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;

[System.Serializable]
public class ToolLogic : MonoBehaviour {

    public Transform firePoint;

    private ItemToolValues itemToolValues;

	void Start () {
   
    }

    public void setItemValues(ItemValues itemValues) {
        itemToolValues = (ItemToolValues)itemValues;
    }

	void Update () {

        if (Input.GetButtonDown("Fire1")) {
            PrimaryFire();
        }
	}

    public void PrimaryFire() {

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
    }
}
