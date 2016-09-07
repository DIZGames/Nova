using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Klasse für alle zu speichernden Components außer dem Transform, da dieses immer mit dem GameObject erzeugt wird.
/// </summary>
[Serializable]
public class SaveComponent {
    ComponentType componenType;
    Type classType;
    Dictionary<string, bool> _boolFields = null;
    Dictionary<string, int> _intFields = null;
    Dictionary<string, float> _floatFields = null;
    Dictionary<string, string> _stringFields = null;
    Dictionary<string, SaveGameObject> _gameObjectFields = null;
    Dictionary<string, SaveComponent> _componentFields = null;

    enum ComponentType { MONOBEHAVIOUR }

    /// <summary>
    /// Nicht nutzen. Dient nur zur Serialisierung!
    /// </summary>
    private SaveComponent() { }

    public SaveComponent(Component c)
    {
        if(c is MonoBehaviour)
        {
            componenType = ComponentType.MONOBEHAVIOUR;
            classType = c.GetType();
            Debug.Log(classType.Name);
            MonoBehaviour script = (MonoBehaviour)c;
            var fields = script.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(prop => Attribute.IsDefined(prop, typeof(SaveThis)));
            foreach (var field in fields)
            {
                object value = field.GetValue(script);
                if (field.FieldType.Name == "String")
                    StringFields.Add(field.Name, value as string);
                else if (field.FieldType.Name == "Int32")
                    IntFields.Add(field.Name, (int)value);
                else if (field.FieldType.Name == "Boolean")
                    BoolFields.Add(field.Name, (bool)value);
                else if (field.FieldType.Name == "Float")
                    FloatFields.Add(field.Name, (float)value);
                else if (field.FieldType.Name == "GameObject")
                    GameObjectFields.Add(field.Name, new SaveGameObject((GameObject)value));
                else if (field.FieldType.Name == "Component")
                    ComponentFields.Add(field.Name, new SaveComponent((Component)value));
            }
        }
    }

    /// <summary>
    /// Erzeugt und fügt die Komponente zum dem GameObject hinzu.
    /// </summary>
    /// <param name="go"></param>
    public void AddComponent(GameObject go)
    {
        switch (componenType)
        {
            case ComponentType.MONOBEHAVIOUR:
                MethodInfo method = typeof(GameObject).GetMethod("AddComponent", new Type[] { });
                MethodInfo generic = method.MakeGenericMethod(classType);
                MonoBehaviour script = (MonoBehaviour)generic.Invoke(go, null);

                var fields = script.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(prop => Attribute.IsDefined(prop, typeof(SaveThis)));
                foreach (var field in fields)
                {
                    if (field.FieldType.Name == "String")
                        field.SetValue(script, StringFields[field.Name]);
                    else if (field.FieldType.Name == "Int32")
                        field.SetValue(script, IntFields[field.Name]);
                    else if (field.FieldType.Name == "Boolean")
                        field.SetValue(script, IntFields[field.Name]);
                    else if (field.FieldType.Name == "Float")
                        field.SetValue(script, FloatFields[field.Name]);
                    // GameObject sind ein problem, da es nicht erstellt werden kann sondern in der Welt vorhanden sein muss. Find Methode in der Szene versuchen?
                    //else if (field.FieldType.Name == "GameObject")
                    //    field.SetValue(script, GameObjectFields[field.Name].ToGameObject());
                    // Components sind ein problem, da es nicht erstellt werden kann sondern in der Welt vorhanden sein muss
                    //else if (field.FieldType.Name == "Component")
                    //    field.SetValue(script, ComponentFields[field.Name].); e));
                }

                break;
        }
    }

    public Dictionary<string, bool> BoolFields
    {
        get
        {
            if (_boolFields == null)
                _boolFields = new Dictionary<string, bool>();
            return _boolFields;
        }
    }

    public Dictionary<string, int> IntFields
    {
        get
        {
            if (_intFields == null)
                _intFields = new Dictionary<string, int>();
            return _intFields;
        }
    }

    public Dictionary<string, float> FloatFields
    {
        get
        {
            if (_floatFields == null)
                _floatFields = new Dictionary<string, float>();
            return _floatFields;
        }
    }

    public Dictionary<string, string> StringFields
    {
        get
        {
            if (_stringFields == null)
                _stringFields = new Dictionary<string, string>();
            return _stringFields;
        }
    }

    public Dictionary<string, SaveGameObject> GameObjectFields
    {
        get
        {
            if (_gameObjectFields == null)
                _gameObjectFields = new Dictionary<string, SaveGameObject>();
            return _gameObjectFields;
        }
    }

    public Dictionary<string, SaveComponent> ComponentFields
    {
        get
        {
            if (_componentFields == null)
                _componentFields = new Dictionary<string, SaveComponent>();
            return _componentFields;
        }
    }
}
