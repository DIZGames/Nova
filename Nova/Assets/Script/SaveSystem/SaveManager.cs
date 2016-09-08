using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager {

    private static string saveFolder = "Savegames";
    private static string fileEnding = ".sav";


    private static List<GameObject> objectsToSave = new List<GameObject>();
    private static BinaryFormatter bf = new BinaryFormatter();

    /// <summary>
    /// Speichert alle GameObjects, die ein SaveMe Script besitzen
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string Save(string fileName, SaveFormat format = SaveFormat.BINARY)
    {
        SaveData saveData = new SaveData();
        foreach(GameObject go in objectsToSave)
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
            using (var stream = File.OpenRead(filePath))
            {
                sd = (SaveData)bf.Deserialize(stream);
            }
        }
        else
            throw new System.Exception("Format not supported.");
        return sd;
    }

    public static List<string> GetAllSaveFiles()
    {
        List<string> files = new List<string>();
        foreach (string file in Directory.GetFiles(saveFolder))
        {
            file.Replace(saveFolder + "\\", "").Replace(fileEnding, "");
            files.Add(file);
        }
        return files;
    }

    public static void DeleteSaveGame(string fileName)
    {
        File.Delete(saveFolder + "/" + fileName + fileEnding);
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
