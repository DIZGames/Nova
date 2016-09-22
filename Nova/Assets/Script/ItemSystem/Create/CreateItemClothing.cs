using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.ItemSystem.Create {
    public class CreateItemClothing {

        [MenuItem("Assets/Create/ItemClothing")]
        public static void CreateMyAsset() {
            ItemClothing asset = ScriptableObject.CreateInstance<ItemClothing>();

            AssetDatabase.CreateAsset(asset, "Assets/Resources/ScriptableObject/Item/Clothing/ItemClothing.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}
