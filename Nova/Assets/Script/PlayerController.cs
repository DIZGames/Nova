using UnityEngine;
using System.Collections;
using Assets.Script.Interface;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float speed;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Rotation
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward);
        Quaternion rotationnew = new Quaternion(0, 0, rot.z, rot.w);
        transform.rotation = rotationnew;

        


        // Berechnet Vektor vom Spieler zur Maus
        Vector2 vectornew = mousePos - transform.position;
        // Normalisiert den Vektor
        vectornew.Normalize();

        Vector3 rayDirection = vectornew * 3;

      

        //if (Input.GetButtonDown("Fire1")) {
        //    RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 10);

        //    Debug.DrawRay(transform.position, rayDirection, Color.magenta, 1);

        //    if (hit.collider != null) {

        //        hit.collider.gameObject.GetComponent<InterfaceRaycastReceiving>().HitByPlayer();
        //        //hit.transform.GetComponent<InterfaceRaycastReceiving>().HitByPlayer();
        //    }
        //}

        float v = Input.GetAxis("Vertical");

        if (v > 0)
        {
            rb2d.AddForce(vectornew * Time.deltaTime * speed);
        }
        if (v < 0)
        {
            rb2d.AddForce(-vectornew * Time.deltaTime * speed);
        }

        float h = Input.GetAxis("Horizontal");

        Vector2 vector2 = new Vector2(-vectornew.y,vectornew.x);

        if (h > 0)
        {
            rb2d.AddForce(-vector2 * Time.deltaTime * speed);
        }
        if (h < 0)
        {
            rb2d.AddForce(vector2 * Time.deltaTime * speed);
        }
    }
}
