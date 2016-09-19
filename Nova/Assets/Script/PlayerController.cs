using UnityEngine;
using System.Collections;
using Assets.Script.Interface;
using Assets.Script;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    public float speed;

    [SerializeField]
    private LayerMask layerMask;

    private Camera camera;
    int oldMask;
    private bool flag;

    [SerializeField]
    private LayerMask maskOnShip;

    private ShipManager OnBoardShip;
    void Start () {
        rb = GetComponent<Rigidbody>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        oldMask = camera.cullingMask;
    }

    void Update() {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mousePos = GetWorldPositionOnPlane(Input.mousePosition, 0);

        //Rotation
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward);
        Quaternion rotationnew = new Quaternion(0, 0, rot.z, rot.w);
        transform.rotation = rotationnew;

        // Berechnet Vektor vom Spieler zur Maus
        Vector2 vectornew = mousePos - transform.position;
        // Normalisiert den Vektor
        vectornew.Normalize();

        Vector3 rayDirection = vectornew;

        RaycastHit2D hit1 = Physics2D.Raycast(transform.position + transform.up / 4, Vector3.zero, 1,layerMask);

        if (hit1.collider != null && gameObject.layer == LayerMask.NameToLayer("Player")) {
            OnBoardShip = hit1.collider.transform.root.GetComponent<ShipManager>();
            OnBoardShip.HideTopList();
        }
        else {
            if (OnBoardShip != null) {
                OnBoardShip.ShowTopList();
            }
        }

        if (Input.GetButtonDown("PlayerUp")) {
            Debug.Log("PlayerUp");
            Vector3 vect = transform.position;
            vect.z = -2;
            transform.position = vect;

        }

        if (Input.GetButtonDown("PlayerNormal")) {
            Debug.Log("PlayerNormal");
            Vector3 vect = transform.position;
            vect.z = 0;
            transform.position = vect;
        }

        if (Input.GetButtonDown("Use")) {
            Debug.DrawRay(transform.position + transform.up / 4, rayDirection, Color.magenta, 1);

            RaycastHit hit;
            Physics.Raycast(transform.position + transform.up / 4, rayDirection, out hit ,1);

            if (hit.collider != null) {
                InteractWithPlayerRaycast interactWithPlayerRaycast = hit.collider.gameObject.GetComponent<InteractWithPlayerRaycast>();
                if (interactWithPlayerRaycast != null)
                    interactWithPlayerRaycast.RaycastAction();
            }

            //RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + transform.up / 4, rayDirection, 1);
           
            //foreach (RaycastHit2D hit in hits) {
            //    InteractWithPlayerRaycast interactWithPlayerRaycast = hit.collider.gameObject.GetComponent<InteractWithPlayerRaycast>();
            //    if (interactWithPlayerRaycast != null)
            //        interactWithPlayerRaycast.RaycastAction();
            //}
        }
    }

    void FixedUpdate() {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mousePos = GetWorldPositionOnPlane(Input.mousePosition,0);

        //Debug.Log(mousePos);

        //Rotation
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward);
        Quaternion rotationnew = new Quaternion(0, 0, rot.z, rot.w);
        transform.rotation = rotationnew;

        // Berechnet Vektor vom Spieler zur Maus
        Vector2 vectornew = mousePos - transform.position;
        // Normalisiert den Vektor
        vectornew.Normalize();

        float v = Input.GetAxis("Vertical");

        if (v > 0) {
            rb.AddForce(vectornew * Time.deltaTime * speed);
        }
        if (v < 0) {
            rb.AddForce(-vectornew * Time.deltaTime * speed);
        }

        float h = Input.GetAxis("Horizontal");

        Vector2 vector2 = new Vector2(-vectornew.y, vectornew.x);

        if (h > 0) {
            rb.AddForce(-vector2 * Time.deltaTime * speed);
        }
        if (h < 0) {
            rb.AddForce(vector2 * Time.deltaTime * speed);
        }
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
