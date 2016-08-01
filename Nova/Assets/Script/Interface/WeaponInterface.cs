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
    private ItemToolValues _ItemToolValues;

    public ItemToolValues ItemToolValues {
        get {
            return _ItemToolValues;
        }
        set {
            _ItemToolValues = value;
            title.text = _ItemToolValues.Name;
        }
    }

    // Update is called once per frame
    void Update () {

        if (ItemToolValues != null) {
            loadedProjectiles.text = _ItemToolValues.loadedProjectiles.ToString();
            inStock.text = _ItemToolValues.inStock.ToString();
        }

	}
}
