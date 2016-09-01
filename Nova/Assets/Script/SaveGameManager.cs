using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using FlatBuffers;
using Nova;


public class SaveGameManager  {

    static string saveFolder = "Savegames";

    public static string Save(SaveData saveFile, string fileName)
    {
        FlatBufferBuilder fbb = new FlatBufferBuilder(1);
        Offset<FlatGameObject>[] flatGameObjects = new Offset<FlatGameObject>[saveFile.gameObjects.Count];
        for(int i = 0; i < saveFile.gameObjects.Count; i++)
        {
            GameObject gameObject = saveFile.gameObjects[i];
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
            Offset<FlatGameObject> offsetGO = FlatGameObject.CreateFlatGameObject(fbb, name, prefabPath, tag, gameObject.layer, gameObject.isStatic, gameObject.activeSelf);
            flatGameObjects[i] = offsetGO;
        }
        VectorOffset vOffset = FlatSaveData.CreateGameObjectsVector(fbb, flatGameObjects);
        FlatSaveData.StartFlatSaveData(fbb);
        FlatSaveData.AddGameObjects(fbb, vOffset);
        Offset<FlatSaveData> offset = FlatSaveData.EndFlatSaveData(fbb);
        FlatSaveData.FinishFlatSaveDataBuffer(fbb, offset);
        string filePath = saveFolder + "/" + fileName;
        using (var stream = new MemoryStream(fbb.DataBuffer.Data, fbb.DataBuffer.Position, fbb.Offset))
        {
            System.IO.File.WriteAllBytes(filePath, stream.ToArray());
        }
        return filePath;
    }

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

    private static GameObject FlatGameObjectToGameObject(FlatGameObject fGameObject)
    {
        GameObject gameObject = new GameObject();
        gameObject.name = fGameObject.Name;
        gameObject.tag = fGameObject.Tag;
        gameObject.layer = fGameObject.Layer;
        gameObject.SetActive(fGameObject.IsActive);
        gameObject.isStatic = fGameObject.IsStatic;
        return gameObject;
    }
}
