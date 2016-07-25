using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.ItemSystem.Create
{
    public class CreateItemAttribute
    {
        [MenuItem("Assets/Create/ItemAttribute")]
        public static void CreateMyAsset()
        {
            Damage asset = ScriptableObject.CreateInstance<Damage>();

            AssetDatabase.CreateAsset(asset, "Assets/Resources/ItemAttribute.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}
