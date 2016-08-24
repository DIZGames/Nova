using Assets.Script.ItemSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {

    [RequireComponent(typeof(Collider2D))]
    public class EquippedBlockLogic : MonoBehaviour, IEquippable {

        Transform player;
        Transform shipTransform;
        new Transform transform;
        new Collider collider;
        GameObject shipPrefab;
        ItemBlockValues itemBlockValues;
        float boxWidth = 0.2f; // Defines the width of the box in which the mouse position is checked for parts that can be set between Blocks (like Walls)
        float rayCastLength = 2f;
        int layerMaskBlock;
        public Transform dummyBlock;
        Vector2 spriteSize;
        List<Collider2D> collidersInArea = new List<Collider2D>();
        List<Collider2D> blocksInArea = new List<Collider2D>();

        void Start() {
            transform = GetComponent<Transform>();
            layerMaskBlock = LayerMask.GetMask("Block") | LayerMask.GetMask("BlockFloor");
            // Muss irgendwann gegen eine bessere Möglichkeit ausgetauscht werden, wie man an den Player kommt
            player = GameObject.Find("Player").transform;
            shipPrefab = (GameObject)Resources.Load("Prefab/Ship/Ship");
            collider = dummyBlock.GetComponent<Collider>();
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
            /* Verbesserungsmöglichkeiten:
             * block position anpassen and block typ, sodass der dummy block nur an den stellen erscheint wo er auch hingesetzt werden kann, 
             * also ThingOnShip nur auf boden blöcke, wand center blöcke nur an einen anderen block dran
             * siehe blöcke setzen bei space engineers
            */ 
            if(shipTransform != null)
            {
                dummyBlock.rotation = shipTransform.rotation;
                dummyBlock.parent = shipTransform;
                Vector3 currentPosLocal = shipTransform.InverseTransformPoint(transform.position);
                float x = currentPosLocal.x - (int)currentPosLocal.x;
                float y = currentPosLocal.y - (int)currentPosLocal.y;
                if (itemBlockValues.IsCenter())
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
                else if (itemBlockValues.IsBetween())
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
                dummyBlock.parent = transform;
                dummyBlock.rotation = transform.rotation;
                dummyBlock.localPosition = Vector3.zero;
            }
        }

        public void setItemValues(ItemValues itemValues) {
            itemBlockValues = (ItemBlockValues)itemValues;
        }

        public void Action1() {
            if (itemBlockValues.stack > 0) {
                /* momentan wird nur die Position kontrolliert, später müssen auch noch benachbarte blöcke kontrolliert werden, 
                   damit z.B. dünne Wände nicht neben oder zwischen dicken Wänden gesetzt werden können*/
                // größe des sprites zur berechnung der OverlapArea nehmen
                bool canBuild = shipTransform != null || itemBlockValues.CreatesNewShip;
                foreach (Collider2D c in collidersInArea)
                {
                    GameObject g = c.gameObject;

                    if (g.layer == LayerMask.GetMask("Player"))
                        continue;

                    Block block = g.GetComponent<Block>();
                    if (block == null)
                    {
                        canBuild = false;
                    }
                    else
                    {
                        if (itemBlockValues.BlockPosition == block.ItemBlockValues.BlockPosition)
                        {
                            canBuild = false;
                        }
                        else 
                        {
                            switch (itemBlockValues.BlockPosition)
                            {
                                case BlockPosition.BETWEEN:
                                    switch (block.ItemBlockValues.BlockPosition)
                                    {
                                        case BlockPosition.BETWEEN_FLOOR:
                                        case BlockPosition.BETWEEN_MIDDLE:
                                        case BlockPosition.BETWEEN_TOP:
                                            canBuild = false;
                                            break;
                                    }
                                    break;
                                case BlockPosition.BETWEEN_TOP:
                                case BlockPosition.BETWEEN_MIDDLE:
                                case BlockPosition.BETWEEN_FLOOR:
                                    switch (block.ItemBlockValues.BlockPosition)
                                    {
                                        case BlockPosition.BETWEEN:
                                            canBuild = false;
                                            break;
                                    }
                                    break;
                                case BlockPosition.CENTER:
                                    switch (block.ItemBlockValues.BlockPosition)
                                    {
                                        case BlockPosition.CENTER_BOTTOM:
                                        case BlockPosition.CENTER_MIDDLE:
                                        case BlockPosition.CENTER_TOP:
                                            canBuild = false;
                                            break;
                                    }
                                    break;
                                case BlockPosition.CENTER_TOP:
                                case BlockPosition.CENTER_MIDDLE:
                                case BlockPosition.CENTER_BOTTOM:
                                    switch (block.ItemBlockValues.BlockPosition)
                                    {
                                        case BlockPosition.CENTER:
                                            canBuild = false;
                                            break;
                                    }
                                    break;
                            }
                        }

                    }

                    if (!canBuild)
                        break;
                }

                if (canBuild)
                {
                    GameObject blockGObject = Instantiate(itemBlockValues.Prefab);
                    Transform blockTransform = blockGObject.transform;
                    blockTransform.position = dummyBlock.position;
                    blockTransform.transform.rotation = dummyBlock.rotation;

                    if (shipTransform == null)
                    {
                        GameObject newShip = Instantiate(shipPrefab, blockTransform.position, blockTransform.rotation) as GameObject;
                        blockTransform.SetParent(newShip.transform);
                    }
                    else
                    {
                        blockTransform.parent = dummyBlock.parent;
                    }


                    blockGObject.name = itemBlockValues.Name;

                    ItemBlockValues newItemBlockValues = ItemBlockValues.CreateNew((ItemBlock)itemBlockValues.itemBase);


                    blockGObject.GetComponent<Block>().ItemBlockValues = newItemBlockValues;

                    itemBlockValues.stack--;

                    if (itemBlockValues.stack <= 0) {
                        Destroy(transform.gameObject);
                        Destroy(dummyBlock.gameObject);
                    }
                }

            }
        }

        public void Action2() {
           
        }

        public void Action3() {
            
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            // Bessere Möglichkeit ein Schiff effizient zu identifizieren?
            collidersInArea.Add(collider);
            if (shipTransform == null && collider.transform.parent.name.Contains("Ship"))
            {
                shipTransform = collider.transform.parent;
                blocksInArea.Add(collider);
            }
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            collidersInArea.Remove(collider);
            blocksInArea.Remove(collider);
            if (blocksInArea.Count == 0)
                shipTransform = null; ;
        }

    }
}
