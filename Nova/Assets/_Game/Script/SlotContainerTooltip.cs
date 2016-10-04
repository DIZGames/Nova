using Assets.Script.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Script {
    public class SlotContainerTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
       

        public void OnPointerEnter(PointerEventData eventData) {

            

            SlotContainer slotContainer = GetComponent<SlotContainer>();
            //Debug.Log("Enter");
            ExecuteEvents.ExecuteHierarchy<IToolTip>(gameObject, null, (x, y) => x.Message(slotContainer.ItemBase.itemName, slotContainer.ItemBase.description));
        }

        public void OnPointerExit(PointerEventData eventData) {
            //Debug.Log("Exit");

            ExecuteEvents.ExecuteHierarchy<IToolTip>(gameObject, null, (x, y) => x.Close());
        }

    }
}
