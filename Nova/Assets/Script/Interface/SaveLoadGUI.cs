using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class SaveLoadGUI : MonoBehaviour {

    public List<SaveField> saveFields { get; private set; }
    public InputField inputField;
    public GameObject saveFieldPrefab;
    public Transform saveList;
    private SaveField selectedField;

    void Awake()
    {
        saveFields = new List<SaveField>();
    }

    void Start()
    {
        Updatelist();
    }

    private void Updatelist()
    {
        for (int i = 1; i < saveList.childCount; i++)
            Destroy(saveList.GetChild(i).gameObject);

        foreach(string filePath in SaveManager.GetAllSaveFiles())
        {
            GameObject saveFieldGO = Instantiate<GameObject>(saveFieldPrefab);
            saveFieldGO.transform.SetParent(saveList);
            SaveField saveField = saveFieldGO.GetComponent<SaveField>();
            saveField.textField.text = filePath.Replace("Savegames\\", "").Replace(".bin", "");
            saveField.dateField.text = File.GetLastWriteTime(filePath).ToString();
            saveField.SetImageColorToDefault();
        }
    }

	public void ClickSave()
    {
        if(selectedField.textField.text == "**** New Game ****")
            SaveManager.Save(inputField.text);
        else
        {
            SaveManager.DeleteSaveGame(selectedField.textField.text);
            SaveManager.Save(inputField.text);
        }
        Updatelist();
    }

    public void ClickDelete()
    {
        if (selectedField.textField.text != "**** New Game ****")
            SaveManager.DeleteSaveGame(selectedField.textField.text);
        Updatelist();
    }

    public void ClickLoad()
    {
        SaveData saveData = SaveManager.Load(selectedField.textField.text);
        foreach (SaveGameObject sgo in saveData.RootGameObjetcs)
            sgo.ToGameObject();
    }

    public void SetSelectedField(SaveField saveField)
    {
        foreach(SaveField sf in saveFields)
            sf.SetImageColorToDefault();

        this.selectedField = saveField;
        saveField.SetImageColorToSelected();

        inputField.text = saveField.textField.text;
    }
}
