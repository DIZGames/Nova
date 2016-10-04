using Assets.Script.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Script {
    public class Terminal : MonoBehaviour, IInteractWithPlayerRaycast, IScrollViewContainer, IUI{

        [SerializeField]
        private Transform scrollFieldList;

        private GameObject uiListElement;
        private InterfaceManager interfaceManager;
        private ShipManager shipManager;

        [SerializeField]
        private GameObject uiObject;

        private Transform standardParent;

        [SerializeField]
        private Transform terminalDock;
        [SerializeField]
        private Transform terminalShield;

        //[SerializeField]
        //private Text energyValue;
        //[SerializeField]
        //private Text oxygenValue;
        //[SerializeField]
        //private Text maxEnergyValue;
        //[SerializeField]
        //private Text maxOxygenValue;

        private ITest currentITest = null;

        public bool IsActive {
            get {
                return uiObject.activeSelf;
            }
        }

        public ISlotContainerList ISlotContainerList {
            get {
                return null;
            }
        }

        void Start() {

            shipManager = transform.root.GetComponent<ShipManager>();
            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
            uiListElement = (GameObject)Resources.Load("Prefab/UIListElement");

            standardParent = transform;
        }

        void Update() {


        }


        public void FillList() {

            List<ITest> listTest = shipManager.iTestListEnergy;
            //scrollFieldList


            for (int i = 0; i < scrollFieldList.childCount; i++) {
                Destroy(scrollFieldList.GetChild(i).gameObject);
            }

            for (int i = 0; i < listTest.Count; i++) {
                GameObject element = Instantiate(uiListElement);

                element.GetComponent<ScrollViewContainer>().iTest = listTest[i];

                element.transform.SetParent(scrollFieldList);
            }

            List<ITest> listTest1 = shipManager.itTestListOxygen;

            for (int i = 0; i < listTest1.Count; i++) {
                GameObject element = Instantiate(uiListElement);

                element.GetComponent<ScrollViewContainer>().iTest = listTest1[i];

                element.transform.SetParent(scrollFieldList);
            }

            List<ITest> listTest2 = shipManager.iTestListStorage;

            for (int i = 0; i < listTest2.Count; i++) {
                GameObject element = Instantiate(uiListElement);

                element.GetComponent<ScrollViewContainer>().iTest = listTest2[i];

                element.transform.SetParent(scrollFieldList);
            }

            List<ITest> listTest3 = shipManager.iTestListConsumerPrio1;

            for (int i = 0; i < listTest3.Count; i++) {
                GameObject element = Instantiate(uiListElement);

                element.GetComponent<ScrollViewContainer>().iTest = listTest3[i];

                element.transform.SetParent(scrollFieldList);
            }


            List<ITest> listTest4 = shipManager.iTestListConsumerPrio2;

            for (int i = 0; i < listTest4.Count; i++) {
                GameObject element = Instantiate(uiListElement);

                element.GetComponent<ScrollViewContainer>().iTest = listTest4[i];

                element.transform.SetParent(scrollFieldList);
            }

            List<ITest> listTest5 = shipManager.iTestListConsumerPrio3;

            for (int i = 0; i < listTest5.Count; i++) {
                GameObject element = Instantiate(uiListElement);

                element.GetComponent<ScrollViewContainer>().iTest = listTest5[i];

                element.transform.SetParent(scrollFieldList);
            }

            List<ITest> listTest6 = shipManager.iTestListContainerEnergy;

            for (int i = 0; i < listTest6.Count; i++) {
                GameObject element = Instantiate(uiListElement);

                element.GetComponent<ScrollViewContainer>().iTest = listTest6[i];

                element.transform.SetParent(scrollFieldList);
            }

        }

        public void RaycastAction() {
            interfaceManager.ShowUI(GetComponent<IUI>(), "Terminal");
            FillList();
        }

        public void ButtonPress(ITest iTest) {

            if (iTest.iUI.IsActive) {
                currentITest.iUI.ResetPosition();
            }
            else {
                if (currentITest != null) {
                    currentITest.iUI.ResetPosition();
                }
                currentITest = iTest;

                currentITest.iUI.Move(terminalDock);
                currentITest.iUI.Show();

                if (iTest.iUI.ISlotContainerList != null) {
                    terminalShield.SetAsLastSibling();
                }

            }


            
        }

        public void Hide() {
            uiObject.SetActive(false);
        }

        public void Move(Transform transform) {
            this.transform.SetParent(transform);
            this.transform.position = transform.position;
            this.transform.rotation = transform.rotation;
        }

        public void Show() {
            uiObject.SetActive(true);
        }

        public void ResetPosition() {
            Move(standardParent);
            uiObject.SetActive(false);

            if (currentITest != null) {
                currentITest.iUI.ResetPosition();
            }
        }
    }

    public interface IScrollViewContainer : IEventSystemHandler{

        void ButtonPress(ITest iTest);

    }
}
