using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using Assets.Script;

public class SlotContainerDrag : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler {

    private static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    public void OnBeginDrag(PointerEventData eventData) {

        itemBeingDragged = gameObject;

        startPosition = transform.position;
        startParent = transform.parent;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GetComponent<LayoutElement>().ignoreLayout = true;
 
        itemBeingDragged.transform.SetParent(transform.parent.parent.parent.parent);


        ExecuteEvents.ExecuteHierarchy<InventoryInterface>(startParent.gameObject, null, (x, y) => x.UpdateList());
    }

    public void OnDrag(PointerEventData eventData) {

        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData) {
        if (transform.parent == startParent || transform.parent == startParent.parent.parent.parent) {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        else if (transform.parent.childCount > 0) {
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        GetComponent<LayoutElement>().ignoreLayout = false;

        ExecuteEvents.ExecuteHierarchy<InventoryInterface>(gameObject, null, (x, y) => x.UpdateList());
    }
}
