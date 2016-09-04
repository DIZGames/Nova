using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.Ship {
    public class ShipController : MonoBehaviour{

        private Rigidbody2D rb2d;

        void Start() {
            eventThrusterUp = new UnityEvent();
            eventThrusterDown = new UnityEvent();
            eventThrusterRight = new UnityEvent();
            eventThrusterLeft = new UnityEvent();

            rb2d = GetComponent<Rigidbody2D>();
        }



        public void AddThrusterAction(UnityAction actionThruster, int degree) {
            if (degree == 0)
                eventThrusterUp.AddListener(actionThruster);
            if (degree == 90)
                eventThrusterLeft.AddListener(actionThruster);
            if (degree == 180)
                eventThrusterDown.AddListener(actionThruster);
            if (degree == 270)
                eventThrusterRight.AddListener(actionThruster);
        }

        private UnityEvent eventThrusterUp;
        private UnityEvent eventThrusterDown;
        private UnityEvent eventThrusterRight;
        private UnityEvent eventThrusterLeft;

        public void Up() {
            eventThrusterUp.Invoke();
        }

        public void Down() {
            eventThrusterDown.Invoke();
        }

        public void Left() {
            eventThrusterLeft.Invoke();
        }

        public void Right() {
            eventThrusterRight.Invoke();
        }


    }
}
