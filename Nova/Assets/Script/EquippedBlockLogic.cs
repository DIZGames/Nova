using Assets.Script.ItemSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//namespace Assets.Script {

//    public class EquippedBlockLogic : MonoBehaviour, IEquippable {

//        Transform shipTransform;
//        new Transform transform;
//        public Transform dummyBlockTransform;
//        GameObject shipPrefab;
//        ItemBlock itemBlock;
//        float boxWidth = 0.2f; // Defines the width of the box in which the mouse position is checked for parts that can be set between Blocks (like Walls)
//        int layerMaskBlock;
//        Vector2 spriteSize;

//        void Start() {
//            transform = GetComponent<Transform>();

//            layerMaskBlock = LayerMask.GetMask("Block") | LayerMask.GetMask("BlockFloor");

//            shipPrefab = (GameObject)Resources.Load("Prefab/Ship/Ship");

//            SpriteRenderer spriteRenderer = dummyBlockTransform.GetComponent<SpriteRenderer>();
//            spriteSize = new Vector2(spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y);
//            Color color = spriteRenderer.color;
//            color.a = 0.5f;
//            spriteRenderer.color = color;

//            //TODO: Collider Punkte für komplexere Formen setzen (z.B. T Form)
//            //collider.points = new[] {new Vector2()};

//            // spriteSize ist zu groß, warum auch immer
//            //collider.size = new Vector2(spriteSize.x - 0.01f, spriteSize.y - 0.01f);
//        }

//        //public void init()
//        //{
//        //    //Kann nicht in die Start Methode, da der Sprite dann noch nicht gesetzt ist
//        //    SpriteRenderer spriteRenderer = dummyBlock.GetComponent<SpriteRenderer>();
//        //    spriteSize = new Vector2(spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y);
//        //    Color color = spriteRenderer.color;
//        //    color.a = 0.5f;
//        //    spriteRenderer.color = color;
//        //    //TODO: Collider Punkte für komplexere Formen setzen (z.B. T Form)
//        //    //collider.points = new[] {new Vector2()};
//        //    collider.size = new Vector2(spriteSize.x - 0.01f, spriteSize.y - 0.01f);

//        //}

//        void Update()
//        {
//            List<RaycastHit2D> hits = new List<RaycastHit2D>();
//            hits.AddRange(Physics2D.RaycastAll(transform.position, transform.rotation * Vector2.up, spriteSize.y / 2, layerMaskBlock));
            
//            hits.AddRange(Physics2D.RaycastAll(transform.position, transform.rotation * Vector2.left, spriteSize.y / 2, layerMaskBlock));
//            hits.AddRange(Physics2D.RaycastAll(transform.position, transform.rotation * Vector2.right, spriteSize.x / 2, layerMaskBlock));
//            hits.AddRange(Physics2D.RaycastAll(transform.position, transform.rotation * Vector2.down, spriteSize.x / 2, layerMaskBlock));

//            shipTransform = null;

//            //Debug.DrawRay(transform.position, transform.rotation * Vector2.up, Color.green , 1);
//            //Debug.DrawRay(transform.position, transform.rotation * Vector2.left, Color.green, 1);
//            //Debug.DrawRay(transform.position, transform.rotation * Vector2.right, Color.green, 1);
//            //Debug.DrawRay(transform.position, transform.rotation * Vector2.down, Color.green, 1);

//            float distance = -1;
//            foreach(RaycastHit2D hit in hits)
//            {
//                if (hit.transform == transform)
//                    continue;
//                else if (distance == -1)
//                {
//                    distance = hit.distance;
//                    shipTransform = hit.transform;
//                }
//                else if (hit.distance < distance)
//                {
//                    distance = hit.distance;
//                    shipTransform = hit.transform;
//                }

//            }

//            if (shipTransform != null)
//            {
//                dummyBlockTransform.rotation = shipTransform.rotation;
//                dummyBlockTransform.parent = shipTransform;

//                Vector3 currentPosLocal = shipTransform.InverseTransformPoint(transform.position);
//                float x = currentPosLocal.x - (int)currentPosLocal.x;
//                float y = currentPosLocal.y - (int)currentPosLocal.y;

//                Vector3 newPos = Vector3.zero;
//                if (itemBlock.IsCenter())
//                {
//                    // Calculate x Position
//                    if (x >= 0)
//                    {
//                        if (x >= 0.5)
//                            x = (int)currentPosLocal.x + 1;
//                        else
//                            x = (int)currentPosLocal.x;
//                    }
//                    else
//                    {
//                        if (x <= -0.5)
//                            x = (int)currentPosLocal.x - 1;
//                        else
//                            x = (int)currentPosLocal.x;
//                    }

//                    // Calculate y Position
//                    if (y >= 0)
//                    {
//                        if (y >= 0.5)
//                            y = (int)currentPosLocal.y + 1;
//                        else
//                            y = (int)currentPosLocal.y;
//                    }
//                    else
//                    {
//                        if (y <= -0.5)
//                            y = (int)currentPosLocal.y - 1;
//                        else
//                            y = (int)currentPosLocal.y;
//                    }
//                    newPos = new Vector3(x, y, transform.position.z);
//                }
//                else if (itemBlock.IsBetween())
//                {
//                    // Between
//                    if (Mathf.Abs(x) >= (0.5 - boxWidth) && Mathf.Abs(x) <= (0.5 + boxWidth))
//                    {
//                        if (x >= 0)
//                            x = (int)currentPosLocal.x + 0.5f;
//                        else
//                            x = (int)currentPosLocal.x - 0.5f;

//                        if (y >= 0)
//                        {
//                            if (y <= 0.5f)
//                                y = (int)currentPosLocal.y;
//                            else
//                                y = (int)currentPosLocal.y + 1;
//                        }
//                        else
//                        {
//                            y = Mathf.Abs(y);
//                            if (y <= 0.5f)
//                                y = (int)currentPosLocal.y;
//                            else
//                                y = (int)currentPosLocal.y - 1;
//                        }

//                        dummyBlockTransform.Rotate(0, 0, 90);
//                        newPos = new Vector3(x, y, transform.position.z);
//                    }
//                    else if (Mathf.Abs(y) >= 0.3 && Mathf.Abs(y) <= 0.7)
//                    {
//                        if (x >= 0)
//                        {
//                            if (x <= 0.5f)
//                                x = (int)currentPosLocal.x;
//                            else
//                                x = (int)currentPosLocal.x + 1;
//                        }
//                        else
//                        {
//                            x = Mathf.Abs(x);
//                            if (x <= 0.5f)
//                                x = (int)currentPosLocal.x;
//                            else
//                                x = (int)currentPosLocal.x - 1;
//                        }

//                        if (y >= 0)
//                            y = (int)currentPosLocal.y + 0.5f;
//                        else
//                            y = (int)currentPosLocal.y - 0.5f;

//                        newPos = new Vector3(x, y, transform.position.z);
//                    }
//                    else
//                    {
//                        dummyBlockTransform.parent = transform;
//                        dummyBlockTransform.rotation = transform.rotation;
//                    }
//                }
//                dummyBlockTransform.localPosition = newPos;
                
//            }
//            else
//            {
//                dummyBlockTransform.parent = transform;
//                dummyBlockTransform.rotation = transform.rotation;
//                dummyBlockTransform.localPosition = Vector3.zero;
//            }
//        }

//        public void SetItem(ItemBase itemBase) {
//            itemBlock = (ItemBlock)itemBase;
//        }

//        public void RaycastAction1() {
//            if (itemBlock.stack > 0) {
//                /* momentan wird nur die Position kontrolliert, später müssen auch noch benachbarte blöcke kontrolliert werden, 
//                   damit z.B. dünne Wände nicht neben oder zwischen dicken Wänden gesetzt werden können*/

//                bool canBuild = shipTransform != null || itemBlock.createsNewShip;

//                RaycastHit2D[] hits = Physics2D.BoxCastAll(dummyBlockTransform.position, new Vector2((spriteSize.x / 2) - 0.01f, (spriteSize.y / 2 - 0.01f)), 
//                    dummyBlockTransform.eulerAngles.z, Vector2.zero);

//                if (hits.Length == 0 && (itemBlock.position == BlockPosition.BETWEEN_MIDDLE || itemBlock.position == BlockPosition.BETWEEN_TOP
//                    || itemBlock.position == BlockPosition.CENTER_MIDDLE || itemBlock.position == BlockPosition.CENTER_TOP))
//                {
//                    canBuild = false;
//                }
//                else
//                {
//                    foreach (RaycastHit2D hit in hits)
//                    {
//                        GameObject g = hit.collider.gameObject;

//                        if (g.layer == LayerMask.GetMask("Player"))
//                            continue;

//                        Block block = g.GetComponent<Block>();
//                        Debug.Log("itemBlock:" + itemBlock);
//                        Debug.Log("block.ItemBlock:" + block.ItemBlock);

//                        if (block == null || block.transform.parent != dummyBlockTransform.parent 
//                            || itemBlock.position == block.ItemBlock.position)
//                        {
//                            canBuild = false;
//                        }
//                        else
//                        {
//                            switch (itemBlock.position)
//                            {
//                                case BlockPosition.BETWEEN:
//                                    switch (block.ItemBlock.position)
//                                    {
//                                        case BlockPosition.BETWEEN_FLOOR:
//                                        case BlockPosition.BETWEEN_MIDDLE:
//                                        case BlockPosition.BETWEEN_TOP:
//                                            canBuild = false;
//                                            break;
//                                    }
//                                    break;
//                                case BlockPosition.BETWEEN_TOP:
//                                case BlockPosition.BETWEEN_MIDDLE:
//                                case BlockPosition.BETWEEN_FLOOR:
//                                    switch (block.ItemBlock.position)
//                                    {
//                                        case BlockPosition.BETWEEN:
//                                            canBuild = false;
//                                            break;
//                                    }
//                                    break;
//                                case BlockPosition.CENTER:
//                                    switch (block.ItemBlock.position)
//                                    {
//                                        case BlockPosition.CENTER_BOTTOM:
//                                        case BlockPosition.CENTER_MIDDLE:
//                                        case BlockPosition.CENTER_TOP:
//                                            canBuild = false;
//                                            break;
//                                    }
//                                    break;
//                                case BlockPosition.CENTER_TOP:
//                                case BlockPosition.CENTER_MIDDLE:
//                                case BlockPosition.CENTER_BOTTOM:
//                                    switch (block.ItemBlock.position)
//                                    {
//                                        case BlockPosition.CENTER:
//                                            canBuild = false;
//                                            break;
//                                    }
//                                    break;
//                            }

//                        }

//                        if (!canBuild)
//                            break;
//                    }
//                }

//                if (canBuild)
//                {
//                    GameObject blockGObject = Instantiate(itemBlock.prefab);
//                    Transform blockTransform = blockGObject.transform;
//                    blockTransform.position = dummyBlockTransform.position;
//                    blockTransform.transform.rotation = dummyBlockTransform.rotation;

//                    if (shipTransform == null)
//                    {
//                        GameObject newShip = Instantiate(shipPrefab, blockTransform.position, blockTransform.rotation) as GameObject;
//                        blockTransform.SetParent(newShip.transform);
//                    }
//                    else
//                    {
//                        blockTransform.parent = dummyBlockTransform.parent;
//                    }

//                    blockGObject.name = itemBlock.itemName;

//                    blockGObject.GetComponent<Block>().ItemBlock = (ItemBlock) itemBlock.Clone();

//                    itemBlock.stack--;

//                    if (itemBlock.stack <= 0) {
//                        Destroy(transform.gameObject);
//                        Destroy(dummyBlockTransform.gameObject);
//                    }
//                }

//            }
//        }

//        public void RaycastAction2() {
//        }

//        public void RaycastAction3() {
            
//        }
//    }
//}
