using UnityEngine;
using System.Collections;
using Assets.Script;
using System;
using Assets.Script.ItemSystem;
using System.Collections.Generic;

public class BlockSetter : MonoBehaviour {

    [SerializeField]
    private BlockDummy dummyBlock;

    private ItemBlock itemBlock;
    int rotation = 0;
    string orientation;

    public void SetItem(ItemBlock itemBlock)
    {
        this.itemBlock = itemBlock;
        dummyBlock.SetItem(itemBlock);
    }

    void Start() {
        //InvokeRepeating("Raycasting", 1, 1);
    }

    void Update() {
        if (Input.GetButtonDown("Fire1"))
        {
            Action1();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Action2();
        }

        Raycasting();
    }

    private Transform hitTransform;

    void Raycasting() {
      

        Debug.DrawRay(transform.position, transform.rotation * transform.up * 0.4f, Color.magenta, 0.2f);
        Debug.DrawRay(transform.position, transform.rotation * -transform.up * 0.4f, Color.magenta, 0.2f);
        Debug.DrawRay(transform.position, transform.rotation * transform.right * 0.4f, Color.magenta, 0.2f);
        Debug.DrawRay(transform.position, transform.rotation * -transform.right * 0.4f, Color.magenta, 0.2f);

        Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        vector3.z = itemBlock.buildLevel;

        transform.position = vector3;
        dummyBlock.transform.position = vector3;

        RaycastHit hit1;
        Physics.Raycast(transform.position, transform.rotation * transform.up * 0.4f, out hit1, 0.4f);
        RaycastHit hit2;
        Physics.Raycast(transform.position, transform.rotation * -transform.up * 0.4f, out hit2, 0.4f);
        RaycastHit hit3;
        Physics.Raycast(transform.position, transform.rotation * transform.right * 0.4f, out hit3, 0.4f);
        RaycastHit hit4;
        Physics.Raycast(transform.position, transform.rotation * -transform.right * 0.4f, out hit4, 0.4f);

        RaycastHit hitDummy = new RaycastHit();

        List<RaycastHit> listDistance = new List<RaycastHit>();
        if (hit1.collider != null) {
            listDistance.Add(hit1);
            hitDummy = hit1;
            Debug.Log("hit1");
        }
        if (hit2.collider != null) {
            listDistance.Add(hit2);
            hitDummy = hit2;
            Debug.Log("hit2");
        }       
        if (hit3.collider != null) {
            listDistance.Add(hit3);
            hitDummy = hit3;
            Debug.Log("hit3");
        }
        if (hit4.collider != null){
            listDistance.Add(hit4);
            hitDummy = hit4;
            Debug.Log("hit4");
        }




        //ungetester CCode für das finden des kürzesten Raycast
        //RaycastHit hit = new RaycastHit();
        //int min = 0;
        //for (int i = 0; i < listDistance.Count; i++)
        //{
        //    if (listDistance[i].distance < hit.distance)
        //    {
        //        hit = listDistance[i];
        //        min = i;
        //    }
        //}

        //if (min == 0)
        //    Debug.Log("Oben");
        //if (min == 1)
        //    Debug.Log("Unten");
        //if (min == 2)
        //    Debug.Log("Rechts");
        //if (min == 3)
        //    Debug.Log("Links");

        hitTransform = null;
        Vector3 hitVect = new Vector3();
        dummyBlock.isAttached = false;

        if (hitDummy.collider != null)
        {

            hitTransform = hitDummy.collider.transform;
            hitVect = hitDummy.collider.transform.InverseTransformPoint(hitDummy.point);

            double x = 0;
            double y = 0;

            x = Math.Round((double)hitVect.x, 1);
            y = Math.Round((double)hitVect.y, 1);

            hitVect.x = (float)x;
            hitVect.y = (float)y;

            if ((itemBlock.position == BlockPosition.CENTER || itemBlock.position == BlockPosition.CENTER_BOTTOM))
            {
                //up
                if (hitVect.y > Math.Abs(hitVect.x))
                {
                    orientation = "up";

                    Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.up);
                    vect.z = itemBlock.buildLevel;
                    dummyBlock.transform.position = vect;

                    dummyBlock.transform.rotation = hitTransform.rotation;
                    dummyBlock.transform.Rotate(0, 0, rotation);
                }

                //down
                if (hitVect.y == -0.5f)
                {
                    orientation = "down";

                    Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.down);
                    vect.z = itemBlock.buildLevel;
                    dummyBlock.transform.position = vect;

                    dummyBlock.transform.rotation = hitTransform.rotation; ;
                    dummyBlock.transform.Rotate(0, 0, rotation);
                }

                //left
                if (hitVect.x == -0.5f)
                {
                    orientation = "left";

                    Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.left);
                    vect.z = itemBlock.buildLevel;
                    dummyBlock.transform.position = vect;

                    dummyBlock.transform.rotation = hitTransform.rotation; ;
                    dummyBlock.transform.Rotate(0, 0, rotation);
                }

                //right
                if (hitVect.x == 0.5f)
                {
                    orientation = "right";

                    Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.right);
                    vect.z = itemBlock.buildLevel;
                    dummyBlock.transform.position = vect;

                    dummyBlock.transform.rotation = hitTransform.rotation; ;
                    dummyBlock.transform.Rotate(0, 0, rotation);
                }
            }

            if (itemBlock.position == BlockPosition.CENTER_MIDDLE)
            {
                orientation = "center";

                Vector3 vect = hitTransform.position;
                vect.z = itemBlock.buildLevel;
                dummyBlock.transform.position = vect;

                dummyBlock.transform.rotation = hitTransform.rotation;
                dummyBlock.transform.Rotate(0, 0, rotation);
            }

            if (itemBlock.position == BlockPosition.BETWEEN)
            {

                //up
                if (hitVect.y > Math.Abs(hitVect.x))
                {
                    orientation = "betweenUp";

                    Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.up) * 0.46f;
                    vect.z = itemBlock.buildLevel;
                    dummyBlock.transform.position = vect;

                    dummyBlock.transform.rotation = hitTransform.rotation;
                }
                //down
                if (hitVect.y < -Math.Abs(hitVect.x))
                {
                    orientation = "betweenDown";

                    Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.down) * 0.46f;
                    vect.z = itemBlock.buildLevel;
                    dummyBlock.transform.position = vect;

                    dummyBlock.transform.rotation = hitTransform.rotation;
                    dummyBlock.transform.Rotate(0, 0, 180);
                }
                //right
                if (hitVect.x > Math.Abs(hitVect.y))
                {
                    orientation = "betweenRight";

                    Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.right) * 0.46f;
                    vect.z = itemBlock.buildLevel;
                    dummyBlock.transform.position = vect;

                    dummyBlock.transform.rotation = hitTransform.rotation;
                    dummyBlock.transform.Rotate(0, 0, 270);
                }
                //left
                if (hitVect.x < -Math.Abs(hitVect.y))
                {
                    orientation = "betweenLeft";

                    Vector3 vect = hitTransform.position + hitTransform.TransformVector(Vector2.left) * 0.46f;
                    vect.z = itemBlock.buildLevel;
                    dummyBlock.transform.position = vect;

                    dummyBlock.transform.rotation = hitTransform.rotation;
                    dummyBlock.transform.Rotate(0, 0, 90);
                }
            }

            if (itemBlock.position == BlockPosition.CENTER_TOP)
            {
                orientation = "top";

                Vector3 vect = hitTransform.position;
                vect.z = itemBlock.buildLevel;
                dummyBlock.transform.position = vect;

                dummyBlock.transform.rotation = hitTransform.rotation;
                dummyBlock.transform.Rotate(0, 0, rotation);
            }

            if (itemBlock.position == BlockPosition.CENTER_ONTOP)
            {
                orientation = "ontop";

                Vector3 vect = hitTransform.position;
                vect.z = itemBlock.buildLevel;
                dummyBlock.transform.position = vect;

                dummyBlock.transform.rotation = hitTransform.rotation;
                dummyBlock.transform.Rotate(0, 0, rotation);
            }

            dummyBlock.isAttached = true;
        }


        if ((dummyBlock.isAttached && dummyBlock.isAttachable && dummyBlock.isNotBlocking && !itemBlock.createsNewShip
            ) || (dummyBlock.isNotBlocking && itemBlock.createsNewShip && !dummyBlock.isAttached))
            dummyBlock.Buildable(true);
        else
            dummyBlock.Buildable(false);

    }

    public void Action1()
    {

        if (dummyBlock.isAttached && dummyBlock.isAttachable && dummyBlock.isNotBlocking)
        {
            if (hitTransform != null)
            {
                Vector3 vect = new Vector3();
                switch (orientation)
                {
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

                GameObject go = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(itemBlock.prefab);

                go.transform.SetParent(hitTransform.root);

                go.transform.localPosition = vect;
                go.transform.localRotation = hitTransform.localRotation;
                go.transform.Rotate(0, 0, rotation, Space.Self);

                go.name = (itemBlock.itemName);
            }
        }
        else
        { // New Ship :: OBSOLETE, because of new Buildmenu
            //if (dummyBlock.isNotBlocking && itemBlock.createsNewShip)
            //{
            //    GameObject goShip = (GameObject)Instantiate(shipPrefab);
            //    GameObject go = (GameObject)Instantiate(itemBlock.prefab);

            //    go.transform.SetParent(goShip.transform, false);

            //    goShip.transform.position = dummyBlock.transform.position;
            //    goShip.transform.rotation = dummyBlock.transform.rotation;

            //    go.transform.position = goShip.transform.position;
            //    go.transform.rotation = goShip.transform.rotation;

            //    Vector3 vectPos = goShip.transform.position;
            //    vectPos.z = 0;
            //    goShip.transform.position = vectPos;

            //    Vector3 vect = go.transform.position;
            //    vect.z = itemBlock.buildLevel;
            //    go.transform.position = vect;



            //}
        }
    }

    public void Action2()
    {

        switch (rotation)
        {
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

    public void Action3()
    {
        Debug.Log(orientation);


    }

}
