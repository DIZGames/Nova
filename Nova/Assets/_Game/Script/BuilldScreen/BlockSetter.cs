using UnityEngine;
using System.Collections;
using Assets.Script;
using System;
using Assets.Script.ItemSystem;

public class BlockSetter : MonoBehaviour {

    [SerializeField]
    private BlockDummy blockDummy;

    private ItemBlock itemBlock;

    public void SetItem(ItemBlock itemBlock)
    {
        this.itemBlock = itemBlock;
        blockDummy.setItem(itemBlock);
    }

    void Start () {
	
	}
	
	void Update () {
       
        Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        vector3.z = 0;

        transform.position = vector3;
        //blockDummy.transform.position = vector3;
    }
}
