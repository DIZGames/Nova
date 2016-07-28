using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Script;

public class SlotContainerSplit : MonoBehaviour, IPointerDownHandler {

    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {

            SlotContainer SlotContainer = transform.GetComponent<SlotContainer>();

            if (SlotContainer.Item.stack > 1) {
                int modulo = 0;

                if (SlotContainer.Item.stack % 2 == 1) {
                    modulo = 1;
                }

                int result = SlotContainer.Item.stack / 2;
                SlotContainer.Item.stack = result + modulo;

                GameObject gObject = Instantiate(gameObject);
                gObject.GetComponent<SlotContainer>().Item = SlotContainer.Item;
                gObject.GetComponent<SlotContainer>().Item.stack = result;


                ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject,null,(x,y) => x.AddSlotContainerToList(gObject));

            }
        }

    }




}
