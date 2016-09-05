using Assets.Script.ItemSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets {
    public class BlockDummy : MonoBehaviour{

        public bool isAttached;
        public bool isNotBlocking;
        public bool isAttachable;

        [SerializeField]
        private float rayLength;

        private ItemBlock itemBlock;

        [SerializeField]
        private Color colorBuildable;
        [SerializeField]
        private Color colorNotBuildable;

        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private LayerMask layerMaskShipBottom;
        [SerializeField]
        private LayerMask layerMaskShipMiddle;
        [SerializeField]
        private LayerMask layerMaskShipTop;
        [SerializeField]
        private LayerMask layerMaskShip;
        [SerializeField]
        private LayerMask layerMaskBetween;
        private LayerMask layerMask;
        private LayerMask layerMaskAttach;

        Vector2 spriteSize = new Vector2();

        public void Buildable(bool flag) {
            if (flag)
                spriteRenderer.color = colorBuildable;
            else
                spriteRenderer.color = colorNotBuildable;
        }

        public void SetItem(ItemBlock itemBlock, LayerMask layerMaskAttach) {

            this.layerMaskAttach = layerMaskAttach;

            this.itemBlock = itemBlock;

            SpriteRenderer spriteRendererItemBlock = itemBlock.prefab.GetComponent<SpriteRenderer>();

            //spriteSize = new Vector2(itemBlock.prefab.GetComponent<BoxCollider2D>().bounds.size.x-0.1f, itemBlock.prefab.GetComponent<BoxCollider2D>().bounds.size.y - 0.1f);

            float shrinkX = 0.3f;
            float shrinkY =  0.3f;

            if (spriteRendererItemBlock.bounds.size.x < shrinkX)
                shrinkX = 0f;
            if (spriteRendererItemBlock.bounds.size.y < shrinkY)
                shrinkY = 0.01f;
            


            spriteSize = new Vector2(spriteRendererItemBlock.bounds.size.x - shrinkX, spriteRendererItemBlock.bounds.size.y - shrinkY);



            spriteRenderer = GetComponent<SpriteRenderer>();


            //spriteRenderer.sortingLayerID = spriteRendererItemBlock.sortingLayerID;



            //transform.localScale = spriteSize;


            spriteRenderer.sprite = itemBlock.prefab.GetComponent<SpriteRenderer>().sprite;

            isNotBlocking = false;


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
            }

        }

        void Start() {
            //InvokeRepeating("DebugTest", 1, 1);
            //InvokeRepeating("test", 1, 1);
            //InvokeRepeating("test2", 1, 1);

        }

        void Update() {
            //DebugTest();
            test2();
            test();
        }

        private void DebugTest() {
            //right
            //up
            if(itemBlock.rightUp)
                Debug.DrawRay(transform.position + transform.TransformVector(new Vector3(0.25f, 0.25f)), transform.rotation * Vector2.right * rayLength, Color.magenta, 0.2f);
            //down
            if(itemBlock.rightDown)
                Debug.DrawRay(transform.position + transform.TransformVector(new Vector3(0.25f, -0.25f)), transform.rotation * Vector2.right * rayLength, Color.magenta, 0.2f);

            //left
            //up
            if(itemBlock.leftUp)
                Debug.DrawRay(transform.position + transform.TransformVector(new Vector3(-0.25f, 0.25f)), transform.rotation * Vector2.left * rayLength, Color.magenta, 0.2f);
            //down
            if(itemBlock.leftDown)
                Debug.DrawRay(transform.position + transform.TransformVector(new Vector3(-0.25f, -0.25f)), transform.rotation * Vector2.left * rayLength, Color.magenta, 0.3f);

            //up
            //right
            if(itemBlock.upRight)
                Debug.DrawRay(transform.position + transform.TransformVector(new Vector3(0.25f, 0.25f)), transform.rotation * Vector2.up * rayLength, Color.magenta, 0.2f);
            //left
            if(itemBlock.upLeft)
                Debug.DrawRay(transform.position + transform.TransformVector(new Vector3(-0.25f, 0.25f)), transform.rotation * Vector2.up * rayLength, Color.magenta, 0.2f);

            //down
            //right
            if(itemBlock.downRight)
                Debug.DrawRay(transform.position + transform.TransformVector(new Vector3(0.25f, -0.25f)), transform.rotation * Vector2.down * rayLength, Color.magenta, 0.2f);
            //left
            if (itemBlock.downLeft)
                Debug.DrawRay(transform.position + transform.TransformVector(new Vector3(-0.25f, -0.25f)), transform.rotation * Vector2.down * rayLength, Color.magenta, 0.2f);

            if (itemBlock.forward) {
                Debug.DrawRay(transform.position + transform.TransformVector(new Vector3(0f, 0f)), transform.rotation * Vector3.zero, Color.magenta, layerMaskAttach);
                RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.TransformVector(new Vector3(0f, 0f)), transform.rotation * Vector3.forward, rayLength, layerMaskAttach);
                if (hit.collider != null) {
                    //Debug.Log(hit.collider.transform.name);

                    
                    //Debug.Log("Forward");
                }

            }

        }

        private void test() {
            
           

            if (itemBlock.position == BlockPosition.CENTER || itemBlock.position == BlockPosition.CENTER_BOTTOM) {
                List<RaycastHit2D> hits = new List<RaycastHit2D>();
                isAttachable = false;
                //right
                //up
                if (itemBlock.rightUp)
                    hits.Add(Physics2D.Raycast(transform.position + transform.TransformVector(new Vector3(0.25f, 0.25f)), transform.rotation * Vector2.right, rayLength, layerMaskAttach));
                //down
                if (itemBlock.rightDown)
                    hits.Add(Physics2D.Raycast(transform.position + transform.TransformVector(new Vector3(0.25f, -0.25f)), transform.rotation * Vector2.right, rayLength, layerMaskAttach));

                //left
                //up
                if (itemBlock.leftUp)
                    hits.Add(Physics2D.Raycast(transform.position + transform.TransformVector(new Vector3(-0.25f, 0.25f)), transform.rotation * Vector2.left, rayLength, layerMaskAttach));
                //down
                if (itemBlock.leftDown)
                    hits.Add(Physics2D.Raycast(transform.position + transform.TransformVector(new Vector3(-0.25f, -0.25f)), transform.rotation * Vector2.left, rayLength, layerMaskAttach));

                //right
                if (itemBlock.upRight)
                    hits.Add(Physics2D.Raycast(transform.position + transform.TransformVector(new Vector3(0.25f, 0.25f)), transform.rotation * Vector2.up, rayLength, layerMaskAttach));
                //links
                if (itemBlock.upLeft)
                    hits.Add(Physics2D.Raycast(transform.position + transform.TransformVector(new Vector3(-0.25f, 0.25f)), transform.rotation * Vector2.up, rayLength, layerMaskAttach));

                //down
                //right
                if (itemBlock.downRight)
                    hits.Add(Physics2D.Raycast(transform.position + transform.TransformVector(new Vector3(0.25f, -0.25f)), transform.rotation * Vector2.down, rayLength, layerMaskAttach));
                //links
                if (itemBlock.downLeft)
                    hits.Add(Physics2D.Raycast(transform.position + transform.TransformVector(new Vector3(-0.25f, -0.25f)), transform.rotation * Vector2.down, rayLength, layerMaskAttach));

                if (itemBlock.forward) {
                    hits.Add(Physics2D.Raycast(transform.position + transform.TransformVector(new Vector3(0f, 0f)), transform.rotation * Vector3.forward, rayLength, layerMaskAttach));
                    //Debug.Log("FORWARD");
                }
                    



                foreach (RaycastHit2D hit in hits) {
                    if (hit.collider != null) {
                        isAttachable = true;
                        //Debug.Log(hit.collider.transform.name);
                        break;
                    }
                }
            }
            else {
                isAttachable = true;
            }

           


        }

        private void test2() {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();

            isNotBlocking = true;

            string test = "";

            if (itemBlock.position == BlockPosition.CENTER || itemBlock.position == BlockPosition.CENTER_BOTTOM || itemBlock.position == BlockPosition.CENTER_MIDDLE || itemBlock.position == BlockPosition.CENTER_TOP || itemBlock.position == BlockPosition.CENTER_ONTOP) {
                hits.AddRange(Physics2D.BoxCastAll(transform.position, spriteSize, transform.eulerAngles.z, Vector3.zero, 200, layerMask));

                //Debug.Log("Position: " + transform.position + " " + spriteSize.x + ", " + spriteSize.y);

                

                foreach (RaycastHit2D hit in hits) {
                    if (hit.collider != null) {
                        isNotBlocking = false;

                        test = test + " " + hit.collider.transform.name;

                        //Debug.Log(hit.collider.transform.name);
                        break;
                    }
                }

               
            }

            if (itemBlock.position == BlockPosition.BETWEEN) {

                //hits.AddRange(Physics2D.BoxCastAll(transform.position + -transform.up * spriteSize.y / 2, spriteSize, transform.eulerAngles.z, Vector3.zero, 200, layerMask));

                ////Debug.Log("Position: " + transform.position + " " + spriteSize.x + ", " + spriteSize.y);



                //foreach (RaycastHit2D hita in hits) {
                //    if (hita.collider != null) {
                //        isNotBlocking = false;

                //        test = test + " " + hita.collider.transform.name;

                //        //Debug.Log(hit.collider.transform.name);
                //        break;
                //    }
                //}

                Debug.DrawRay(transform.position + -transform.up * spriteSize.y / 2, transform.rotation * Vector3.forward, Color.magenta, 1);
                RaycastHit2D hit = Physics2D.Raycast(transform.position + -transform.up * spriteSize.y / 2, transform.rotation * Vector3.forward, rayLength, layerMask);
                if (hit.collider != null) {
                    //Debug.Log(hit.collider.transform.name);
                    //Debug.Log("Ray:" + hit.collider.transform.name);
                    test = test + " " + hit.collider.transform.name;
                    //Debug.Log("Forward");
                    isNotBlocking = false;
                }

            }

            if (test != "") {
                //Debug.Log(test);
            }
        }

    }
}
