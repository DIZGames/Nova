using UnityEngine;
using Assets.Script;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    bool isLocalPlayer = true;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private ShipManager OnBoardShip;

    void Start () {
        PlayerNetwork playerNetwork = GetComponent<PlayerNetwork>();
        if (playerNetwork != null)
            isLocalPlayer = playerNetwork.isLocalPlayer;

        rb = GetComponent<Rigidbody>();
        if (isLocalPlayer){
            ((CameraController)Camera.main.GetComponent<CameraController>()).Player = gameObject;
            GameObject hotbar = GameObject.Find("Hotbar");
            if (hotbar != null)
                ((Hotbar)hotbar.GetComponent<Hotbar>()).Player = gameObject.GetComponent<Player>();
            GameObject playerInterface = GameObject.Find("PlayerStatusUI");
            if (playerInterface != null)
                ((PlayerInterface)playerInterface.GetComponent<PlayerInterface>()).Player = gameObject.GetComponent<Player>();
        }

    }

    void Update() {
        if (isLocalPlayer)
        {
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

            RaycastHit hit1;
            Physics.Raycast(transform.position, Vector3.forward, out hit1, 1, layerMask);

            Debug.DrawRay(transform.position, Vector3.forward, Color.magenta, 1);

            if (hit1.collider != null)
            {
                OnBoardShip = hit1.collider.transform.root.GetComponent<ShipManager>();
                OnBoardShip.HideTopList();

                Vector3 asd3 = hit1.point;
                asd3.z = 0;

                rb.MovePosition(asd3);
                //rb.useGravity = true;

                //Vector3 asd2 = hit1.point;//hit1.collider.transform.position;
                //asd2.z = 0;
                //transform.position = asd2;

            }
            else
            {
                if (OnBoardShip != null)
                {
                    OnBoardShip.ShowTopList();
                    OnBoardShip = null;

                    //rb.useGravity = false;
                }
            }

            if (Input.GetButtonDown("PlayerUp"))
            {
                Debug.Log("PlayerUp");
                Vector3 vect = transform.position;
                vect.z = -2;
                transform.position = vect;

            }

            if (Input.GetButtonDown("PlayerNormal"))
            {
                Debug.Log("PlayerNormal");
                Vector3 vect = transform.position;
                vect.z = 0;
                transform.position = vect;
            }

            if (Input.GetButtonDown("Use"))
            {
                Debug.DrawRay(transform.position + transform.up / 4, rayDirection, Color.magenta, 1);

                RaycastHit hit;
                Physics.Raycast(transform.position + transform.up / 4, rayDirection, out hit, 1);

                if (hit.collider != null)
                {
                    InteractWithPlayerRaycast interactWithPlayerRaycast = hit.collider.gameObject.GetComponent<InteractWithPlayerRaycast>();
                    if (interactWithPlayerRaycast != null)
                        interactWithPlayerRaycast.RaycastAction();
                }
            }
        }
    }

    void FixedUpdate() {
        if (isLocalPlayer)
        {
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

            float v = Input.GetAxis("Vertical");

            if (OnBoardShip != null)
            {


                Vector3 asd = OnBoardShip.gameObject.GetComponent<Rigidbody>().velocity;
                float t = asd.magnitude;

                //rb.AddForce(asd, ForceMode.VelocityChange);

                //rb.AddForce(asd, ForceMode.Impulse);

                //rb.velocity += asd;

                //rb.AddForce(asd * t * Time.deltaTime);
            }


            if (v > 0)
            {
                rb.AddForce(vectornew * Time.deltaTime * speed);
                //rb.AddRelativeForce(vectornew * Time.deltaTime * speed);
            }
            if (v < 0)
            {
                rb.AddForce(-vectornew * Time.deltaTime * speed);
            }

            float h = Input.GetAxis("Horizontal");

            Vector2 vector2 = new Vector2(-vectornew.y, vectornew.x);

            if (h > 0)
            {
                rb.AddForce(-vector2 * Time.deltaTime * speed);
            }
            if (h < 0)
            {
                rb.AddForce(vector2 * Time.deltaTime * speed);
            }
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
