using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class TerminalManager : MonoBehaviour{
        private List<TerminalContainer> scrollViewContainerList;

        void Awake() {
            scrollViewContainerList = new List<TerminalContainer>(); 
        }

        public void Add(string name, Sprite icon, Transform transformContainer) {
            TerminalContainer terminalContainer = new TerminalContainer(name,icon,transformContainer);
            scrollViewContainerList.Add(terminalContainer);
        }

        public TerminalContainer getTerminalByIndex(int index) {
            return scrollViewContainerList[index];
        }

        public int getCount() {
            return scrollViewContainerList.Count;
        }

    }
}
