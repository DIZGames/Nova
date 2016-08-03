using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.ItemSystem.Create {
    public class CraeteItemBlock {

        [MenuItem("Assets/Create/ItemBlock")]
        public static void CreateMyAsset() {
            ItemBlock asset = ScriptableObject.CreateInstance<ItemBlock>();

            AssetDatabase.CreateAsset(asset, "Assets/Resources/ScriptableObject/ItemBlock.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}
