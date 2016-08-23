using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Script {
    public class SlotLefftClick : MonoBehaviour, IPointerDownHandler {

        private bool modifierHeldDown = false;

        public void OnPointerDown(PointerEventData eventData) {

            if (modifierHeldDown) {
                if (eventData.button == PointerEventData.InputButton.Left) {
                  

                    ExecuteEvents.ExecuteHierarchy<IExchangeSlotContainer>(gameObject, null, (x, y) => x.Exchange(GetComponent<SlotContainer>()));
                }
            }

            
        }

        void Update() {

            if (Input.GetButton("Shift")) {
          
                modifierHeldDown = true;
            }
            else {
                modifierHeldDown = false;
            }



        }

    }
}
