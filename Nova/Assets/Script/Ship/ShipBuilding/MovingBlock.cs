using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MovingBlock : MonoBehaviour {

    new Transform transform;
    new Collider2D collider;
    SpriteRenderer spriteRenderer;
    BlockPosition blockPosition;
    bool createsNewShip;
    float boxWidth = 0.2f; // Defines the width of the box in which the mouse position is checked for parts that can be set between Blocks (like Walls)
    int layerMaskBlock = LayerMask.GetMask("Block") | LayerMask.GetMask("BlockFloor");

    void Start()
    {
        transform = gameObject.transform;
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        IBlock iShipPart = GetComponent<IBlock>();
        blockPosition = iShipPart.position;
        createsNewShip = iShipPart.createsNewShip;

        collider.enabled = false;
        Color color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;
    }

    void OnDestroy()
    {
        collider.enabled = true;
        Color color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] collidersAtMousePos = Physics2D.OverlapAreaAll(new Vector2(mousePos.x - 0.5f, mousePos.y + 0.5f), new Vector2(mousePos.x + 0.5f, mousePos.y - 0.5f), layerMaskBlock);
        Transform newParent = null;
        foreach (Collider2D c in collidersAtMousePos)
        {
            if (c.gameObject != gameObject)
            {
                newParent = c.gameObject.transform.parent;
            }
        }

        Vector3 newPos;

        if (newParent != null)
        {
            if (transform.parent != newParent)
                transform.parent = newParent;
            Quaternion newRotation = new Quaternion(0f, 0f, newParent.rotation.z, newParent.rotation.w);
            transform.rotation = newRotation;

            Vector3 mousePosLocal = newParent.InverseTransformPoint(mousePos);
            float x = mousePosLocal.x - (int)mousePosLocal.x;
            float y = mousePosLocal.y - (int)mousePosLocal.y;
            if (blockPosition == BlockPosition.CENTER_FLOOR || blockPosition == BlockPosition.CENTER_TOP || blockPosition == BlockPosition.CENTER)
            {
                // Calculate x Position
                if (x >= 0)
                {
                    if (x >= 0.5)
                        x = (int)mousePosLocal.x + 1;
                    else
                        x = (int)mousePosLocal.x;
                }
                else
                {
                    if (x <= -0.5)
                        x = (int)mousePosLocal.x - 1;
                    else
                        x = (int)mousePosLocal.x;
                }

                // Calculate y Position
                if (y >= 0)
                {
                    if (y >= 0.5)
                        y = (int)mousePosLocal.y + 1;
                    else
                        y = (int)mousePosLocal.y;
                }
                else
                {
                    if (y <= -0.5)
                        y = (int)mousePosLocal.y - 1;
                    else
                        y = (int)mousePosLocal.y;
                }
            }
            else if (blockPosition == BlockPosition.BETWEEN_TOP || blockPosition == BlockPosition.BETWEEN_FLOOR || blockPosition == BlockPosition.BETWEEN)
            {
                if (Mathf.Abs(x) >= (0.5 - boxWidth) && Mathf.Abs(x) <= (0.5 + boxWidth))
                {
                    if(x >= 0)
                        x = (int)mousePosLocal.x + 0.5f;
                    else
                        x = (int)mousePosLocal.x - 0.5f;
                    if (y >= 0)
                    {
                        if(y <= 0.5f)
                            y = (int)mousePosLocal.y;
                        else
                            y = (int)mousePosLocal.y + 1;
                    }
                    else
                    {
                        y = Mathf.Abs(y);
                        if (y <= 0.5f)
                            y = (int)mousePosLocal.y;
                        else
                            y = (int)mousePosLocal.y - 1;
                    }
                    transform.Rotate(0, 0, 90);
                }
                else if (Mathf.Abs(y) >= 0.3 && Mathf.Abs(y) <= 0.7)
                {
                    if (x >= 0)
                    {
                        if (x <= 0.5f)
                            x = (int)mousePosLocal.x;
                        else
                            x = (int)mousePosLocal.x + 1;
                    }
                    else
                    {
                        x = Mathf.Abs(x);
                        if (x <= 0.5f)
                            x = (int)mousePosLocal.x;
                        else
                            x = (int)mousePosLocal.x - 1;
                    }
                    if (y >= 0)
                        y = (int)mousePosLocal.y + 0.5f;
                    else
                        y = (int)mousePosLocal.y - 0.5f;
                }
            }

            newPos = new Vector3(x, y, transform.position.z);
            transform.localPosition = newPos;
        }
        else
        {
            if (transform.parent != null)
                transform.SetParent(null);
            newPos = new Vector3(mousePos.x, mousePos.y, transform.position.z);
            transform.position = newPos;
            transform.rotation = Quaternion.identity;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            bool canBuild = true;
            if(newParent != null || createsNewShip)
            {
                Collider2D[] objects = Physics2D.OverlapAreaAll(new Vector2(newPos.x - 0.49f, newPos.y + 0.49f), new Vector2(newPos.x + 0.49f, newPos.y - 0.49f));

                //foreach (Collider2D c in objects)
                //{
                //    GameObject g = c.gameObject;
                //    IBlock gBlock;

                //    gBlock = g.GetComponent<IBlock>();
                //    bool isPlayer = g.layer == LayerMask.GetMask("Player");

                //    if (gBlock != null || isPlayer)
                //    {
                //        if (isPlayer && (gBlock.position == BlockPosition.BETWEEN_FLOOR) || gBlock.position == BlockPosition.CENTER_FLOOR)
                //        {
                //            canBuild = true;
                //        }
                //    }


                //    if (g.layer != LayerMask.GetMask("Block") && g.layer != LayerMask.GetMask("BlockFloor") && g.layer != LayerMask.GetMask("Player"))
                //        canBuild = false;
                //    else
                //    {
                //        switch (blockPosition)
                //        {
                //            case BlockPosition.CENTER:
                //                if (g.layer == LayerMask.GetMask("Block") || g.layer == LayerMask.GetMask("BlockFloor") || g.layer == LayerMask.GetMask("Player"))
                //                    canBuild = false;
                //                break;
                //            case BlockPosition.CENTER_FLOOR:
                //                if (g.layer == LayerMask.GetMask("Block") || g.layer == LayerMask.GetMask("BlockFloor"))
                //                    canBuild = false;
                //                break;
                //            case BlockPosition.CENTER_TOP:
                //                if (g.layer == LayerMask.GetMask("Block") || g.layer == LayerMask.GetMask("Player"))
                //                    canBuild = false;
                //                break;
                //            case BlockPosition.BETWEEN:
                //                break;
                //            case BlockPosition.BETWEEN_FLOOR:
                //                break;
                //            case BlockPosition.BETWEEN_TOP:
                //                break;
                //        }
                //    }
                //}




                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if ((hit.collider == null && (blockPosition == BlockPosition.CENTER || blockPosition == BlockPosition.CENTER_FLOOR)
                    || (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("BlockFloor") && (blockPosition == BlockPosition.BETWEEN_TOP || blockPosition == BlockPosition.CENTER_TOP))))
                {
                    Global.objectToMove = null;
                    if (newParent == null)
                    {
                        GameObject newShip = Instantiate(Global.shipPrefab, transform.position, transform.rotation) as GameObject;
                        transform.SetParent(newShip.transform);
                    }
                    Destroy(this);
                }
            }

            //if (canBuild)
            //{
            //    Global.objectToMove = null;
            //    if (newParent == null)
            //    {
            //        GameObject newShip = Instantiate(Global.shipPrefab, transform.position, transform.rotation) as GameObject;
            //        transform.SetParent(newShip.transform);
            //    }
            //    Destroy(this);
            //}

        } else if(Input.GetButtonDown("Fire2"))
        {
            Destroy(gameObject);
        }

    }
}
