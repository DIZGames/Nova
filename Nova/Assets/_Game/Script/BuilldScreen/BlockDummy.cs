using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;
using System.Collections.Generic;
using System.Reflection;

public class BlockDummy : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    //public void setItem(ItemBlock itemBlock) {
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //    spriteRenderer.sprite = itemBlock.prefab.GetComponent<SpriteRenderer>().sprite;
    //}

    [SerializeField]
    private LayerMask layerMaskAttachableShipBottom;
    [SerializeField]
    private LayerMask layerMaskAttachableShipMiddle;
    [SerializeField]
    private LayerMask layerMaskAttachableShipTop;
    [SerializeField]
    private LayerMask layerMaskAttachableShipOnTop;
    [SerializeField]
    private LayerMask layerMaskAttachableShip;
    [SerializeField]
    private LayerMask layerMaskAttachableBetween;

    [SerializeField]
    private Color colorBuildable;
    [SerializeField]
    private Color colorNotBuildable;
    [SerializeField]
    private bool isDebug;

    public bool isAttached;
    public bool isNotBlocking;
    public bool isAttachable;
    private ItemBlock itemBlock;
    private LayerMask layerMaskAttachable;


    public void Buildable(bool flag)
    {
        if (flag)
            spriteRenderer.color = colorBuildable;
        else
            spriteRenderer.color = colorNotBuildable;
    }

    //Reflection; works, but doesnt suit the problem -> OBSOLETE
    private T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        System.Reflection.PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        foreach (System.Reflection.PropertyInfo property in properties)
        {
            property.SetValue(copy, property.GetValue(original, null), null);
        }
        return copy as T;
    }

    private void CreateColliderOnDummy()
    {
        float colliderScale = 0.95f;

        Collider collider = itemBlock.prefab.GetComponent<Collider>();

        if (itemBlock.position == BlockPosition.CENTER | itemBlock.position == BlockPosition.CENTER_BOTTOM | itemBlock.position == BlockPosition.CENTER_MIDDLE | itemBlock.position == BlockPosition.CENTER_TOP | itemBlock.position == BlockPosition.CENTER_ONTOP)
        {
            if (collider.GetType() == typeof(BoxCollider))
            {
                BoxCollider prefabCollider = itemBlock.prefab.GetComponent<BoxCollider>();

                BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
                boxCollider.isTrigger = true;

                boxCollider.size = prefabCollider.size * colliderScale;
                boxCollider.center = prefabCollider.center;
            }
            if (collider.GetType() == typeof(SphereCollider))
            {
                SphereCollider prefabCollider = itemBlock.prefab.GetComponent<SphereCollider>();

                SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
                sphereCollider.isTrigger = true;

                sphereCollider.radius = prefabCollider.radius * colliderScale;
                sphereCollider.center = prefabCollider.center;
            }
        }
        if (itemBlock.position == BlockPosition.BETWEEN)
        {
            if (collider.GetType() == typeof(BoxCollider))
            {
                BoxCollider prefabCollider = itemBlock.prefab.GetComponent<BoxCollider>();

                BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
                boxCollider.isTrigger = true;

                Vector3 vect = prefabCollider.size * colliderScale;
                vect.x -= 0.2f;
                boxCollider.size = vect;
                boxCollider.center = prefabCollider.center * colliderScale;
            }
        }
    }

    public void SetItem(ItemBlock itemBlock)
    {

        this.itemBlock = itemBlock;

        CreateColliderOnDummy();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemBlock.prefab.GetComponent<SpriteRenderer>().sprite;

        switch (this.itemBlock.position)
        {
            case BlockPosition.CENTER_BOTTOM:
                layerMaskAttachable = layerMaskAttachableShipBottom;
                break;
            case BlockPosition.CENTER_MIDDLE:
                layerMaskAttachable = layerMaskAttachableShipMiddle;
                break;
            case BlockPosition.CENTER_TOP:
                layerMaskAttachable = layerMaskAttachableShipTop;
                break;
            case BlockPosition.CENTER_ONTOP:
                layerMaskAttachable = layerMaskAttachableShipOnTop;
                break;
            case BlockPosition.CENTER:
                layerMaskAttachable = layerMaskAttachableShip;
                break;
            case BlockPosition.BETWEEN:
                layerMaskAttachable = layerMaskAttachableBetween;
                break;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (isDebug)
            DebugCheckAttachable();
        CheckAttachable();
    }

    void FixedUpdate()
    {
        isNotBlocking = true;
    }

    private void DebugCheckAttachable()
    {

        if (itemBlock.attachableRaysUp.Count != 0)
        {
            for (int i = 0; i < itemBlock.attachableRaysUp.Count; i++)
            {
                Debug.DrawRay(transform.position + transform.TransformVector(itemBlock.attachableRaysUp[i].origin), transform.rotation * itemBlock.attachableRaysUp[i].direction * itemBlock.attachableRaysUp[i].distance, Color.magenta, 0.2f);
            }
        }

        if (itemBlock.attachableRaysDown.Count != 0)
        {
            for (int i = 0; i < itemBlock.attachableRaysDown.Count; i++)
            {
                Debug.DrawRay(transform.position + transform.TransformVector(itemBlock.attachableRaysDown[i].origin), transform.rotation * itemBlock.attachableRaysDown[i].direction * itemBlock.attachableRaysDown[i].distance, Color.magenta, 0.2f);
            }
        }

        if (itemBlock.attachableRaysLeft.Count != 0)
        {
            for (int i = 0; i < itemBlock.attachableRaysLeft.Count; i++)
            {
                Debug.DrawRay(transform.position + transform.TransformVector(itemBlock.attachableRaysLeft[i].origin), transform.rotation * itemBlock.attachableRaysLeft[i].direction * itemBlock.attachableRaysLeft[i].distance, Color.magenta, 0.2f);
            }
        }

        if (itemBlock.attachableRaysRight.Count != 0)
        {
            for (int i = 0; i < itemBlock.attachableRaysRight.Count; i++)
            {
                Debug.DrawRay(transform.position + transform.TransformVector(itemBlock.attachableRaysRight[i].origin), transform.rotation * itemBlock.attachableRaysRight[i].direction * itemBlock.attachableRaysRight[i].distance, Color.magenta, 0.2f);
            }
        }

        if (itemBlock.attachableRaysForward.Count != 0)
        {
            for (int i = 0; i < itemBlock.attachableRaysForward.Count; i++)
            {
                Debug.DrawRay(transform.position + transform.TransformVector(itemBlock.attachableRaysForward[i].origin), transform.rotation * itemBlock.attachableRaysForward[i].direction * itemBlock.attachableRaysForward[i].distance, Color.magenta, 0.2f);
            }
        }

    }

    private void CheckAttachable()
    {
        isAttachable = false;
        bool allMandatoryAttached = true;

        if (isAttached)
        {
            List<RaycastHit> hits = new List<RaycastHit>();

            if (itemBlock.attachableRaysUp.Count != 0 && allMandatoryAttached)
            {
                for (int i = 0; i < itemBlock.attachableRaysUp.Count; i++)
                {
                    RaycastHit hit;
                    Physics.Raycast(transform.position + transform.TransformVector(itemBlock.attachableRaysUp[i].origin), transform.rotation * itemBlock.attachableRaysUp[i].direction, out hit, itemBlock.attachableRaysUp[i].distance, layerMaskAttachable);
                    hits.Add(hit);
                    if (hit.collider == null && itemBlock.attachableRaysUp[i].isMandatory)
                        allMandatoryAttached = false;
                }
            }

            if (itemBlock.attachableRaysDown.Count != 0 && allMandatoryAttached)
            {
                for (int i = 0; i < itemBlock.attachableRaysDown.Count; i++)
                {
                    RaycastHit hit;
                    Physics.Raycast(transform.position + transform.TransformVector(itemBlock.attachableRaysDown[i].origin), transform.rotation * itemBlock.attachableRaysDown[i].direction, out hit, itemBlock.attachableRaysDown[i].distance, layerMaskAttachable);
                    hits.Add(hit);
                    if (hit.collider == null && itemBlock.attachableRaysDown[i].isMandatory)
                        allMandatoryAttached = false;
                }
            }

            if (itemBlock.attachableRaysLeft.Count != 0 && allMandatoryAttached)
            {
                for (int i = 0; i < itemBlock.attachableRaysLeft.Count; i++)
                {
                    RaycastHit hit;
                    Physics.Raycast(transform.position + transform.TransformVector(itemBlock.attachableRaysLeft[i].origin), transform.rotation * itemBlock.attachableRaysLeft[i].direction, out hit, itemBlock.attachableRaysLeft[i].distance, layerMaskAttachable);
                    hits.Add(hit);
                    if (hit.collider == null && itemBlock.attachableRaysLeft[i].isMandatory)
                        allMandatoryAttached = false;
                }
            }

            if (itemBlock.attachableRaysRight.Count != 0 && allMandatoryAttached)
            {
                for (int i = 0; i < itemBlock.attachableRaysRight.Count; i++)
                {
                    RaycastHit hit;
                    Physics.Raycast(transform.position + transform.TransformVector(itemBlock.attachableRaysRight[i].origin), transform.rotation * itemBlock.attachableRaysRight[i].direction, out hit, itemBlock.attachableRaysRight[i].distance, layerMaskAttachable);
                    hits.Add(hit);
                    if (hit.collider == null && itemBlock.attachableRaysRight[i].isMandatory)
                        allMandatoryAttached = false;
                }
            }

            if (itemBlock.attachableRaysForward.Count != 0 && allMandatoryAttached)
            {
                for (int i = 0; i < itemBlock.attachableRaysForward.Count; i++)
                {
                    RaycastHit hit;
                    Physics.Raycast(transform.position + transform.TransformVector(itemBlock.attachableRaysForward[i].origin), transform.rotation * itemBlock.attachableRaysForward[i].direction, out hit, itemBlock.attachableRaysForward[i].distance, layerMaskAttachable);
                    hits.Add(hit);
                    if (hit.collider == null && itemBlock.attachableRaysForward[i].isMandatory)
                        allMandatoryAttached = false;
                }
            }

            if (allMandatoryAttached)
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider != null)
                    {
                        isAttachable = true;
                        break;
                    }
                }
            }
        }
    }

    private void OnTriggerStay()
    {
        isNotBlocking = false;
    }
}
