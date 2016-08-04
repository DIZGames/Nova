using Assets.Script.ItemSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class EquippedBlockLogic : MonoBehaviour, IEquippable {

        ItemBlockValues itemBlockValues;
        new Collider2D collider;
        float boxWidth = 0.2f; // Defines the width of the box in which the mouse position is checked for parts that can be set between Blocks (like Walls)
        int layerMaskBlock;
        Vector2 spriteSize;
        Transform shipTransform;
        Vector3 localBlockSpawnPos;

        void Start() {
            layerMaskBlock = LayerMask.GetMask("Block") | LayerMask.GetMask("BlockFloor");
            collider = GetComponent<Collider2D>();
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteSize = new Vector2(spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y);
            localBlockSpawnPos = transform.localPosition;
            collider.enabled = false;
        }

        void Update()
        {
            Vector3 currentPosition = gameObject.transform.position;
            Collider2D[] collidersAtPos = Physics2D.OverlapAreaAll(new Vector2(currentPosition.x - 0.5f, currentPosition.y + 0.5f), new Vector2(currentPosition.x + 0.5f, currentPosition.y - 0.5f), layerMaskBlock);
            shipTransform = null;
            foreach (Collider2D c in collidersAtPos)
            {
                if (c.gameObject != gameObject && c.gameObject.GetComponent(typeof(IBlock)))
                {
                    shipTransform = c.transform.parent;
                }
            }

            Vector3 newPos;

            if (shipTransform != null)
            {
                Quaternion newRotation = new Quaternion(0f, 0f, shipTransform.rotation.z, shipTransform.rotation.w);
                transform.rotation = newRotation;

                Vector3 currentPosLocal = shipTransform.InverseTransformPoint(currentPosition);
                float x = currentPosLocal.x - (int)currentPosLocal.x;
                float y = currentPosLocal.y - (int)currentPosLocal.y;
                if (IsCenterBlock())
                {
                    // Calculate x Position
                    if (x >= 0)
                    {
                        if (x >= 0.5)
                            x = (int)currentPosLocal.x + 1;
                        else
                            x = (int)currentPosLocal.x;
                    }
                    else
                    {
                        if (x <= -0.5)
                            x = (int)currentPosLocal.x - 1;
                        else
                            x = (int)currentPosLocal.x;
                    }

                    // Calculate y Position
                    if (y >= 0)
                    {
                        if (y >= 0.5)
                            y = (int)currentPosLocal.y + 1;
                        else
                            y = (int)currentPosLocal.y;
                    }
                    else
                    {
                        if (y <= -0.5)
                            y = (int)currentPosLocal.y - 1;
                        else
                            y = (int)currentPosLocal.y;
                    }
                }
                else if (IsBetweenBlock())
                {
                    // Between
                    if (Mathf.Abs(x) >= (0.5 - boxWidth) && Mathf.Abs(x) <= (0.5 + boxWidth))
                    {
                        if (x >= 0)
                            x = (int)currentPosLocal.x + 0.5f;
                        else
                            x = (int)currentPosLocal.x - 0.5f;
                        if (y >= 0)
                        {
                            if (y <= 0.5f)
                                y = (int)currentPosLocal.y;
                            else
                                y = (int)currentPosLocal.y + 1;
                        }
                        else
                        {
                            y = Mathf.Abs(y);
                            if (y <= 0.5f)
                                y = (int)currentPosLocal.y;
                            else
                                y = (int)currentPosLocal.y - 1;
                        }
                        transform.Rotate(0, 0, 90);
                    }
                    else if (Mathf.Abs(y) >= 0.3 && Mathf.Abs(y) <= 0.7)
                    {
                        if (x >= 0)
                        {
                            if (x <= 0.5f)
                                x = (int)currentPosLocal.x;
                            else
                                x = (int)currentPosLocal.x + 1;
                        }
                        else
                        {
                            x = Mathf.Abs(x);
                            if (x <= 0.5f)
                                x = (int)currentPosLocal.x;
                            else
                                x = (int)currentPosLocal.x - 1;
                        }
                        if (y >= 0)
                            y = (int)currentPosLocal.y + 0.5f;
                        else
                            y = (int)currentPosLocal.y - 0.5f;
                    }
                }

                newPos = new Vector3(x, y, transform.position.z);
                transform.localPosition = newPos;
            }
            else
            {
                //if (transform.parent != null)
                //    transform.SetParent(null);
                //newPos = new Vector3(currentPosition.x, currentPosition.y, transform.position.z);
                ////transform.position = newPos;
                //transform.rotation = Quaternion.identity;
            }
        }

        public void setItemValues(ItemValues itemValues) {
            itemBlockValues = (ItemBlockValues)itemValues;
        }

        public void Action1() {
            if (itemBlockValues.stack > 0) {

                //GameObject go = Instantiate(itemBlockValues.Prefab);

                //go.transform.position = transform.position;
                //go.transform.rotation = transform.rotation;

                //go.name = itemBlockValues.Name;

                //go.GetComponent<EquippedBlockLogic>().setItemValues(itemBlockValues); // Problem??

                //RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.zero);
                //if ((hit.collider == null && (itemBlockValues.BlockPosition == BlockPosition.CENTER || itemBlockValues.BlockPosition == BlockPosition.CENTER_BOTTOM)
                //    || (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("BlockFloor") && (itemBlockValues.BlockPosition == BlockPosition.BETWEEN_TOP
                //    || itemBlockValues.BlockPosition == BlockPosition.CENTER_TOP))))
                //{
                //    Global.objectToMove = null;
                //    if (newParent == null)
                //    {
                //        GameObject newShip = Instantiate(Global.shipPrefab, transform.position, transform.rotation) as GameObject;
                //        transform.SetParent(newShip.transform);
                //    }
                //    Destroy(this);
                //}

                //if (itemBlockValues.stack == 1) {
                //    Destroy(gameObject);
                //}

                //itemBlockValues.stack--;

            }
        }

        public void Action2() {
           
        }

        public void Action3() {
            
        }

        private bool IsCenterBlock()
        {
            BlockPosition bPosition = itemBlockValues.BlockPosition;
            return bPosition == BlockPosition.CENTER || bPosition == BlockPosition.CENTER_BOTTOM || bPosition == BlockPosition.CENTER_MIDDLE
                || bPosition == BlockPosition.CENTER_TOP;
        }

        private bool IsBetweenBlock()
        {
            BlockPosition bPosition = itemBlockValues.BlockPosition;
            return bPosition == BlockPosition.BETWEEN || bPosition == BlockPosition.BETWEEN_FLOOR || bPosition == BlockPosition.BETWEEN_MIDDLE
                || bPosition == BlockPosition.BETWEEN_TOP;
        }

    }
}
