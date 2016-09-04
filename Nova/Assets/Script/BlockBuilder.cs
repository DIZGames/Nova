using Assets.Script.ItemSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class BlockBuilder : MonoBehaviour, IEquippable {

        [SerializeField]
        private GameObject shipPrefab;

        [SerializeField]
        private LayerMask layerMaskShipBottom;
        [SerializeField]
        private LayerMask layerMaskShipMiddle;
        [SerializeField]
        private LayerMask layerMaskShipTop;
        [SerializeField]
        private LayerMask layerMaskShipOnTop;

        [SerializeField]
        private LayerMask layerMaskShip;
        [SerializeField]
        private LayerMask layerMaskBetween;
        private LayerMask layerMask;

        [SerializeField]
        private Transform dummyBlock;
        public BlockDummy dummyBlockScript;

        [SerializeField]
        private Transform hitTransform;

        ItemBlock itemBlock;
        int rotation = 0;
        string orientation;

        void Start() {
            shipPrefab = (GameObject)Resources.Load("Prefab/Ship/Ship");


            //InvokeRepeating("raycasting",1,1); 
        }

        void Update() {
            raycasting();
        }

        void FixedUpdate() {
            //raycasting();
        }

        private Vector2 asd2;
        private Vector2 ASD4;

        void raycasting() {
            dummyBlockScript.isAttached = false;
            //RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + transform.up / 4, transform.rotation* Vector2.up, 1);

            //Debug.DrawRay(transform.position + transform.up/3 , transform.rotation * Vector2.up * 1.5f, Color.magenta, 0.1f);

            //foreach (RaycastHit2D hit in hits) {
            //    InteractWithPlayerRaycast interactWithPlayerRaycast = hit.collider.gameObject.GetComponent<InteractWithPlayerRaycast>();
            //    if (interactWithPlayerRaycast != null)
            //        interactWithPlayerRaycast.RaycastAction();
            //}

            Vector2 asd = new Vector2();
             ASD4 = new Vector2();

            hitTransform = null;



            RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up, transform.rotation * Vector2.up * 1, 1.5f, layerMask);
            if (hit.collider != null) {

                hitTransform = hit.collider.transform;
                asd = hit.collider.transform.InverseTransformPoint(hit.point);

                //asd = hit.collider.transform.localPosition;

                double x = 0;
                double y = 0;

                x = Math.Round((double)asd.x, 1);
                y = Math.Round((double)asd.y, 1);

                asd.x = (float)x;
                asd.y = (float)y;

                dummyBlock.rotation = transform.rotation;
                dummyBlock.position = transform.position + transform.up;

                dummyBlockScript.isAttached = false;

                //Debug.Log("XY: " + asd.x + "   " + asd.y);

                //Debug.Log("Worldspace: " + hit.point);


                //dummyBlock.Rotate(0,0,rotation);
                asd2 = asd;
                ASD4 = hitTransform.TransformPoint(asd2);

                if ((itemBlock.position == BlockPosition.CENTER || itemBlock.position == BlockPosition.CENTER_BOTTOM)) {
                    //up
                    if (asd.y > Math.Abs(asd.x)) {//asd.y == 0.5f) {
                        //Debug.Log("UP");
                        orientation = "up";

                        dummyBlock.position = hitTransform.position + hitTransform.TransformVector(Vector2.up);
                        dummyBlock.rotation = hitTransform.rotation;
                        dummyBlock.Rotate(0, 0, rotation);
                    }

                    //down
                    if (asd.y == -0.5f) {
                        //Debug.Log("DOWN");
                        orientation = "down";

                        dummyBlock.position = hitTransform.position + hitTransform.TransformVector(Vector2.down);
                        dummyBlock.rotation = hitTransform.rotation; ;
                        dummyBlock.Rotate(0, 0, rotation);
                    }

                    //left
                    if (asd.x == -0.5f) {
                        //Debug.Log("LEFT");
                        orientation = "left";

                        dummyBlock.position = hitTransform.position + hitTransform.TransformVector(Vector2.left);
                        dummyBlock.rotation = hitTransform.rotation; ;
                        dummyBlock.Rotate(0, 0, rotation);
                    }

                    //right
                    if (asd.x == 0.5f) {
                        //Debug.Log("RIGHT");
                        orientation = "right";

                        dummyBlock.position = hitTransform.position + hitTransform.TransformVector(Vector2.right);
                        dummyBlock.rotation = hitTransform.rotation; ;
                        dummyBlock.Rotate(0, 0, rotation);
                    }
                }

                if (itemBlock.position == BlockPosition.CENTER_MIDDLE) {
                    orientation = "center";

                    dummyBlock.position = hitTransform.position;
                    dummyBlock.rotation = hitTransform.rotation;
                    dummyBlock.Rotate(0, 0, rotation);
                }

                if (itemBlock.position == BlockPosition.BETWEEN) {

                    //up
                    if (asd.y > Math.Abs(asd.x)) {
                        orientation = "betweenUp";

                        dummyBlock.position = hitTransform.position + hitTransform.TransformVector(Vector2.up) * 1 / 2;
                        dummyBlock.rotation = hitTransform.rotation;
                    }
                    //down
                    if (asd.y < -Math.Abs(asd.x)) {
                        orientation = "betweenDown";

                        dummyBlock.position = hitTransform.position + hitTransform.TransformVector(Vector2.down) * 1 / 2;
                        dummyBlock.rotation = hitTransform.rotation;
                        dummyBlock.Rotate(0, 0, 180);
                    }
                    //right
                    if (asd.x > Math.Abs(asd.y)) {
                        orientation = "betweenRight";

                        dummyBlock.position = hitTransform.position + hitTransform.TransformVector(Vector2.right) * 1 / 2;
                        dummyBlock.rotation = hitTransform.rotation;
                        dummyBlock.Rotate(0, 0, 270);
                    }
                    //left
                    if (asd.x < -Math.Abs(asd.y)) {
                        orientation = "betweenLeft";

                        dummyBlock.position = hitTransform.position + hitTransform.TransformVector(Vector2.left) * 1 / 2;
                        dummyBlock.rotation = hitTransform.rotation;
                        dummyBlock.Rotate(0, 0, 90);
                    }
                }

                if (itemBlock.position == BlockPosition.CENTER_ONTOP) {
                    orientation = "ontop";

                    dummyBlock.position = hitTransform.position;
                    dummyBlock.rotation = hitTransform.rotation;
                    dummyBlock.Rotate(0, 0, rotation);

                    Debug.Log("ON TOP" + hitTransform.name);
                }

                dummyBlockScript.isAttached = true;
            }
            else {
                dummyBlock.rotation = transform.rotation;
                dummyBlock.position = transform.position + transform.up;

                //dummyBlock.Rotate(0, 0, rotation);
                dummyBlockScript.isAttached = false;
            }

            if ((dummyBlockScript.isAttached && dummyBlockScript.isAttachable && dummyBlockScript.isNotBlocking) || (dummyBlockScript.isNotBlocking && itemBlock.createsNewShip))
                dummyBlockScript.Buildable(true);
            else
                dummyBlockScript.Buildable(false);
        }

        public void RaycastAction1() {
            if (dummyBlockScript.isAttached && dummyBlockScript.isAttachable && dummyBlockScript.isNotBlocking) {

                if (hitTransform != null) {
                    Vector3 vect = new Vector3();
                    switch (orientation) {
                        case "up":
                            //vect = Vector3.up;
                            vect = hitTransform.root.InverseTransformVector(hitTransform.up); 
                            break;
                        case "down":
                            //vect = Vector3.down;
                            vect = hitTransform.root.InverseTransformVector(-hitTransform.up);
                            //vect = -hitTransform.up;
                            break;
                        case "left":
                            //vect = Vector3.left;
                            vect = hitTransform.root.InverseTransformVector(-hitTransform.right);
                            //vect = -hitTransform.right;
                            break;
                        case "right":
                            //vect = Vector3.right;
                            vect = hitTransform.root.InverseTransformVector(hitTransform.right);
                            //vect = hitTransform.right;
                            break;
                        case "center":
                            vect = Vector3.zero;
                            break;
                        case "ontop":
                            vect = hitTransform.parent.localPosition; ;
                            break;
                        case "betweenUp":
                            vect = hitTransform.root.InverseTransformVector(hitTransform.up)*1/2;

                            //vect = hitTransform.up*1/2; //Vector3.up * 1 / 2;
                            rotation = 0;
                            break;
                        case "betweenDown":
                            vect = -hitTransform.root.InverseTransformVector(hitTransform.up) * 1 / 2;
                            //vect = -hitTransform.up * 1 / 2;
                            //vect = Vector3.down * 1 / 2;
                            rotation = 180;
                            break;
                        case "betweenRight":
                            vect = hitTransform.root.InverseTransformVector(hitTransform.right) * 1 / 2;
                            //vect = hitTransform.right * 1 / 2;
                            //vect = Vector3.right * 1 / 2;
                            rotation = 270;
                            break;
                        case "betweenLeft":
                            vect = -hitTransform.root.InverseTransformVector(hitTransform.right) * 1 / 2;
                            //vect = -hitTransform.right * 1 / 2;
                            //vect = Vector3.left * 1 / 2;
                            rotation = 90;
                            break;
                    }

                    GameObject go = (GameObject)Instantiate(itemBlock.prefab);

                    go.transform.SetParent(hitTransform.root);
                   
                    go.transform.localPosition = hitTransform.localPosition + vect;
                    go.transform.localRotation = hitTransform.localRotation;
                    go.transform.Rotate(0, 0, rotation, Space.Self);

                    go.name = (itemBlock.itemName);
                }
            }
            else {
                if (dummyBlockScript.isNotBlocking && itemBlock.createsNewShip) {
                    GameObject goShip = Instantiate(shipPrefab);
                    GameObject go = Instantiate(itemBlock.prefab);

                    go.transform.SetParent(goShip.transform, false);

                    goShip.transform.position = dummyBlock.transform.position;
                    goShip.transform.rotation = dummyBlock.transform.rotation;

                    go.transform.position = goShip.transform.position;
                    go.transform.rotation = goShip.transform.rotation;

                    //go.transform.rotation = Quaternion.EulerAngles(0, 0, 0);
                    //goShip.transform.rotation = Quaternion.EulerAngles(0, 0, 0);
                }
            }
        }

        public void RaycastAction2() {

            switch (rotation) {
                case 0:
                    rotation = 270;
                    break;
                case 270:
                    rotation = 180;
                    break;
                case 180:
                    rotation = 90;
                    break;
                case 90:
                    rotation = 360;
                    break;
                case 360:
                    rotation = 270;
                    break;
            }
            Debug.Log(rotation);
        }

        public void RaycastAction3() {
            Debug.Log(orientation);
            Debug.Log("Hitpoint:" + asd2);
            Debug.Log("Hitpoint:" + ASD4);
            
        }
    
        

        public void SetItem(ItemBase itemBase) {
            this.itemBlock = (ItemBlock)itemBase;

            switch (this.itemBlock.position) {
                case BlockPosition.CENTER_BOTTOM:
                    layerMask = layerMaskShipBottom;
                    break;
                case BlockPosition.CENTER_MIDDLE:
                    layerMask = layerMaskShipMiddle;
                    break;
                case BlockPosition.CENTER_TOP:
                    layerMask = layerMaskShipTop;
                    break;
                case BlockPosition.CENTER:
                    layerMask = layerMaskShip;
                    break;
                case BlockPosition.BETWEEN:
                    layerMask = layerMaskBetween;
                    break;
                case BlockPosition.CENTER_ONTOP:
                    layerMask = layerMaskShipOnTop;
                    break;
            }

            dummyBlock.GetComponent<BlockDummy>().SetItem(itemBlock,layerMask);

        }

        public void TryPlaceBlock() {
            if (true) {
             

            }
        }

        public void TryPlaceBlockWithBlock() {
            if (true) {
              

            }
        }
    }
}
