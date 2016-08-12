using UnityEngine;
using System.Collections;
using UnityEditor;
using Assets.Script.RecipeSystem;

public class CreateRecipeList {

    [MenuItem("Assets/Create/RecipeList")]
    public static void CreateMyAsset()
    {
        RecipeList asset = ScriptableObject.CreateInstance<RecipeList>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/RecipeDataBase.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
