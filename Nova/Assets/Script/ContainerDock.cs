using Assets.Script.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class ContainerDock : MonoBehaviour, IExchangeSlotContainer, IToolTip {

        public List<ContainerDock> otherDocks = new List<ContainerDock>();
        private IUI iUI;

        public ToolTip toolTip;

        public void TryPlacing(SlotContainer slotContainer) {
            iUI.Add(slotContainer);
        }

        public void SetIUI(IUI iUI) {
            this.iUI = iUI;
            iUI.Move(transform);
            iUI.Show();
        }

        public void ResetUI() {
            if (iUI != null) {
                iUI.ResetPosition();
            }

            Close();
            
        }

        public void Exchange(SlotContainer slotContainer) {
            for (int i = 0; i < otherDocks.Count; i++) {
                otherDocks[i].TryPlacing(slotContainer);
            }
            Close();
        }

        public void Message(string title, string text) {
            if (toolTip != null) {
                toolTip.SetToolTip(title,text);
                toolTip.gameObject.SetActive(true);
            }
        }

        public void Close() {
            if (toolTip != null) {
                toolTip.gameObject.SetActive(false);
            }
            
        }
    }
}
