using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.RecipeSystem {
    [System.Serializable]
    public class Recipe {

        public string name;
        public string description;

        public int energycosts;

        [SerializeField]
        public Ingredient ingredient1;
        public Ingredient ingredient2;
        public Ingredient ingredient3;
        public Ingredient ingredient4;
        public Ingredient ingredient5;
     
        public Ingredient result;
    }
}
