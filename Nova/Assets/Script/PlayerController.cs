using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float speed;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Rotation
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward);
        transform.rotation = rot;
        

        // Berechnet Vektor vom Spieler zur Maus
        Vector2 vectornew = mousePos - transform.position;
        // Normalisiert den Vektor
        vectornew.Normalize();

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
