using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using FlatBuffers;
using Nova;
using System.Reflection;

public class SaveGameManager  {

    static string saveFolder = "Savegames";
    const BindingFlags flags = /*BindingFlags.NonPublic | */BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;

    #region Speichern
    public static string Save(SaveData saveFile, string fileName)
    {
        FlatBufferBuilder fbb = new FlatBufferBuilder(1);
        Offset<FlatGameObject>[] flatGameObjects = new Offset<FlatGameObject>[saveFile.gameObjects.Count];
        for(int i = 0; i < saveFile.gameObjects.Count; i++)
        {
            GameObject gameObject = saveFile.gameObjects[i];
            flatGameObjects[i] = GameObjectToFlatGameObject(fbb, gameObject);
        }

        VectorOffset vOffset = FlatSaveData.CreateGameObjectsVector(fbb, flatGameObjects);
        FlatSaveData.StartFlatSaveData(fbb);
        FlatSaveData.AddGameObjects(fbb, vOffset);
        Offset<FlatSaveData> offset = FlatSaveData.EndFlatSaveData(fbb);
        FlatSaveData.FinishFlatSaveDataBuffer(fbb, offset);

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
    private static Offset<FlatGameObject> GameObjectToFlatGameObject(FlatBufferBuilder fbb, GameObject gameObject)
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
            children.Add(GameObjectToFlatGameObject(fbb, transformChild.gameObject));
        VectorOffset childrenOffset = FlatGameObject.CreateChildrenVector(fbb, children.ToArray());
        List<Offset<FlatComponent>> components = new List<Offset<FlatComponent>>();
        foreach (Component c in gameObject.GetComponents(typeof(Component)))
        {
            components.Add(ComponentToFlatComponent(fbb, c));
        }
        VectorOffset componentOffset = FlatGameObject.CreateComponentsVector(fbb, components.ToArray());
        return FlatGameObject.CreateFlatGameObject(fbb, name, prefabPath, tag, gameObject.layer, gameObject.isStatic, gameObject.activeSelf, childrenOffset, componentOffset);
    }

    private static Offset<FlatComponent> ComponentToFlatComponent(FlatBufferBuilder fbb, Component c)
    {
        List<Offset<ValuePair>> valuePairList = new List<Offset<ValuePair>>();
        if(c is Transform)
        {
            StringOffset type = fbb.CreateString("Transform");
            Transform t = (Transform)c;
            StringOffset key = fbb.CreateString("Position");
            StringOffset value = fbb.CreateString(t.localPosition.ToString());
            valuePairList.Add(ValuePair.CreateValuePair(fbb, key, value));
            key = fbb.CreateString("Rotation");
            value = fbb.CreateString(t.localRotation.ToString());
            valuePairList.Add(ValuePair.CreateValuePair(fbb, key, value));
            key = fbb.CreateString("Scale");
            value = fbb.CreateString(t.localScale.ToString());
            valuePairList.Add(ValuePair.CreateValuePair(fbb, key, value));
            VectorOffset components = FlatComponent.CreateValuesVector(fbb, valuePairList.ToArray());
            return FlatComponent.CreateFlatComponent(fbb, type, components);
        }

        return new Offset<FlatComponent>();
    }
    #endregion
    #endregion

    #region Laden
    public static SaveData Load(string fileName)
    {
        SaveData saveData = new SaveData();
        ByteBuffer bb = new ByteBuffer(File.ReadAllBytes(saveFolder + "/" + fileName));
        FlatSaveData fSaveData = FlatSaveData.GetRootAsFlatSaveData(bb);
        for(int i = 0; i < fSaveData.GameObjectsLength; i++)
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
                    // eigene table für jede komponent? verschiedene listen für verschiedene typen?
                    Transform transform = gameObject.transform;
                    string VectorString = fComponent.GetValues(0).Value.Substring(1, fComponent.GetValues(0).Value.Length - 2);
                    string[] vector = VectorString.Split(',');
                    Debug.Log(vector);
                    transform.localPosition = new Vector3(float.Parse(vector[0]), float.Parse(vector[1]), float.Parse(vector[2]));
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
