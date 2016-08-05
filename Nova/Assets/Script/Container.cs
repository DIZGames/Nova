using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Container : MonoBehaviour, InventoryInterface
{
    List<SlotContainer> slotContainerList = new List<SlotContainer>();

    public void Add(GameObject gOContainer)
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            if (transform.GetChild(0).GetChild(i).childCount == 0)
            {
                gOContainer.transform.SetParent(transform.GetChild(0).GetChild(i));
                gOContainer.transform.position = transform.GetChild(0).GetChild(i).position;

                break;
            }
        }
    }

    public int Count(string name)
    {
        int count = 0;

        for (int i = 0; i < slotContainerList.Count; i++)
        {
            if (name == slotContainerList[i].Item.Name)
            {
                count += slotContainerList[i].Item.stack;
            }
        }
        return count;
    }

    public bool ReduceStackOne(string name)
    {
        for (int i = 0; i < slotContainerList.Count; i++)
        {
            if (name == slotContainerList[i].Item.Name && slotContainerList[i].Item.stack != 0)
            {
                slotContainerList[i].Item.stack--;
                return true;
            }
        }
        return false;
    }

    public void UpdateList()
    {
        slotContainerList.Clear();

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            if (transform.GetChild(0).GetChild(i).childCount != 0)
            {
                slotContainerList.Add(transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<SlotContainer>());
            }
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
