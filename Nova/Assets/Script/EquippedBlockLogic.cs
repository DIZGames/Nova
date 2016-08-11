using Assets.Script.ItemSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    
    public class EquippedBlockLogic : MonoBehaviour, IEquippable {

        Transform player;
        new Transform transform;
        ItemBlockValues itemBlockValues;
        float boxWidth = 0.2f; // Defines the width of the box in which the mouse position is checked for parts that can be set between Blocks (like Walls)
        int layerMaskBlock;
        Transform shipTransform;
        public Transform dummyBlock;
        Vector2 spriteSize;
        float rayCastLength = 2f;
        

        void Start() {
            transform = GetComponent<Transform>();
            layerMaskBlock = LayerMask.GetMask("Block") | LayerMask.GetMask("BlockFloor");
            // Muss irgendwann gegen eine bessere Möglichkeit ausgetauscht werden, wie man an den Player kommt
            player = GameObject.Find("Player").transform;
        }

        public void init()
        {
            //Kann nicht in die Start Methode, da der Sprite dann noch nicht gesetzt ist
            SpriteRenderer spriteRenderer = dummyBlock.GetComponent<SpriteRenderer>();
            spriteSize = new Vector2(spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y);
            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;

            //TODO: Collider Punkte für komplexere Formen setzen (z.B. T Form)
            //collider.points = new[] {new Vector2()};
            //TODO: rayCastLength abhängig vom Sprite berechnen
            //rayCastLength = 
        }

        void Update()
        {

            RaycastHit2D hit2D = Physics2D.Raycast(player.position, player.up, rayCastLength, layerMaskBlock);
            if (hit2D.collider != null)
            {
                shipTransform = hit2D.transform;
                dummyBlock.rotation = shipTransform.rotation;
                dummyBlock.parent = shipTransform;
                Vector3 currentPosLocal = shipTransform.InverseTransformPoint(transform.position);
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

                Vector3 newPos = new Vector3(x, y, transform.position.z);
                dummyBlock.localPosition = newPos;
                
            }
            else
            {
                shipTransform = null;
                dummyBlock.parent = transform.parent;
                dummyBlock.rotation = transform.rotation;
                dummyBlock.localPosition = Vector3.zero;
            }
        }

        public void setItemValues(ItemValues itemValues) {
            itemBlockValues = (ItemBlockValues)itemValues;
        }

        public void Action1() {
            if (itemBlockValues.stack > 0) {

                GameObject blockGObject = Instantiate(itemBlockValues.Prefab);
                Transform blockTransform = blockGObject.transform;

                blockTransform.position = dummyBlock.position;
                blockTransform.transform.rotation = dummyBlock.rotation;
                blockTransform.parent = dummyBlock.parent;

                blockGObject.name = itemBlockValues.Name;

                ItemBlockValues newItemBlockValues = ScriptableObject.CreateInstance<ItemBlockValues>();
                newItemBlockValues.CopyValues(itemBlockValues);

                blockGObject.GetComponent<Block>().ItemBlockValues = newItemBlockValues;

                //Collider2D[] objects = Physics2D.OverlapAreaAll(new Vector2(newPos.x - 0.49f, newPos.y + 0.49f), new Vector2(newPos.x + 0.49f, newPos.y - 0.49f));
                //bool canBuild = newParent != null || createsNewShip;

                //foreach (Collider2D c in objects)
                //{
                //    GameObject g = c.gameObject;

                //    if (g.layer == LayerMask.GetMask("Player"))
                //        continue;

                //    IBlock gBlock = g.GetComponent<IBlock>();

                //}

                //if (canBuild)
                //{
                //    if (newParent == null)
                //    {
                //        GameObject newShip = Instantiate(Global.shipPrefab, transform.position, transform.rotation) as GameObject;
                //        transform.SetParent(newShip.transform);
                //    }
                //    Destroy(this);
                //}

                itemBlockValues.stack--;

                if (itemBlockValues.stack <= 0) {
                    Destroy(transform.parent.gameObject);
                    Destroy(dummyBlock.gameObject);
                }

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

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(player.position, player.up * rayCastLength);
        }

    }
}
