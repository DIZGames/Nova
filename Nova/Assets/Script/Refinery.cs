using Assets.Script.Interface;
using Assets.Script.RecipeSystem;
using Assets.Script.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class Refinery : MonoBehaviour, ISlotContainerRecipeList, IOpenUI {
        public ShipManagerUnitType unitType;


        private ShipManager shipManager;
        private TerminalManager terminalManger;

        [SerializeField]
        private Transform buttonList;
        [SerializeField]
        private SlotList slotList;

        private List<SlotContainerRecipe> slotContainerRecipeList;
        private GameObject slotRecipe;

        private bool isProducing;

        private RecipeList refineryRecipes;
        private InterfaceManager interfaceManager;


        // Use this for initialization
        void Start() {
            Debug.Log("Start Methode");

            shipManager = transform.root.GetComponent<ShipManager>();
            terminalManger = transform.root.GetComponent<TerminalManager>();
            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();



            terminalManger.Add("Refinery",transform.parent.GetComponent<SpriteRenderer>().sprite,this.transform);

            refineryRecipes = (RecipeList)Resources.Load("RefineryRecipes");
            slotRecipe = (GameObject)Resources.Load("Prefab/SlotRecipe");


            slotContainerRecipeList = new List<SlotContainerRecipe>();

            loadRecipeListToUI();

            ButtonListToSlotContainerRecipeList();
            UpdateList();
        }



        public void CraftRecipe(SlotContainerRecipe slotContainerRecipe) {
            bool isBuildable = checkRecipe(slotContainerRecipe);
            bool hasFreeSlot = slotList.CheckNextFreeSlot();
            slotContainerRecipe.Buildable(isBuildable);

            if (isBuildable && hasFreeSlot) {
                DecreaseIngredients(slotContainerRecipe);
                Recipe recipe = slotContainerRecipe.Recipe;
                slotList.addItemToItemStack(recipe.result.item,recipe.result.count);
            }
            else {
                Debug.Log("Kann nicht gecraftet werden!");
            }
        }

        private void ButtonListToSlotContainerRecipeList() {
            slotContainerRecipeList.Clear();

            for (int i = 0; i < buttonList.childCount; i++) {
                slotContainerRecipeList.Add(buttonList.GetChild(i).GetComponent<SlotContainerRecipe>());
            }
        }

        private void UpdateList() {
            for (int i = 0; i < slotContainerRecipeList.Count; i++) {
                Recipe recipe = slotContainerRecipeList[i].Recipe;


                bool isBuildable = checkRecipe(slotContainerRecipeList[i]);

                //Backgroundcolor
                slotContainerRecipeList[i].Buildable(isBuildable);

            }
        }

        private void loadRecipeListToUI() {

            for (int i = 0; i < buttonList.childCount; i++) {
                Destroy(buttonList.GetChild(i).gameObject);
            }

            for (int j = 0; j < refineryRecipes.getCount(); j++) {
                GameObject gO = (GameObject)Instantiate(slotRecipe);
                gO.transform.SetParent(buttonList);

                gO.GetComponent<SlotContainerRecipe>().Recipe = refineryRecipes.getRecipeByIndex(j);
            }

        }

        private bool checkRecipe(SlotContainerRecipe slotContainerRecipe) {
            Recipe recipe = slotContainerRecipe.Recipe;

            bool isBuildable = true;

            if (recipe.ingredient1.count != 0) {
                if (shipManager.Count(recipe.ingredient1.item.name, unitType) < recipe.ingredient1.count) {
                    isBuildable = false;
                }
            }
            if (recipe.ingredient2.count != 0) {
                if (shipManager.Count(recipe.ingredient2.item.name, unitType) < recipe.ingredient2.count) {
                    isBuildable = false;
                }
            }
            if (recipe.ingredient3.count != 0) {
                if (shipManager.Count(recipe.ingredient3.item.name, unitType) < recipe.ingredient3.count) {
                    isBuildable = false;
                }
            }
            if (recipe.ingredient4.count != 0) {
                if (shipManager.Count(recipe.ingredient4.item.name, unitType) < recipe.ingredient4.count) {
                    isBuildable = false;
                }
            }
            if (recipe.ingredient5.count != 0) {
                if (shipManager.Count(recipe.ingredient5.item.name, unitType) < recipe.ingredient5.count) {
                    isBuildable = false;
                }
            }


            return isBuildable;
        }

        private void DecreaseIngredients(SlotContainerRecipe slotContainerRecipe) {
            Recipe recipe = slotContainerRecipe.Recipe;

            if (recipe.ingredient1.count != 0) {
                shipManager.Decrease(recipe.ingredient1.item.name, unitType, recipe.ingredient1.count);
            }
            if (recipe.ingredient2.count != 0) {
                shipManager.Decrease(recipe.ingredient2.item.name, unitType, recipe.ingredient2.count);
            }
            if (recipe.ingredient3.count != 0) {
                shipManager.Decrease(recipe.ingredient3.item.name, unitType, recipe.ingredient3.count);
            }
            if (recipe.ingredient4.count != 0) {
                shipManager.Decrease(recipe.ingredient4.item.name, unitType, recipe.ingredient4.count);
            }
            if (recipe.ingredient5.count != 0) {
                shipManager.Decrease(recipe.ingredient5.item.name, unitType, recipe.ingredient5.count);
            }

        }

        public void OpenUI() {
            interfaceManager.setChildOnUIContainer(this.transform);
        }
    }
}
