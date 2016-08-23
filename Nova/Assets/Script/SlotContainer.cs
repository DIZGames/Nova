using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Script.ItemSystem;
using UnityEngine.EventSystems;
using System;
using Assets.Script.Interface;

[System.Serializable]
public class SlotContainer : MonoBehaviour {

    [SerializeField]
    private ItemBase _ItemBase;
    [SerializeField]
    private Text Text;
    [SerializeField]
    private Image Image;

    public ItemBase ItemBase
    {
        get {
            return _ItemBase;
        }
        set {
            _ItemBase = value;
            this.Image.sprite = _ItemBase.icon;
            this.Text.text = _ItemBase.stack.ToString();
        }
    }

    void Update() {
        if(_ItemBase != null)
            this.Text.text = _ItemBase.stack.ToString();

        if (_ItemBase.stack == 0) {
            Destroy(gameObject);
        }
    }
}
