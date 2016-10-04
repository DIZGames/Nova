using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private GameObject _player;
    private Vector3 offset;

    void Start() {
        
    }

    void LateUpdate() {
        transform.position = Player.transform.position + offset;
    }

    public GameObject Player
    {
        set {
            _player = (GameObject)value;
            offset = transform.position - Player.transform.position;
        }
        get { return _player; }
    }
}
