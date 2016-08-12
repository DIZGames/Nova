using Assets.Script.Interface;
using Assets.Script.RecipeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Script {
    public class SlotContainerRecipe : MonoBehaviour, IPointerDownHandler {
        [SerializeField]
        private Recipe _Recipe;

        [SerializeField]
        private Image Image;


        public Recipe Recipe {
            get {
                return _Recipe;
            }
            set {
                _Recipe = value;
                this.Image.sprite = _Recipe.result.item.icon;
            }
        }

        void Start() {

        }

        public void Buildable(bool isBuildable) {
            if (isBuildable) {
                Image.color = new Color(255, 255, 255, 255);
            }
            else {
                Image.color = new Color(255, 0, 0, 37);
            }
        }

        public void OnPointerDown(PointerEventData eventData) {

            ExecuteEvents.ExecuteHierarchy<ISlotContainerRecipeList>(gameObject, null, (x, y) => x.CraftRecipe(this));
        }
    }
}
