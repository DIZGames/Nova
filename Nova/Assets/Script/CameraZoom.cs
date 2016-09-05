using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class CameraZoom : MonoBehaviour{

        void Update() {
            if (Input.GetAxis("Mouse ScrollWheel") < 0) {

                    Camera.main.orthographicSize += 0.5f;

            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0) {

                if (Camera.main.orthographicSize > 0f) {
                    Camera.main.orthographicSize -= 0.5f;
                }

                   
            }
        }

    }
}
