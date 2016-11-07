using UnityEngine;
using System.Collections;
using Assets.Script.ItemSystem;
using Assets.Script;

public class BlockSpawner : MonoBehaviour {

    [SerializeField]
    private ItemBlock itemBlock;
    [SerializeField]
    private GameObject gBlockSetter;


    public void spawnBlock() {
        GameObject go = Instantiate(gBlockSetter);

        BlockSetter blockSetter = go.GetComponent<BlockSetter>();
        blockSetter.SetItem(itemBlock);
    }
}
