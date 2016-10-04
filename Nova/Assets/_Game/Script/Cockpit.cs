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

        private InterfaceManager interfaceManager;

        private Transform cockPitTransform;

        public void RaycastAction() {
            this.enabled = true;

            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            playerController.enabled = false;

            shipController = transform.root.GetComponent<ShipController>();

            playerController.GetComponent<Rigidbody>().isKinematic = false;
            playerController.GetComponent<Collider>().enabled = false;
            flag = true;

            cockPitTransform = transform.parent;

            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
            interfaceManager.ShowShipInterface(transform.root.gameObject);

            playerController.transform.position = cockPitTransform.position;
            playerController.transform.rotation = cockPitTransform.rotation;

            playerController.transform.SetParent(shipController.transform);
        }

        void Update() {
            if (flag) {
                playerController.transform.position = cockPitTransform.position;
                playerController.transform.rotation = cockPitTransform.rotation;
            }

            float v = Input.GetAxis("Vertical");

            if (v > 0) {
                shipController.Up();
            }
            if (v < 0) {
                shipController.Down();
            }

            float h = Input.GetAxis("Horizontal");

            if (h > 0) {
                shipController.Right();
            }
            if (h < 0) {
                shipController.Left();
            }

            if (Input.GetButtonDown("Inertia")) {
                shipController.ToggleDamper();
            }

            //ShipRotation
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Quaternion newRotation = Quaternion.LookRotation(cockPitTransform.root.forward, mousePos - cockPitTransform.position);

            newRotation.x = 0.0f;
            newRotation.y = 0.0f;

            cockPitTransform.root.rotation = Quaternion.Slerp(cockPitTransform.root.rotation, newRotation, Time.deltaTime * 0.4f);

            if (Input.GetButtonDown("Use"))
            {
                interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
                interfaceManager.ShowShipInterface(transform.root.gameObject);

                playerController.GetComponent<Rigidbody>().isKinematic = false;
                playerController.GetComponent<Collider>().enabled = true;
                flag = false;
                this.enabled = false;
                playerController.enabled = true;
                playerController.transform.SetParent(null);
            }
        }
    }
}
