using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.ItemSystem.Create {
    public class CreateItemMaterial {

        [MenuItem("Assets/Create/ItemMaterial ")]
        public static void CreateMyAsset() {
            ItemMaterial asset = ScriptableObject.CreateInstance<ItemMaterial>();

            AssetDatabase.CreateAsset(asset, "Assets/Resources/ScriptableObject/Item/Material/ItemMaterial .asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;

        }
    }
}
