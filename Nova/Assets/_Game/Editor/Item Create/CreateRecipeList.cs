﻿using UnityEngine;
using UnityEditor;
using Assets.Script.RecipeSystem;

public class CreateRecipeList {

    [MenuItem("Assets/Create/RecipeList")]
    public static void CreateMyAsset()
    {
        RecipeList asset = ScriptableObject.CreateInstance<RecipeList>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/ScriptableObject/Recipe/RecipeDataBase.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
