using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlotContainer : MonoBehaviour {

    [SerializeField]
    private ItemBase _Item;
    [SerializeField]
    public int _Stack;

    [SerializeField]
    private Text Text;
    [SerializeField]
    private Image Image;

    public ItemBase Item{
        get {
            return _Item;
        }
        set {
                _Item = value;
                this.Image.sprite = _Item.icon;
        }
    }

    public int Stack{
        get {
            return _Stack;
        }
        set {
            _Stack = value;
            this.Text.text = Stack.ToString();
        }
    }

}
