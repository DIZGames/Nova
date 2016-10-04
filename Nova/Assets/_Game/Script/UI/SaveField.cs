using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveField : MonoBehaviour {

    public Image image;
    public Color defaultColor;
    public Color selectedColor;
    public SaveLoadGUI saveLoadGUI;
    public Text textField;
    public Text dateField;

    void Start()
    {
        if (saveLoadGUI == null)
            saveLoadGUI = (SaveLoadGUI)GameObject.Find("SaveLoad").GetComponent("SaveLoadGUI");
        saveLoadGUI.saveFields.Add(this);
    }

    public void Click()
    {
        saveLoadGUI.SetSelectedField(this);
    }

    public void SetImageColorToDefault()
    {
        image.color = defaultColor;
    }

    public void SetImageColorToSelected()
    {
        image.color = selectedColor;
    }
}
