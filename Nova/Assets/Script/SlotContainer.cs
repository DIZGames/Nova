using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Script.ItemSystem;

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

}
