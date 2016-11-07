using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;
using Assets.Script;

public class BlockSpawner : MonoBehaviour {

    [SerializeField]
    private ItemBlock itemBlock;
    [SerializeField]
    private GameObject blockBuilderPrefab;

    public void spawnBlock() {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject go = Instantiate(blockBuilderPrefab);

        IHasItem iHasItem = go.GetComponent<IHasItem>();
        iHasItem.SetItem(itemBlock);

        go.transform.position = mousePos;
    }
}
