using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class SlotContainerCreator : MonoBehaviour{

        public void CreateSlotContainer(ItemBase itemBase, int stack) {
            GameObject goSlotContainer = (GameObject)Resources.Load("Prefab/SlotContainer");

            IUI iUI = GetComponent<IUI>();

            while (itemBase.maxStack <= stack && iUI.FreeSlot()) {

                stack -= itemBase.maxStack;

                GameObject gObject1 = Instantiate(goSlotContainer);
                gObject1.name = itemBase.name;

                SlotContainer slotContainer1 = gObject1.GetComponent<SlotContainer>();
                ItemBase itemBase1 = itemBase.Clone();
                itemBase1.stack = itemBase.maxStack;

                slotContainer1.ItemBase = itemBase1;


                iUI.Add(slotContainer1);
            }

            if (stack > 0 && iUI.FreeSlot()) {

                GameObject gObject = Instantiate(goSlotContainer);
                gObject.name = itemBase.name;

                SlotContainer slotContainer = gObject.GetComponent<SlotContainer>();
                ItemBase itemBase1 = itemBase.Clone();
                itemBase1.stack = stack;

                slotContainer.ItemBase = itemBase1;

                iUI.Add(slotContainer);

            }

           
        }

    }
}
