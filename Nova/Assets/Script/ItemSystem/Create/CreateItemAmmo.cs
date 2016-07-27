using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.ItemSystem.Create {
    public class CreateItemAmmo {

        [MenuItem("Assets/Create/ItemAmmo")]
        public static void CreateMyAsset()
        {
            //ItemAmmo asset = ScriptableObject.CreateInstance<ItemAmmo>();

            //AssetDatabase.CreateAsset(asset, "Assets/Resources/ScriptableObject/ItemAmmo.asset");
            //AssetDatabase.SaveAssets();

            //EditorUtility.FocusProjectWindow();

            //Selection.activeObject = asset;
        }
    }
}
