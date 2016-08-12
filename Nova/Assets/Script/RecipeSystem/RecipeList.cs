using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.RecipeSystem {
    public class RecipeList : ScriptableObject{

        [SerializeField]
        private List<Recipe> recipeList;

        public Recipe getRecipeByName(string Name) {
            for (int i = 0; i < recipeList.Count; i++) {
                if (Name == recipeList[i].name) {
                    return recipeList[i];
                }
            }

            return null;
        }

        public Recipe getRecipeByIndex(int index) {
            return recipeList[index];
        }

        public int getCount() {
            return recipeList.Count;
        }

    }
}
