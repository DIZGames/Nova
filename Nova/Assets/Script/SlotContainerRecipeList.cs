using Assets.Script.Interface;
using Assets.Script.RecipeSystem;
using Assets.Script.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class SlotContainerRecipeList : MonoBehaviour, ISlotContainerRecipeList {

        [SerializeField]
        private Transform recipeList;
        [SerializeField]
        private RecipeList recipeListDB;
        [SerializeField]
        private ShipManagerUnitType unitType;
        [SerializeField]
        private SlotContainerQueueList slotContainerQueueList; 

        private ShipManager shipManager;

        private GameObject goSlotRecipe;

        void Start() {
            this.shipManager = transform.root.GetComponent<ShipManager>();
            goSlotRecipe = (GameObject)Resources.Load("Prefab/SlotRecipe");

            fillList();
        }

        private void fillList() {

            for (int i = 0; i < recipeList.childCount; i++) {
                Destroy(recipeList.GetChild(i).gameObject);
            }

            for (int j = 0; j < recipeListDB.Count(); j++) {
                GameObject gO = (GameObject)Instantiate(goSlotRecipe);
                gO.transform.SetParent(recipeList);

                SlotContainerRecipe slotContainerRecipe = gO.GetComponent<SlotContainerRecipe>();

                slotContainerRecipe.Recipe = recipeListDB.RecipeByIndex(j);

                if (checkRecipe(recipeListDB.RecipeByIndex(j))) {
                    slotContainerRecipe.Buildable(true);
                }
                else {
                    slotContainerRecipe.Buildable(false);
                }
            }
        }

        public void ButtonPress(SlotContainerRecipe slotContainerRecipe) {

            if (checkRecipe(slotContainerRecipe.Recipe)) {
                slotContainerRecipe.Buildable(true);
                slotContainerQueueList.AddQueue(slotContainerRecipe);
            }
            else {
                slotContainerRecipe.Buildable(false);
            }
        }

        private bool checkRecipe(Recipe recipe) {

            for (int i = 0; i < recipe.ingredients.Count; i++) {
                int itemCount = shipManager.Count(recipe.ingredients[i].item.name, unitType);

                if (itemCount < recipe.ingredients[i].count) {
                    return false;
                }
            }

            return true;
        }

    }
}
