using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;

public class ToolLogic : MonoBehaviour {

    public Transform firePoint;
    private ToolData toolData;

    public int currentDamage;

	void Start () {
        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;

        firePoint = gameObject.transform.FindChild("FirePoint");

        
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

        GameObject t = GameObject.Instantiate(toolData.Ammo.prefab);
        t.transform.position = firePoint.position;
        t.transform.rotation = firePoint.rotation;
        t.name = "Rocket";

        t.GetComponent<Rigidbody2D>().AddForce(vectornew * toolData.BulletSpeed * Time.deltaTime, ForceMode2D.Impulse);
        t.GetComponent<Projectile>().damage = currentDamage;
    }

    public ToolData ToolData
    {
        get { return toolData; }
        set { toolData = value; }
    }
}
