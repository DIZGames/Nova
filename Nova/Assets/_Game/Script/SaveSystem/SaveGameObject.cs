using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasse zur speicherung von GameObjects und ihrem Transform.
/// </summary>
[Serializable]
public class SaveGameObject
{
    public string Name { set; get; }
    public string Tag { set; get; }
    public string prefabId { set; get; }
    public int Layer { set; get; }
    public bool IsStatic { set; get; }
    public bool IsActive { set; get; }
    public SaveVector3 LocalPosition { set; get; }
    public SaveVector3 LocalRotation { set; get; }
    public SaveVector3 LocalScale { set; get; }

    private List<SaveGameObject> _children = null;
    private List<SaveComponent> _components = null;
    private bool hasSaveMe;

    /// <summary>
    /// Nicht nutzen. Dient nur zur Serialisierung!
    /// </summary>
    private SaveGameObject() {}


    public SaveGameObject(GameObject gameObject, bool isChild = false)
    {
        SaveMe saveMe = gameObject.GetComponent<SaveMe>();
        hasSaveMe = saveMe != null ? true : false;

        if (saveMe == null && !isChild)
            throw new Exception("Only objects with a SaveMe Component can be saved!");

        Name = gameObject.name;
        Tag = gameObject.tag;
        Layer = gameObject.layer;
        IsActive = gameObject.activeSelf;
        IsStatic = gameObject.isStatic;
        prefabId = saveMe != null ? saveMe.prefabId : "";

        Transform t = gameObject.transform;
        LocalPosition = new SaveVector3(t.localPosition.x, t.localPosition.y, t.localPosition.z);
        LocalRotation = new SaveVector3(t.localRotation.eulerAngles.x, t.localRotation.eulerAngles.y, t.localRotation.eulerAngles.z);
        LocalScale = new SaveVector3(t.localScale.x, t.localScale.y, t.localScale.z);

        if (saveMe != null && saveMe.saveChildren && t.childCount > 0)
        {
            _children = new List<SaveGameObject>();
            for (int i = 0; i < t.childCount; i++)
                _children.Add(new SaveGameObject(t.GetChild(i).gameObject, true));
        }

        Component[] cs = gameObject.GetComponents<Component>();
        foreach (Component c in cs)
        {
            if (c is Transform == false)
            {
                Components.Add(new SaveComponent(c));
            }
        }
    }

    public GameObject ToGameObject(GameObject go = null, Transform parent = null)
    {
        if (!string.IsNullOrEmpty(prefabId))
            go = GameObject.Instantiate(SaveManager.getPrefab(prefabId));
        else if(go == null)
            go = new GameObject();
        go.name = Name;
        go.tag = Tag;
        go.layer = Layer;
        go.SetActive(IsActive);
        go.isStatic = IsStatic;
        Transform transform = go.transform;
        transform.SetParent(parent);
        if (LocalPosition != null)
            transform.localPosition = new Vector3(LocalPosition.A, LocalPosition.B, LocalPosition.C);
        if (LocalRotation != null)
            transform.localRotation = Quaternion.Euler(LocalRotation.A, LocalRotation.B, LocalRotation.C);
        if (LocalScale != null)
            transform.localScale = new Vector3(LocalScale.A, LocalScale.B, LocalScale.C);
        if (_children != null) {
            foreach (SaveGameObject child in _children)
            {
                if(child.hasSaveMe)
                    child.ToGameObject(null, transform);
                else
                {
                    GameObject childGo = go.transform.FindChild(child.Name).gameObject;
                    child.ToGameObject(childGo, transform);
                }
            }
        }
        if(_components != null)
        {
            foreach(SaveComponent sc in _components)
            {
                sc.AddComponent(go);
            }
        }

        return go;
    }

    public List<SaveGameObject> Children{
        get {
            if (_children == null)
                _children = new List<SaveGameObject>();
            return _children;
        }
    }

    public List<SaveComponent> Components
    {
        get
        {
            if (_components == null)
                _components = new List<SaveComponent>();
            return _components;
        }
    }
}
