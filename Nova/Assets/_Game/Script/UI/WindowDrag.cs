using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Script {
    public class WindowDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

        Vector3 startPosition;
        Transform startParent;

        public void OnBeginDrag(PointerEventData eventData) {

            startPosition = transform.position;
            startParent = transform.parent;

            transform.parent.parent.SetAsLastSibling();

            //transform.SetParent(transform.root);

            //transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData) {
            //transform.parent.position = Input.mousePosition;

            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData) {
           
        }
    }
}
