using FlatBuffers;
using Nova;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class FlatSaveGameManager  {

    private static string saveFolder = "Savegames";
    private static FlatBufferBuilder fbb = new FlatBufferBuilder(1);

    enum Type { DATA, GAMEOBJECT, COMPONENT}

    #region Speichern
    public static string Save(FlatSaveData saveFile, string fileName)
    {
        Offset<FlatGameObject>[] flatGameObjects = new Offset<FlatGameObject>[saveFile.gameObjects.Count];
        for(int i = 0; i < saveFile.gameObjects.Count; i++)
        {
            GameObject gameObject = saveFile.gameObjects[i];
            flatGameObjects[i] = GameObjectToFlatGameObject(gameObject);
        }

        VectorOffset vOffset = Nova.FlatSaveData.CreateGameObjectsVector(fbb, flatGameObjects);
        Nova.FlatSaveData.StartFlatSaveData(fbb);
        Nova.FlatSaveData.AddGameObjects(fbb, vOffset);
        Offset<Nova.FlatSaveData> offset = Nova.FlatSaveData.EndFlatSaveData(fbb);
        Nova.FlatSaveData.FinishFlatSaveDataBuffer(fbb, offset);

        string filePath = saveFolder + "/" + fileName;
        if (!Directory.Exists(saveFolder))
            Directory.CreateDirectory(saveFolder);

        using (var stream = new MemoryStream(fbb.DataBuffer.Data, fbb.DataBuffer.Position, fbb.Offset))
        {
            System.IO.File.WriteAllBytes(filePath, stream.ToArray());
        }
        return filePath;
    }

    #region Objekt zu FlatBuffer
    private static Offset<FlatGameObject> GameObjectToFlatGameObject(GameObject gameObject)
    {
        // Überprüfe ob das GameObject mit einem prefab verbunden ist und speichere den Pfad
        string prefabPathString = null;
        GameObject prefab = (GameObject)PrefabUtility.GetPrefabParent(gameObject);
        if (prefab != null)
        {
            string path = AssetDatabase.GetAssetPath(prefab);
            Debug.Log("prefab path:" + path);
            prefabPathString = path;
        }
        StringOffset prefabPath = prefabPathString != null ? fbb.CreateString(prefabPathString) : fbb.CreateString("");
        StringOffset name = fbb.CreateString(gameObject.name);
        StringOffset tag = fbb.CreateString(gameObject.tag);

        List<Offset<FlatGameObject>> children = new List<Offset<FlatGameObject>>();
        foreach(Transform transformChild in gameObject.transform)
            children.Add(GameObjectToFlatGameObject(transformChild.gameObject));
        VectorOffset childrenOffset = FlatGameObject.CreateChildrenVector(fbb, children.ToArray());

        List<Offset<FlatComponent>> components = new List<Offset<FlatComponent>>();
        foreach (Component c in gameObject.GetComponents(typeof(Component)))
            components.Add(ComponentToFlatComponent(c));
        VectorOffset componentOffset = FlatGameObject.CreateComponentsVector(fbb, components.ToArray());

        return FlatGameObject.CreateFlatGameObject(fbb, name, prefabPath, tag, gameObject.layer, gameObject.isStatic, gameObject.activeSelf, childrenOffset, componentOffset);
    }

    private static Offset<FlatComponent> ComponentToFlatComponent(Component c)
    {
        if (c is Transform)
        {
            Transform t = (Transform)c;
            StringOffset type = fbb.CreateString("Transform");
            string[] keys = new string[] { "Position", "Rotation", "Scale" };
            Vector3[] values = new Vector3[] { t.localPosition, t.localRotation.eulerAngles, t.localScale };
            VectorOffset components = BuildVectorOffset(keys, values, Type.COMPONENT);
            return FlatComponent.CreateFlatComponent(fbb, type, components);
        }
        else if (c is MonoBehaviour)
        {
            MonoBehaviour script = (MonoBehaviour)c;
            StringOffset type = fbb.CreateString("MonoBehaviour");

            Dictionary<string, string> stringList = new Dictionary<string, string>();
            Dictionary<string, bool> boolList = new Dictionary<string, bool> ();
            Dictionary<string, int> intList = new Dictionary<string, int>();
            Dictionary<string, float> floatList = new Dictionary<string, float>();
            Dictionary<string, Component> componentList = new Dictionary<string, Component>();
            Dictionary<string, GameObject> gameObjectList = new Dictionary<string, GameObject>();

            var fields = script.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(prop => Attribute.IsDefined(prop, typeof(SaveThis)));
            foreach(var field in fields)
            {
                if (field.FieldType.Name == "String")
                    stringList.Add(field.Name, field.GetValue(script).ToString());
                else if (field.FieldType.Name == "Int32")
                    intList.Add(field.Name, (int)field.GetValue(script));
                else if (field.FieldType.Name == "Boolean")
                    boolList.Add(field.Name, (bool)field.GetValue(script));
                else if (field.FieldType.Name == "Float")
                    floatList.Add(field.Name, (float)field.GetValue(script));
                else if (field.FieldType.Name == "GameObject")
                    gameObjectList.Add(field.Name, (GameObject)field.GetValue(script));
                else if (field.FieldType.Name == "Component")
                    componentList.Add(field.Name, (Component)field.GetValue(script));
            }

            VectorOffset stringVector = BuildVectorOffset(stringList.Keys.ToArray<string>(), stringList.Values.ToArray<string>(), Type.COMPONENT);
            VectorOffset boolVector = BuildVectorOffset(boolList.Keys.ToArray<string>(), boolList.Values.ToArray<bool>(), Type.COMPONENT);
            VectorOffset intVector = BuildVectorOffset(intList.Keys.ToArray<string>(), intList.Values.ToArray<int>(), Type.COMPONENT);
            VectorOffset floatVector = BuildVectorOffset(floatList.Keys.ToArray<string>(), floatList.Values.ToArray<float>(), Type.COMPONENT);
            VectorOffset gameObjectVector = BuildVectorOffset(gameObjectList.Keys.ToArray<string>(), gameObjectList.Values.ToArray<GameObject>(), Type.COMPONENT);
            VectorOffset componentVector = BuildVectorOffset(componentList.Keys.ToArray<string>(), componentList.Values.ToArray<Component>(), Type.COMPONENT);
            return FlatComponent.CreateFlatComponent(fbb, type, stringVector, intVector, boolVector, floatVector, gameObjectVector, componentVector);
        }
        else
            throw (new System.Exception("Unsupported Component type: " + c.GetType()));

    }
    #endregion

    #region Save Type Builders
    private static VectorOffset BuildVectorOffset(string[] keys, bool[] values, Type type)
    {
        Offset<BoolValuePair>[] boolValuePairList = new Offset<BoolValuePair>[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            boolValuePairList[i] = BoolValuePair.CreateBoolValuePair(fbb, fbb.CreateString(keys[i]), values[i]);

        switch (type)
        {
            case Type.DATA:
                return Nova.FlatSaveData.CreateBoolValuesVector(fbb, boolValuePairList);
            case Type.COMPONENT:
                return FlatComponent.CreateBoolValuesVector(fbb, boolValuePairList);
            default:
                throw (new System.Exception("Invalid Type"));
        }
    }

    private static VectorOffset BuildVectorOffset(string[] keys, int[] values, Type type)
    {
        Offset<IntValuePair>[] IntValuePairList = new Offset<IntValuePair>[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            IntValuePairList[i] = IntValuePair.CreateIntValuePair(fbb, fbb.CreateString(keys[i]), values[i]);

        switch (type)
        {
            case Type.DATA:
                return Nova.FlatSaveData.CreateIntValuesVector(fbb, IntValuePairList);
            case Type.COMPONENT:
                return FlatComponent.CreateIntValuesVector(fbb, IntValuePairList);
            default:
                throw (new System.Exception("Invalid Type"));
        }
    }

    private static VectorOffset BuildVectorOffset(string[] keys, float[] values, Type type)
    {
        Offset<FloatValuePair>[] FloatValuePairList = new Offset<FloatValuePair>[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            FloatValuePairList[i] = FloatValuePair.CreateFloatValuePair(fbb, fbb.CreateString(keys[i]), values[i]);

        switch (type)
        {
            case Type.DATA:
                return Nova.FlatSaveData.CreateFloatValuesVector(fbb, FloatValuePairList);
            case Type.COMPONENT:
                return FlatComponent.CreateFloatValuesVector(fbb, FloatValuePairList);
            default:
                throw (new System.Exception("Invalid Type"));
        }
    }

    private static VectorOffset BuildVectorOffset(string[] keys, string[] values, Type type)
    {
        Offset<StringValuePair>[] StringValuePairList = new Offset<StringValuePair>[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            StringValuePairList[i] = StringValuePair.CreateStringValuePair(fbb, fbb.CreateString(keys[i]), fbb.CreateString(values[i]));

        switch (type)
        {
            case Type.DATA:
                return Nova.FlatSaveData.CreateStringValuesVector(fbb, StringValuePairList);
            case Type.COMPONENT:
                return FlatComponent.CreateStringValuesVector(fbb, StringValuePairList);
            default:
                throw (new System.Exception("Invalid Type"));
        }
    }

    private static VectorOffset BuildVectorOffset(string[] keys, Vector2[] values, Type type)
    {
        Offset<Vector2ValuePair>[] Vector2ValuePairList = new Offset<Vector2ValuePair>[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            Vector2ValuePairList[i] = (BuildVector2ValuePair(keys[i], values[i]));

        switch (type)
        {
            case Type.DATA:
                return Nova.FlatSaveData.CreateVector2ValuesVector(fbb, Vector2ValuePairList);
            case Type.COMPONENT:
                return FlatComponent.CreateVector2ValuesVector(fbb, Vector2ValuePairList);
            default:
                throw (new System.Exception("Invalid Type"));
        }
    }

    private static VectorOffset BuildVectorOffset(string[] keys, Vector3[] values, Type type)
    {
        Offset<Vector3ValuePair>[] vector3ValuePairList = new Offset<Vector3ValuePair>[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            vector3ValuePairList[i] = (BuildVector3ValuePair(keys[i], values[i]));

        switch (type)
        {
            case Type.DATA:
                return Nova.FlatSaveData.CreateVector3ValuesVector(fbb, vector3ValuePairList);
            case Type.COMPONENT:
                return FlatComponent.CreateVector3ValuesVector(fbb, vector3ValuePairList);
            default:
                throw (new System.Exception("Invalid Type"));
        }
    }
    private static VectorOffset BuildVectorOffset(string[] keys, Vector4[] values, Type type)
    {
        Offset<Vector4ValuePair>[] Vector4ValuePairList = new Offset<Vector4ValuePair>[keys.Length];
        for(int i = 0; i < keys.Length; i++)
            Vector4ValuePairList[i] = (BuildVector4ValuePair(keys[i], values[i]));

        switch (type)
        {
            case Type.DATA:
                return Nova.FlatSaveData.CreateVector4ValuesVector(fbb, Vector4ValuePairList);
            case Type.COMPONENT:
                return FlatComponent.CreateVector4ValuesVector(fbb, Vector4ValuePairList);
            default:
                throw (new System.Exception("Invalid Type"));
        }
    }

    private static VectorOffset BuildVectorOffset(string[] keys, Component[] values, Type type)
    {
        Offset<ComponentValuePair>[] componentValuePairList = new Offset<ComponentValuePair>[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            componentValuePairList[i] = ComponentValuePair.CreateComponentValuePair(fbb, fbb.CreateString(keys[i]), ComponentToFlatComponent(values[i]));

        switch (type)
        {
            case Type.DATA:
                throw (new System.Exception("Invalid Type"));
            case Type.COMPONENT:
                return FlatComponent.CreateComponentsVector(fbb, componentValuePairList);
            default:
                throw (new System.Exception("Invalid Type"));
        }
    }

    private static VectorOffset BuildVectorOffset(string[] keys, GameObject[] values, Type type)
    {
        Offset<GameObjectValuePair>[] gameObjectValuePairList = new Offset<GameObjectValuePair>[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            gameObjectValuePairList[i] = GameObjectValuePair.CreateGameObjectValuePair(fbb, fbb.CreateString(keys[i]), GameObjectToFlatGameObject(values[i]));

        switch (type)
        {
            case Type.DATA:
                throw (new System.Exception("Invalid Type"));
            case Type.COMPONENT:
                return FlatComponent.CreateGameObjectsVector(fbb, gameObjectValuePairList);
            default:
                throw (new System.Exception("Invalid Type"));
        }
    }

    private static Offset<Vector2ValuePair> BuildVector2ValuePair(string key, Vector2 vec2)
    {
        StringOffset keyOffset = fbb.CreateString(key);
        Vector2ValuePair.StartVector2ValuePair(fbb);
        Vector2ValuePair.AddName(fbb, keyOffset);
        Vector2ValuePair.AddValue(fbb, FlatVector2.CreateFlatVector2(fbb, vec2.x, vec2.y));
        return Vector2ValuePair.EndVector2ValuePair(fbb);
    }

    private static Offset<Vector3ValuePair> BuildVector3ValuePair(string key, Vector3 vec3)
    {
        StringOffset keyOffset = fbb.CreateString(key);
        Vector3ValuePair.StartVector3ValuePair(fbb);
        Vector3ValuePair.AddName(fbb, keyOffset);
        Vector3ValuePair.AddValue(fbb, FlatVector3.CreateFlatVector3(fbb, vec3.x, vec3.y, vec3.z));
        return Vector3ValuePair.EndVector3ValuePair(fbb);
    }

    private static Offset<Vector4ValuePair> BuildVector4ValuePair(string key, Vector4 vec4)
    {
        StringOffset keyOffset = fbb.CreateString(key);
        Vector4ValuePair.StartVector4ValuePair(fbb);
        Vector4ValuePair.AddName(fbb, keyOffset);
        Vector4ValuePair.AddValue(fbb, FlatVector4.CreateFlatVector4(fbb, vec4.w, vec4.x, vec4.y, vec4.z));
        return Vector4ValuePair.EndVector4ValuePair(fbb);
    }
    #endregion
    #endregion

    #region Laden
    public static FlatSaveData Load(string fileName)
    {
        FlatSaveData saveData = new FlatSaveData();
        ByteBuffer bb = new ByteBuffer(File.ReadAllBytes(saveFolder + "/" + fileName));
        Nova.FlatSaveData fSaveData = Nova.FlatSaveData.GetRootAsFlatSaveData(bb);

        for (int i = 0; i < fSaveData.GameObjectsLength; i++)
        {
            GameObject gameObject = FlatGameObjectToGameObject(fSaveData.GetGameObjects(i));
            saveData.gameObjects.Add(gameObject);
        }

        return saveData;
    }

    #region FlatBuffer zu Objekt
    private static GameObject FlatGameObjectToGameObject(FlatGameObject fGameObject)
    {
        GameObject gameObject = new GameObject();
        gameObject.name = fGameObject.Name;
        gameObject.tag = fGameObject.Tag;
        gameObject.layer = fGameObject.Layer;
        gameObject.SetActive(fGameObject.IsActive);
        gameObject.isStatic = fGameObject.IsStatic;
        if (fGameObject.ChildrenLength > 0)
        {
            for (int i = 0; i < fGameObject.ChildrenLength; i++)
                FlatGameObjectToGameObject(fGameObject.GetChildren(i)).transform.parent = gameObject.transform;
        }
        if(fGameObject.ComponentsLength > 0)
        {
            for (int i = 0; i < fGameObject.ComponentsLength; i++)
            {
                FlatComponent fComponent = (FlatComponent)fGameObject.GetComponents(i);
                if(fComponent.Type == "Transform")
                {
                    //Transform transform = gameObject.transform;
                }
            }
        }
        return gameObject;
    }

    //private static Component FlatComponentToComponent(FlatComponent fc)
    //{
    //}
    #endregion
    #endregion
}
