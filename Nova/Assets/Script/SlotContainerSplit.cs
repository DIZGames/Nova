using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Script;

public class SlotContainerSplit : MonoBehaviour, IPointerDownHandler {

    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {

            SlotContainer SlotContainer = transform.GetComponent<SlotContainer>();

            if (SlotContainer.Stack > 1) {
                int modulo = 0;

                if (SlotContainer.Stack % 2 == 1) {
                    modulo = 1;
                }

                int result = SlotContainer.Stack / 2;
                SlotContainer.Stack = result + modulo;

                GameObject gObject = Instantiate(gameObject);
                gObject.GetComponent<SlotContainer>().prefab = SlotContainer.prefab;
                gObject.GetComponent<SlotContainer>().Stack = result;


                ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject,null,(x,y) => x.AddSlotContainerToList(gObject));

            }
        }

    }




}
