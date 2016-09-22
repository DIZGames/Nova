using Assets.Script.Interface;
using Assets.Script.ItemSystem;
using Assets.Script.RecipeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Script {
    public class SlotContainerQueue : MonoBehaviour, IPointerDownHandler {
        [SerializeField]
        private Recipe recipe;

        [SerializeField]
        private Image image;
        [SerializeField]
        private Slider slider;

        public Recipe Recipe {
            get {
                return recipe;
            }
            set {
                recipe = value;
                CreateList(recipe.ingredients);
                this.image.sprite = recipe.result.item.icon;
                this.slider.maxValue = ingredientsUnique.Count;           
            }
        }

        private List<ItemBase> ingredientsUnique;

        private void CreateList(List<Ingredient> ingredients) {
            ingredientsUnique = new List<ItemBase>();

            for (int j = 0; j < recipe.ingredients.Count; j++) {
                for (int k = 0; k < recipe.ingredients[j].count; k++) {
                    ingredientsUnique.Add(recipe.ingredients[j].item);
                }
            }
        }

        public ItemBase GetIngredientForProgress() {

            return ingredientsUnique[0];

            //for (int i = 0; i < ingredientsUnique.Count; i++) {
            //    return ingredientsUnique[i];      
            //}
            //return null;
        }

        public void RemoveIngredientFromProgress(ItemBase itemBase) {
                ingredientsUnique.Remove(itemBase);
        }

        public void Progress() {

            if (this.slider.value < this.slider.maxValue) {
                this.slider.value++;
            }

            if (this.slider.value == this.slider.maxValue)
                ExecuteEvents.ExecuteHierarchy<ISlotContainerQueueList>(gameObject, null, (x, y) => x.CraftDone(this));
        }

        public void Buildable(bool isBuildable) {
            if (isBuildable) {
                image.color = new Color(255, 255, 255, 255);
            }
            else {
                image.color = new Color(255, 0, 0, 37);
            }
        }

        public void OnPointerDown(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Right) {
                ExecuteEvents.ExecuteHierarchy<ISlotContainerQueueList>(gameObject, null, (x, y) => x.DeleteQueue(transform));
            }
        }
    }
}
