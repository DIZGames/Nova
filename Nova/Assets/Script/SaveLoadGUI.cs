using UnityEngine;
using System.Collections;

public class SaveLoadGUI : MonoBehaviour {

    public GameObject test;

	public void ClickSave()
    {
        SaveGameManager.Save(test, SaveGameManager.SaveFileFormat.BINARY);
    }

    public void ClickLoad()
    {
        SaveGameManager.Load("save1", SaveGameManager.SaveFileFormat.BINARY);
    }
}
