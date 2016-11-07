using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;

public class BlockDummy : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    public void setItem(ItemBlock itemBlock) {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemBlock.prefab.GetComponent<SpriteRenderer>().sprite;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
