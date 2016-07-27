using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlotContainer : MonoBehaviour {

    [SerializeField]
    private GameObject _ItemPrefab;
    [SerializeField]
    public int _Stack;

    [SerializeField]
    private Text Text;
    [SerializeField]
    private Image Image;

    ItemData _ItemData;

    public GameObject prefab{
        get {
            return _ItemPrefab;
        }
        set {
            _ItemPrefab = value;
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

    public ItemData ItemData{
        get {
            return _ItemData;
        }
        set {
            _ItemData = value;
            this.Image.sprite = _ItemData.icon;
        }
    }


}
