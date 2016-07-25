using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateItemList {

    [MenuItem("Assets/Create/ItemList")]
    public static void CreateMyAsset()
    {
        ItemList asset = ScriptableObject.CreateInstance<ItemList>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/ItemList.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
