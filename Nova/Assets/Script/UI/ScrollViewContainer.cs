using Assets.Script.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Script {
    public class ScrollViewContainer : MonoBehaviour, IPointerDownHandler {

        private ITest _iTest;
        public ITest iTest {
            get {
                return _iTest;
            }

            set {
                _iTest = value;
                text.text = _iTest.gameObject1.transform.name;
            }
        }

        [SerializeField]
        private Image buttonImage;
      
        [SerializeField]
        private Text text;

        public void OnPointerDown(PointerEventData eventData) {
            ExecuteEvents.ExecuteHierarchy<IScrollViewContainer>(gameObject, null, (x, y) => x.ButtonPress(_iTest));

        }

        void Update() {
            if (_iTest.Power) {
                buttonImage.color = Color.green;
            }
            else {
                buttonImage.color = Color.red;
            }
        }
    }
}
