using UnityEngine;
using System.Collections;
using System.IO;

public class SaveLoad {

    public static void Save()
    {
        SaveFile saveFile = new SaveFile();
        foreach(Ship ship in GameObject.FindObjectsOfType<Ship>())
        {
            saveFile.ships.Add(ship.gameObject);
        }

        File.WriteAllText("Savegames/save1", JsonUtility.ToJson(saveFile));
        Debug.Log("Gespeichert");
    }
	
}
