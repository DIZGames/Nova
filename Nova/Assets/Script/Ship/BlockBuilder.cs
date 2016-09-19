using Assets.Script.ItemSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class BlockBuilder : MonoBehaviour, IEquippable {

        [SerializeField]
        private LayerMask layerMaskAttachShipBottom;
        [SerializeField]
        private LayerMask layerMaskAttachShipMiddle;
        [SerializeField]
        private LayerMask layerMaskAttachShipTop;
        [SerializeField]
        private LayerMask layerMaskAttachShipOnTop;
        [SerializeField]
        private LayerMask layerMaskAttachShip;
        [SerializeField]
        private LayerMask layerMaskAttachBetween;

        [SerializeField]
        private Transform dummyBlock;
        [SerializeField]
        private BlockDummy dummyBlockScript;

        private GameObject shipPrefab;
        private LayerMask layerAttachMask;
        private Transform hitTransform;
        ItemBlock itemBlock;
        int rotation = 0;
        string orientation;

        void Start() {
            shipPrefab = (GameObject)Resources.Load("Prefab/Ship/Ship");
        }

        void Update() {
            raycasting();
        }

        void FixedUpdate() {
            //raycasting();
        }

        void raycasting() {

            Vector3 hitVect = new Vector3();
            hitTransform = null;

            Debug.DrawRay(transform.position + transform.up * 1 / 2, transform.rotation  * new Vector3(0, 1, 1), Color.magenta, 0.2f);

            dummyBlock.rotation = transform.rotation;

            //dummyBlock.position = transform.position + transform.up;

            Vector3 vectPos = transform.position + transform.up;
            vectPos.z = itemBlock.buildLevel;
            dummyBlock.position = vectPos;

            dummyBlockScript.isAttached = false;

            RaycastHit hit;
            Physics.Raycast(transform.position + transform.up * 1 / 2, transform.rotation * new Vector3(0, 1, 1), out hit,2, layerAttachMask);

            if (hit.collider != null) {

                hitTransform = hit.collider.transform;
                hitVect = hit.collider.transform.InverseTransformPoint(hit.point);

                double x = 0;
                double y = 0;

                x = Math.Round((double)hitVect.x, 1);
                y = Math.Round((double)hitVect.y, 1);

                hitVect.x = (float)x;
                hitVect.y = (float)y;

                if ((itemBlock.position == BlockPosition.CENTER || itemBlock.position == BlockPosition.CENTER_BOTTOM)) {
                    //up
                    if (hitVect.y > Math.Abs(hitVect.x)) {
                        orientation = "up";

                        Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.up);
                        vect.z = itemBlock.buildLevel;
                        dummyBlock.position = vect;

                        dummyBlock.rotation = hitTransform.rotation;
                        dummyBlock.Rotate(0, 0, rotation);
                    }

                    //down
                    if (hitVect.y == -0.5f) {
                        orientation = "down";

                        Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.down);
                        vect.z = itemBlock.buildLevel;
                        dummyBlock.position = vect;

                        dummyBlock.rotation = hitTransform.rotation; ;
                        dummyBlock.Rotate(0, 0, rotation);
                    }

                    //left
                    if (hitVect.x == -0.5f) {
                        orientation = "left";

                        Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.left);
                        vect.z = itemBlock.buildLevel;
                        dummyBlock.position = vect;

                        dummyBlock.rotation = hitTransform.rotation; ;
                        dummyBlock.Rotate(0, 0, rotation);
                    }

                    //right
                    if (hitVect.x == 0.5f) {
                        orientation = "right";

                        Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.right);
                        vect.z = itemBlock.buildLevel;
                        dummyBlock.position = vect;

                        dummyBlock.rotation = hitTransform.rotation; ;
                        dummyBlock.Rotate(0, 0, rotation);
                    }
                }

                if (itemBlock.position == BlockPosition.CENTER_MIDDLE) {
                    orientation = "center";

                    Vector3 vect = hitTransform.position;
                    vect.z = itemBlock.buildLevel;
                    dummyBlock.position = vect;

                    dummyBlock.rotation = hitTransform.rotation;
                    dummyBlock.Rotate(0, 0, rotation);
                }

                if (itemBlock.position == BlockPosition.BETWEEN) {

                    //up
                    if (hitVect.y > Math.Abs(hitVect.x)) {
                        orientation = "betweenUp";

                        Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.up) * 0.46f;
                        vect.z = itemBlock.buildLevel;
                        dummyBlock.position = vect;

                        dummyBlock.rotation = hitTransform.rotation;
                    }
                    //down
                    if (hitVect.y < -Math.Abs(hitVect.x)) {
                        orientation = "betweenDown";

                        Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.down) * 0.46f;
                        vect.z = itemBlock.buildLevel;
                        dummyBlock.position = vect;

                        dummyBlock.rotation = hitTransform.rotation;
                        dummyBlock.Rotate(0, 0, 180);
                    }
                    //right
                    if (hitVect.x > Math.Abs(hitVect.y)) {
                        orientation = "betweenRight";

                        Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.right) * 0.46f;
                        vect.z = itemBlock.buildLevel;
                        dummyBlock.position = vect;

                        dummyBlock.rotation = hitTransform.rotation;
                        dummyBlock.Rotate(0, 0, 270);
                    }
                    //left
                    if (hitVect.x < -Math.Abs(hitVect.y)) {
                        orientation = "betweenLeft";

                        Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.left) * 0.46f;
                        vect.z = itemBlock.buildLevel;
                        dummyBlock.position = vect;

                        dummyBlock.rotation = hitTransform.rotation;
                        dummyBlock.Rotate(0, 0, 90);
                    }
                }

                if (itemBlock.position == BlockPosition.CENTER_TOP){
                    orientation = "top";

                    Vector3 vect = hitTransform.position;
                    vect.z = itemBlock.buildLevel;
                    dummyBlock.position = vect;

                    dummyBlock.rotation = hitTransform.rotation;
                    dummyBlock.Rotate(0, 0, rotation);
                }

                if (itemBlock.position == BlockPosition.CENTER_ONTOP) {
                    orientation = "ontop";

                    Vector3 vect = hitTransform.position;
                    vect.z = itemBlock.buildLevel;
                    dummyBlock.position = vect;

                    dummyBlock.rotation = hitTransform.rotation;
                    dummyBlock.Rotate(0, 0, rotation);
                }

                dummyBlockScript.isAttached = true;
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
                            vect = hitTransform.localPosition + hitTransform.root.InverseTransformVector(hitTransform.up);
                            vect.z = itemBlock.buildLevel;
                            break;
                        case "down":
                            vect = hitTransform.localPosition + hitTransform.root.InverseTransformVector(-hitTransform.up);
                            vect.z = itemBlock.buildLevel;
                            break;
                        case "left":
                            vect = hitTransform.localPosition + hitTransform.root.InverseTransformVector(-hitTransform.right);
                            vect.z = itemBlock.buildLevel;
                            break;
                        case "right":
                            vect = hitTransform.localPosition + hitTransform.root.InverseTransformVector(hitTransform.right);
                            vect.z = itemBlock.buildLevel;
                            break;
                        case "center":
                            vect = hitTransform.localPosition + Vector3.zero;
                            vect.z = itemBlock.buildLevel;
                            break;
                        case "top":
                            vect = hitTransform.localPosition + Vector3.zero;
                            vect.z = itemBlock.buildLevel;
                            break;
                        case "ontop":
                            vect = hitTransform.localPosition + Vector3.zero;
                            vect.z = itemBlock.buildLevel;
                            break;
                        case "betweenUp":
                            vect = hitTransform.localPosition + hitTransform.root.InverseTransformVector(hitTransform.up) * 0.46f;
                            vect.z = itemBlock.buildLevel;
                            rotation = 0;
                            break;
                        case "betweenDown":
                            vect = hitTransform.localPosition - hitTransform.root.InverseTransformVector(hitTransform.up) * 0.46f;
                            vect.z = itemBlock.buildLevel;
                            rotation = 180;
                            break;
                        case "betweenRight":
                            vect = hitTransform.localPosition + hitTransform.root.InverseTransformVector(hitTransform.right) * 0.46f;
                            vect.z = itemBlock.buildLevel;
                            rotation = 270;
                            break;
                        case "betweenLeft":
                            vect = hitTransform.localPosition - hitTransform.root.InverseTransformVector(hitTransform.right) * 0.46f;
                            vect.z = itemBlock.buildLevel;
                            rotation = 90;
                            break;
                    }

                    GameObject go = (GameObject)Instantiate(itemBlock.prefab);

                    go.transform.SetParent(hitTransform.root);
                   
                    go.transform.localPosition = vect;
                    go.transform.localRotation = hitTransform.localRotation;
                    go.transform.Rotate(0, 0, rotation, Space.Self);

                    go.name = (itemBlock.itemName);
                }
            }
            else { // New Ship
                if (dummyBlockScript.isNotBlocking && itemBlock.createsNewShip) {
                    GameObject goShip = Instantiate(shipPrefab);
                    GameObject go = Instantiate(itemBlock.prefab);

                    go.transform.SetParent(goShip.transform, false);

                    goShip.transform.position = dummyBlock.transform.position;
                    goShip.transform.rotation = dummyBlock.transform.rotation;

                    go.transform.position = goShip.transform.position;
                    go.transform.rotation = goShip.transform.rotation;

                    Vector3 vectPos = goShip.transform.position;
                    vectPos.z = 0;
                    goShip.transform.position = vectPos;

                    Vector3 vect = go.transform.position;
                    vect.z = itemBlock.buildLevel;
                    go.transform.position = vect;



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

            
        }
    
        

        public void SetItem(ItemBase itemBase) {
            this.itemBlock = (ItemBlock)itemBase;

            switch (this.itemBlock.position) {
                case BlockPosition.CENTER_BOTTOM:
                    layerAttachMask = layerMaskAttachShipBottom;
                    break;
                case BlockPosition.CENTER_MIDDLE:
                    layerAttachMask = layerMaskAttachShipMiddle;
                    break;
                case BlockPosition.CENTER_TOP:
                    layerAttachMask = layerMaskAttachShipTop;
                    break;
                case BlockPosition.CENTER:
                    layerAttachMask = layerMaskAttachShip;
                    break;
                case BlockPosition.BETWEEN:
                    layerAttachMask = layerMaskAttachBetween;
                    break;
                case BlockPosition.CENTER_ONTOP:
                    layerAttachMask = layerMaskAttachShipOnTop;
                    break;
            }

            dummyBlock.GetComponent<BlockDummy>().SetItem(itemBlock);

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
