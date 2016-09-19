using Assets.Script.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class Thruster : MonoBehaviour{

        private ShipController shipController;
        private Rigidbody rb;

        void Start() {
            shipController = transform.root.GetComponent<ShipController>();
            rb = transform.root.GetComponent<Rigidbody>();


            Vector3 eulerAngles = transform.rotation.eulerAngles;
            //Debug.Log(eulerAngles.z);
            Vector3 eulerAngles2 = transform.localRotation.eulerAngles;
            //Debug.Log("local:"+eulerAngles2.z);

            shipController.AddThrusterAction(Force, (int)eulerAngles2.z);

        }



        private void Force() {

            Vector3 asdwer = transform.root.position - transform.position;
            float angle = Vector3.Angle(transform.root.position, transform.position);

            rb.AddForce(transform.up * 200);

            

            //Debug.Log("Force"+(int)transform.eulerAngles.z);
        }

    }
}
