using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script
{
    public class SlotContainerCreator : MonoBehaviour
    {
        public List<GameObject> CreateSlotContainer(ItemBase itemBase, int stack)
        {
            List<GameObject> listGameObjects = new List<GameObject>();
            GameObject goSlotContainer = (GameObject)Resources.Load("Prefab/SlotContainer");

            while (itemBase.maxStack <= stack)
            {
                stack -= itemBase.maxStack;



                GameObject gObject1 = Instantiate(goSlotContainer);
                gObject1.name = itemBase.name;

                SlotContainer slotContainer1 = gObject1.GetComponent<SlotContainer>();
                ItemBase itemBase1 = itemBase.Clone();
                itemBase1.stack = itemBase.maxStack;

                slotContainer1.ItemBase = itemBase1;

                listGameObjects.Add(slotContainer1.gameObject);
            }

            if (stack > 0)
            {

                GameObject gObject = Instantiate(goSlotContainer);
                gObject.name = itemBase.name;

                SlotContainer slotContainer = gObject.GetComponent<SlotContainer>();
                ItemBase itemBase1 = itemBase.Clone();
                itemBase1.stack = stack;

                slotContainer.ItemBase = itemBase1;

                listGameObjects.Add(slotContainer.gameObject);
            }
            return null;

        }



    }
}
