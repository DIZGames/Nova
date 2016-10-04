using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadGUI : MonoBehaviour {

    public List<SaveField> saveFields { get; private set; }
    public InputField inputField;
    public GameObject saveFieldPrefab;
    public Transform saveList;
    public GameObject saveButton;
    public bool newScene;
    public bool showSave;
    private SaveField selectedField;

    void Awake()
    {
        saveFields = new List<SaveField>();
    }

    void Start()
    {
        if (!showSave)
            saveButton.SetActive(false);
        Updatelist();
    }

    void OnEnable()
    {
        Updatelist();
    }

    private void Updatelist()
    {
        for (int i = showSave ? 1 : 0; i < saveList.childCount; i++)
            Destroy(saveList.GetChild(i).gameObject);

        foreach(string fileName in SaveManager.GetAllSaveFiles(true))
        {
            GameObject saveFieldGO = Instantiate<GameObject>(saveFieldPrefab);
            saveFieldGO.transform.SetParent(saveList);
            SaveField saveField = saveFieldGO.GetComponent<SaveField>();
            saveField.textField.text = fileName.Replace(".sav","").Replace("Savegames\\", "");
            saveField.dateField.text = File.GetLastWriteTime(fileName).ToString();
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
        if (selectedField != null)
        {
            if (selectedField.textField.text != "**** New Game ****")
                SaveManager.DeleteSaveGame(selectedField.textField.text);
            SetSelectedField((SaveField)saveFieldPrefab.GetComponent("SaveField"));
            Updatelist();
        }
    }

    public void ClickLoad()
    {
        if (selectedField != null && selectedField.textField.text != "**** New Game ****")
        {
            SaveData saveData = SaveManager.Load(selectedField.textField.text);
            if (newScene)
            {
                Global.saveData = saveData;
                SceneManager.LoadScene("Game");
            }
            else
            {
                foreach (SaveGameObject sgo in saveData.RootGameObjetcs)
                    sgo.ToGameObject();
            }

        }

    }

    public void ClicklCancel()
    {
        gameObject.SetActive(false);
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
