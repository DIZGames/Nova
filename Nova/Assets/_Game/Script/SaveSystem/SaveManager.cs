using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager {

    private static string saveFolder = "/Savegames"; 
    private static string fileEnding = ".sav";
    private static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    private static List<GameObject> objectsToSave = new List<GameObject>();

    static SaveManager() {
        //TODO Prefabs laden wenn savegame geladen wird anstatt alle im speicher zu halten?
        try
        {
            foreach (GameObject go in Resources.LoadAll("", typeof(GameObject)).Cast<GameObject>())
            {
                SaveMe saveMe = go.GetComponent<SaveMe>();
                if (saveMe != null && !string.IsNullOrEmpty(saveMe.prefabId))
                {
                    prefabs.Add(saveMe.prefabId, go);
                }
            }
        } catch(System.ArgumentException e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            Resources.UnloadUnusedAssets();
        }
    }

    /// <summary>
    /// Speichert alle GameObjects, die ein SaveMe Script besitzen
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string Save(string saveName, string fileName = null, SaveFormat format = SaveFormat.BINARY)
    {
        if (!Directory.Exists(saveFolder))
            Directory.CreateDirectory(saveFolder);

        if (fileName == null)
            fileName = saveName.Replace(" ", "_");

        SaveData saveData = new SaveData(saveName);

        foreach (GameObject go in objectsToSave)
            saveData.RootGameObjetcs.Add(new SaveGameObject(go));
        return Save(saveData, fileName);
    }

    /// <summary>
    /// Speichert die übergebene SaveData Instanz
    /// </summary>
    /// <param name="saveFile"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string Save(SaveData saveFile, string fileName, SaveFormat format = SaveFormat.BINARY)
    {
        string filePath = saveFolder + "/" + fileName + fileEnding;
        if (format == SaveFormat.BINARY)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                bf.Serialize(stream, saveFile);
                File.WriteAllBytes(filePath, stream.ToArray());
            }
        }
        else
            throw new System.Exception("Format not supported");
        return filePath;
    }

    public static SaveData Load(string fileName, SaveFormat format = SaveFormat.BINARY)
    {
        string filePath = saveFolder + "/" + fileName + fileEnding;
        SaveData sd = null;
        if (format == SaveFormat.BINARY)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var stream = File.OpenRead(filePath))
            {
                sd = (SaveData)bf.Deserialize(stream);
            }
        }
        else
            throw new System.Exception("Format not supported.");
        return sd;
    }

    public static List<string> GetAllSaveFiles(bool withPath = false)
    {
        List<string> files = new List<string>();

        if (Directory.Exists(saveFolder))
        {
            foreach (string file in Directory.GetFiles(saveFolder))
            {
                if(!withPath)
                    file.Replace(saveFolder + "\\", "").Replace(fileEnding, "");
                files.Add(file);
            }
        }
        return files;
    }

    public static void DeleteSaveGame(string fileName)
    {
        File.Delete(saveFolder + "/" + fileName + fileEnding);
    }

    public static GameObject getPrefab(string id)
    {
        return prefabs[id];
    }

    public static void Add(GameObject go)
    {
        objectsToSave.Add(go);
    }

    public static void Remove(GameObject go)
    {
        objectsToSave.Remove(go);
    }
}
