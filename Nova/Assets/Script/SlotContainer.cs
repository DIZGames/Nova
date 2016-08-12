using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Script.ItemSystem;
using UnityEngine.EventSystems;
using System;

[System.Serializable]
public class SlotContainer : MonoBehaviour {

    [SerializeField]
    private ItemValues _ItemValues;

    [SerializeField]
    private Text Text;
    [SerializeField]
    private Image Image;

    public ItemValues Item
    {
        get {
            return _ItemValues;
        }
        set {
            _ItemValues = value;
            this.Image.sprite = _ItemValues.itemBase.icon;
            this.Text.text = _ItemValues.stack.ToString();
        }
    }

    void Update() {
        if(_ItemValues != null)
            this.Text.text = _ItemValues.stack.ToString();

        if (_ItemValues.stack == 0) {
            Destroy(gameObject);          
        }
    }

}
