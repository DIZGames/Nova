using UnityEngine;
using System.Collections;
using Assets.Script.Interface;
using Assets.Script;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float speed;

    [SerializeField]
    private LayerMask layerMask;

    private Camera camera;
    int oldMask;

    private bool flag;

    //[SerializeField]
    //private LayerMask maskNotOnShip;
    [SerializeField]
    private LayerMask maskOnShip;

    private ShipManager OnBoardShip;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        oldMask = camera.cullingMask;
        //camera.cullingMask = maskNotOnShip;

        //InvokeRepeating("test", 1, 1);
    }

    private void test() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;

        speed = 0.1f * Time.deltaTime; ;

        Vector3 targetDir = (mousePos - transform.position).normalized;

        //targetDir.z = 0;

        float step = speed * Time.deltaTime;
        //Vector3 newDir = Vector3.RotateTowards(transform.up, targetDir, step, 0.0F);
        Vector3 newDir = Vector3.RotateTowards(transform.up, transform.up + new Vector3(1,1,0), step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);


        Debug.Log("Mousepos" + mousePos);
    }
    [SerializeField]
    private Transform transform1;


    void Update() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
            //camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Ship_Top"));

            //camera.cullingMask = maskOnShip;
            OnBoardShip = hit1.collider.transform.root.GetComponent<ShipManager>();
            OnBoardShip.HideTopList();


            //if (flag == false) {
            //    gameObject.AddComponent<RelativeJoint2D>();
            //    gameObject.GetComponent<RelativeJoint2D>().connectedBody = hit1.collider.transform.root.GetComponent<Rigidbody2D>();
            //    flag = true;
            //}

            //camera.cullingMask = maskOnShip;
        }
        else {
            if (OnBoardShip != null) {
                OnBoardShip.ShowTopList();
            }
            //camera.cullingMask = oldMask;
            //Destroy(gameObject.GetComponent<FixedJoint2D>());
            //flag = false;
            //camera.cullingMask = maskNotOnShip;
        }

        if (Input.GetButtonDown("PlayerUp")) {


            Debug.Log("PlayerUp");
            gameObject.layer = LayerMask.NameToLayer("PlayerUp");
            GetComponent<SpriteRenderer>().sortingLayerName = "PlayerUp";
        }

        if (Input.GetButtonDown("PlayerDown")) {
            Debug.Log("PlayerDown");
            gameObject.layer = LayerMask.NameToLayer("Player");
            GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        }

        if (Input.GetButtonDown("Use")) {
            //RaycastHit2D hit = Physics2D.Raycast(transform.position+transform.up/4, rayDirection,1);
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + transform.up / 4, rayDirection, 1);

            Debug.DrawRay(transform.position + transform.up / 4, rayDirection, Color.magenta, 1);

            //if (hit.collider != null) {

            //    InteractWithPlayerRaycast interactWithPlayerRaycast = hit.collider.gameObject.GetComponent<InteractWithPlayerRaycast>();
            //    interactWithPlayerRaycast.RaycastAction();

            //}

            foreach (RaycastHit2D hit in hits) {
                InteractWithPlayerRaycast interactWithPlayerRaycast = hit.collider.gameObject.GetComponent<InteractWithPlayerRaycast>();
                if (interactWithPlayerRaycast != null)
                    interactWithPlayerRaycast.RaycastAction();
            }
        }
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

        float v = Input.GetAxis("Vertical");

        if (v > 0) {
            rb2d.AddForce(vectornew * Time.deltaTime * speed);
        }
        if (v < 0) {
            rb2d.AddForce(-vectornew * Time.deltaTime * speed);
        }

        float h = Input.GetAxis("Horizontal");

        Vector2 vector2 = new Vector2(-vectornew.y, vectornew.x);

        if (h > 0) {
            rb2d.AddForce(-vector2 * Time.deltaTime * speed);
        }
        if (h < 0) {
            rb2d.AddForce(vector2 * Time.deltaTime * speed);
        }
    }
}
