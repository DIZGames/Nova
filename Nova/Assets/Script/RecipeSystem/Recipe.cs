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
        public int ticksToBuild;

        public List<Ingredient> ingredients;
     
        public Ingredient result;
    }
}
