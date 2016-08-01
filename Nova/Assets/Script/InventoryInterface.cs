using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public interface InventoryInterface : IEventSystemHandler {


    void UpdateList();

    int Count(string name);

    void Add(GameObject gOContainer);

    bool ReduceStackOne(string name);
}

    


