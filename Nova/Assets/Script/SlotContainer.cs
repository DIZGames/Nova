using UnityEngine;
using System.Collections;

public class SlotContainer : MonoBehaviour {

    Item item;
    ItemValues itemValues;

    public Item getItem()
    {
        if (item != null)
        {
            refreshTile();
        }
        return item;
    }

    public void setItem(Item item)
    {
        this.item = item;
        refreshTile();
    }

    public void refreshTile()
    {
        //transform.GetChild(0).GetComponent<Image>().sprite = item.Icon;
        //transform.GetChild(1).GetComponent<Text>().text = itemValues
    }

    public void destroyObject()
    {
        Destroy(gameObject);
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
