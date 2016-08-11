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
    bool isCenterBlock;
    Vector2 spriteSize = new Vector2();

    void Start()
    {
        transform = gameObject.transform;
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Block iShipPart = GetComponent<Block>();
        blockPosition = iShipPart.Position;
        createsNewShip = iShipPart.CreatesNewShip;
        isCenterBlock = blockPosition.ToString().StartsWith("C"); //is blockPosition starts with C it is a Center block
        spriteSize.x = spriteRenderer.bounds.size.x;
        spriteSize.y = spriteRenderer.bounds.size.y;

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
            if (isCenterBlock)
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
            else
            {
                // Between
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
            Collider2D[] objects = Physics2D.OverlapAreaAll(new Vector2(newPos.x - 0.49f, newPos.y + 0.49f), new Vector2(newPos.x + 0.49f, newPos.y - 0.49f));
            bool canBuild = newParent != null || createsNewShip;

            foreach (Collider2D c in objects)
            {
                GameObject g = c.gameObject;

                if (g.layer == LayerMask.GetMask("Player"))
                    continue;

                Block gBlock = g.GetComponent<Block>();
                    
            }




                //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                //if ((hit.collider == null && (blockPosition == BlockPosition.CENTER || blockPosition == BlockPosition.CENTER_FLOOR)
                //    || (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("BlockFloor") && (blockPosition == BlockPosition.BETWEEN_TOP || blockPosition == BlockPosition.CENTER_TOP))))
                //{
                //    Global.objectToMove = null;
                //    if (newParent == null)
                //    {
                //        GameObject newShip = Instantiate(Global.shipPrefab, transform.position, transform.rotation) as GameObject;
                //        transform.SetParent(newShip.transform);
                //    }
                //    Destroy(this);
                //}

            if (canBuild)
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
        else if(Input.GetButtonDown("Fire2"))
        {
            Destroy(gameObject);
        }

    }
}
