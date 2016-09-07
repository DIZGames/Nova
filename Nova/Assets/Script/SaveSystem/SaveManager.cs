using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager {

    private static string saveFolder = "Savegames";
    private static List<GameObject> objectsToSave = new List<GameObject>();
    private static BinaryFormatter bf = new BinaryFormatter();

    //Erzeuge SaveData nur noch hierdrin, siehe "partially" keyword
    //public static SaveData CreateSaveData()
    //{

    //}

    /// <summary>
    /// Speichert alle GameObjects in die ein SaveThis Script besitzen
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string Save(string fileName)
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
    public static string Save(SaveData saveFile, string fileName)
    {
        string filePath = saveFolder + "/" + fileName + ".bin";
        using (var stream = new MemoryStream())
        {
            bf.Serialize(stream, saveFile);
            File.WriteAllBytes(filePath, stream.ToArray());
        }
        return filePath;
    }

    public static SaveData Load(string fileName)
    {
        string filePath = saveFolder + "/" + fileName + ".bin";
        SaveData sd = null;
        using (var stream = File.OpenRead(filePath))
        {
            sd = (SaveData)bf.Deserialize(stream);
        }
        return sd;
    }

    public static string[] GetAllSaveFiles()
    {
        return Directory.GetFiles(saveFolder);
    }

    public static void DeleteSaveGame(string fileName)
    {
        File.Delete(saveFolder + "/" + fileName + ".bin");
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
