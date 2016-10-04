using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetwork : NetworkBehaviour {

	public new bool isLocalPlayer
    {
        get { return base.isLocalPlayer; }
    }
}
