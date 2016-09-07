using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Klasse zur speicherung von GameObjects und ihrem Transform.
/// </summary>
[Serializable]
public class SaveGameObject
{
    public string Name { set; get; }
    public string Tag { set; get; }
    public string PrefabPath { set; get; }
    public int Layer { set; get; }
    public bool IsStatic { set; get; }
    public bool IsActive { set; get; }
    public SaveVector3 LocalPosition { set; get; }
    public SaveVector3 LocalRotation { set; get; }
    public SaveVector3 LocalScale { set; get; }

    List<SaveGameObject> _children = null;
    List<SaveComponent> _components = null;

    /// <summary>
    /// Nicht nutzen. Dient nur zur Serialisierung!
    /// </summary>
    private SaveGameObject() {}


    public SaveGameObject(GameObject gameObject, SaveMe saveMe = null)
    {
        if(saveMe == null)
            saveMe = gameObject.GetComponent<SaveMe>();

        if (saveMe == null)
            throw new Exception("Only objects with a SaveMe Component can be saved!");

        Name = gameObject.name;
        Tag = gameObject.tag;
        Layer = gameObject.layer;
        IsActive = gameObject.activeSelf;
        IsStatic = gameObject.isStatic;

        GameObject prefab = (GameObject)PrefabUtility.GetPrefabParent(gameObject);
        if (prefab != null)
            PrefabPath = AssetDatabase.GetAssetPath(prefab).Remove(0, "Assets\\Resources\\".Length).Replace(".prefab", "");

        Transform t = gameObject.transform;
        LocalPosition = new SaveVector3(t.localPosition.x, t.localPosition.y, t.localPosition.z);
        LocalRotation = new SaveVector3(t.localRotation.eulerAngles.x, t.localRotation.eulerAngles.y, t.localRotation.eulerAngles.z);
        LocalScale = new SaveVector3(t.localScale.x, t.localScale.y, t.localScale.z);

        if (saveMe.saveChildren && t.childCount > 0)
        {
            _children = new List<SaveGameObject>();
            for (int i = 0; i < t.childCount; i++)
                _children.Add(new SaveGameObject(t.GetChild(i).gameObject, saveMe));
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

    public GameObject ToGameObject()
    {
        GameObject go;
        if (PrefabPath != null && PrefabPath != "")
            go = GameObject.Instantiate((GameObject)Resources.Load(PrefabPath));
        else 
            go = new GameObject();
        go.name = Name;
        go.tag = Tag;
        go.layer = Layer;
        go.SetActive(IsActive);
        go.isStatic = IsStatic;
        Transform transform = go.transform;
        //TODO Posion wird manchmal nicht richtig gesetzt
        if (LocalPosition != null)
            transform.localPosition = new Vector3(LocalPosition.A, LocalPosition.B, LocalPosition.C);
        if (LocalRotation != null)
            transform.localRotation = Quaternion.Euler(LocalRotation.A, LocalRotation.B, LocalRotation.C);
        if (LocalScale != null)
            transform.localScale = new Vector3(LocalScale.A, LocalScale.B, LocalScale.C);
        if (_children != null) {
            foreach (SaveGameObject child in _children)
            {
                if((PrefabPath != null || PrefabPath != "") && transform.FindChild(child.Name))
                {

                }
                else
                    child.ToGameObject().transform.SetParent(transform);
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
