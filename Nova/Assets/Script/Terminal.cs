using Assets.Script.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script {
    public class Terminal : MonoBehaviour, IInteractWithPlayerRaycast, ITerminalContainerList{

        private TerminalManager terminalManager;

        [SerializeField]
        private Transform scrollFieldList;

        private GameObject uiListElement;
        private InterfaceManager interfaceManager;
        private ShipManager shipManager;

        [SerializeField]
        private Text energyValue;
        [SerializeField]
        private Text oxygenValue;
        [SerializeField]
        private Text maxEnergyValue;
        [SerializeField]
        private Text maxOxygenValue;

        [SerializeField]
        private UIContainer uiContainer;

        void Start() {
            terminalManager = transform.root.GetComponent<TerminalManager>();
            shipManager = transform.root.GetComponent<ShipManager>();
            interfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();

            //shipManager.AddToPing(UpdateEnergyOxygen);

            uiListElement = (GameObject)Resources.Load("Prefab/UIListElement");
            

        }

        void FillList() {
            for (int i = 0; i < scrollFieldList.childCount; i++) {
                Destroy(scrollFieldList.GetChild(i).gameObject);
            }

            for (int i = 0; i < terminalManager.getCount(); i++) {
                GameObject gO = (GameObject)Instantiate(uiListElement);
                gO.transform.SetParent(scrollFieldList);

                gO.GetComponent<ScrollViewContainer>().image.sprite = terminalManager.getTerminalByIndex(i).icon;
                gO.GetComponent<ScrollViewContainer>().text.text = terminalManager.getTerminalByIndex(i).name;
                gO.GetComponent<ScrollViewContainer>().transform = terminalManager.getTerminalByIndex(i).transform;
            }

        }

        public void OpenInTerminal(ScrollViewContainer scrollViewContainer) {
            //uiContainer.setChild(scrollViewContainer.transform);
        }

        private void ToggleUpdateEnergyOxygen() {
            if (IsInvoking("UpdateEnergyOxygen")) {
                CancelInvoke("UpdateEnergyOxygen");
            }
            else {
                InvokeRepeating("UpdateEnergyOxygen", 0, 1);
            }
        }

        private void UpdateEnergyOxygen() {
            //energyValue.text = shipManager.Energy.ToString();
            //oxygenValue.text = shipManager.Oxygen.ToString();
            //maxEnergyValue.text = shipManager.MaxEnergy.ToString();
            //maxOxygenValue.text = shipManager.MaxOxygen.ToString();
        }

        public void RaycastAction() {
            FillList();
            //interfaceManager.setChildOnUIContainer(this.transform);
        }
    }
}
