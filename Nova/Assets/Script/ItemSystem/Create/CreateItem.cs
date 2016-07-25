using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.ItemSystem.Create
{
    public class CreateItem
    {
        [MenuItem("Assets/Create/Item")]
        public static void CreateMyAsset()
        {
            Item asset = ScriptableObject.CreateInstance<Item>();

            AssetDatabase.CreateAsset(asset, "Assets/Resources/Item.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}
