using Assets.Script.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class Cockpit : MonoBehaviour, IInteractWithPlayerRaycast{

        private ShipController shipController;
        private PlayerController playerController;

        private bool flag;

        public void RaycastAction() {
            this.enabled = true;

            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerController.enabled = false;
            shipController = transform.root.GetComponent<ShipController>();

            //Destroy(playerController.GetComponent<Rigidbody2D>());
            playerController.GetComponent<Rigidbody2D>().isKinematic = false;
            flag = true;


            playerController.transform.position = transform.position;
            playerController.transform.rotation = transform.rotation;

            playerController.transform.SetParent(shipController.transform);

        }

        void Start() {
            
            
        }

        void Update() {
            if (flag) {
                playerController.transform.position = transform.position;
                playerController.transform.rotation = transform.rotation;
            }

            float v = Input.GetAxis("Vertical");

            if (v > 0) {
                shipController.Up();
                //rb2d.AddForce(vectornew * Time.deltaTime * speed);
            }
            if (v < 0) {
                shipController.Down();
                //rb2d.AddForce(-vectornew * Time.deltaTime * speed);
            }

            float h = Input.GetAxis("Horizontal");

            //Vector2 vector2 = new Vector2(-vectornew.y, vectornew.x);

            if (h > 0) {
                shipController.Right();
                //rb2d.AddForce(-vector2 * Time.deltaTime * speed);
            }
            if (h < 0) {
                shipController.Left();
                //rb2d.AddForce(vector2 * Time.deltaTime * speed);
            }

            //ShipRotation
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Quaternion newRotation = Quaternion.LookRotation(transform.root.forward, mousePos - transform.position);

            newRotation.x = 0.0f;
            newRotation.y = 0.0f;
            transform.root.rotation = Quaternion.Slerp(transform.root.rotation, newRotation, Time.deltaTime * 0.4f);

            if (Input.GetButtonDown("Use")) {

                //playerController.gameObject.
                playerController.GetComponent<Rigidbody2D>().isKinematic = false;
                flag = false;
                //playerController.GetComponent<Rigidbody2D>().WakeUp();
                this.enabled = false;

                playerController.enabled = true;
                

                playerController.transform.SetParent(null);


            }
        }

    }
}
