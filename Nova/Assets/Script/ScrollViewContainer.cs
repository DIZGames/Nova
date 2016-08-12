using Assets.Script.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Script {
    public class ScrollViewContainer : MonoBehaviour, IPointerDownHandler{

        [SerializeField]
        public Image image;
        [SerializeField]
        public Text text;

        [SerializeField]
        public Transform transform;

        public void OnPointerDown(PointerEventData eventData) {
            ExecuteEvents.ExecuteHierarchy<ITerminalContainerList>(gameObject, null, (x, y) => x.OpenInTerminal(this));

        }
    }
}
