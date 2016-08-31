using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveGameManager  {

    static string saveFolder = "Savegames";

    public enum SaveFileFormat{
        JSON, BINARY
    }

	public static void Save(List<GameObject> objectsToSave, SaveFileFormat format){

    }

    public static void Save(GameObject objectToSave, SaveFileFormat format)
    {
        SaveFile saveFile = new SaveFile();
        AddToSaveFile(objectToSave, saveFile);
        if (format == SaveFileFormat.BINARY)
            SaveAsBinary(saveFile);
        else if(format == SaveFileFormat.JSON)
            SaveAsJSON(saveFile);
    }

    private static void AddToSaveFile(GameObject gameObject, SaveFile saveFile)
    {
        SaveFile.SaveGameObject saveGO = new SaveFile.SaveGameObject();
        saveGO.name = gameObject.name;
        saveGO.isStatic = gameObject.isStatic;
        saveGO.isActive = gameObject.activeSelf;
        saveGO.layer = gameObject.layer;
        saveGO.tag = gameObject.tag;
        GameObject prefab = (GameObject)PrefabUtility.GetPrefabParent(gameObject);
        if (prefab != null)
        {
            string path = AssetDatabase.GetAssetPath(prefab);
            Debug.Log("prefab path:" + path);
            saveGO.prefabPath = path;
        }
        saveFile.topLevelObjects.Add(saveGO);
    }

    private static void SaveAsBinary(SaveFile file)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (var stream = new MemoryStream()) {
            bf.Serialize(stream, file);
            System.IO.File.WriteAllBytes(saveFolder + "/save1", stream.ToArray());
        }
    }

    private static void SaveAsJSON(SaveFile file)
    {
        System.IO.File.WriteAllText(saveFolder + "/save1", JsonUtility.ToJson(file, true));
    }

    public static void Load(string saveFileName, SaveFileFormat format)
    {
        string filePath = saveFolder + "/" + saveFileName;
        SaveFile file = null;
        if (format == SaveFileFormat.BINARY)
            file = LoadFromBinary(filePath);
        else if (format == SaveFileFormat.JSON)
            file = LoadFromJSON(filePath);
        GameObject go = new GameObject();
        SaveFile.SaveGameObject saveGO = (SaveFile.SaveGameObject)file.topLevelObjects[0];
        go.name = saveGO.name;
        go.SetActive(saveGO.isActive);
        go.isStatic = saveGO.isStatic;
        go.layer = saveGO.layer;
        go.tag = saveGO.tag;
    }

    private static SaveFile LoadFromJSON(string filePath)
    {
        return (SaveFile)JsonUtility.FromJson<SaveFile>(filePath);
    }

    private static SaveFile LoadFromBinary(string filePath)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            return (SaveFile)bf.Deserialize(stream);
        }

    }
}
