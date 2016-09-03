using UnityEngine;
using System.Collections;

public class SaveLoadGUI : MonoBehaviour {

    public GameObject test;

	public void ClickSave()
    {
        SaveData saveData = new SaveData();
        saveData.gameObjects.Add(test);
        SaveGameManager.Save(saveData, "save");
    }

    public void ClickLoad()
    {
        SaveGameManager.Load("save");
    }
}
