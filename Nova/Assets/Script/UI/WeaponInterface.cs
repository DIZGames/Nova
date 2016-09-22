using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Script.ItemSystem;

public class WeaponInterface : MonoBehaviour {

    [SerializeField]
    private Text title;
    [SerializeField]
    private Text loadedProjectiles;
    [SerializeField]
    private Text inStock;

    [SerializeField]
    private ItemTool _ItemTool;

    public ItemTool ItemTool {
        get {
            return _ItemTool;
        }
        set {
            _ItemTool = value;
            title.text = _ItemTool.itemName;
        }
    }

    void Update () {

        if (ItemTool != null) {
            loadedProjectiles.text = _ItemTool.loadedProjectiles.ToString();
            inStock.text = _ItemTool.inStock.ToString();
        }

	}
}
