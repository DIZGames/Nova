using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Ship
{
    [Serializable]
    public class AttachableRay
    {
        public Vector3 origin;
        public Vector3 direction;
        public float distance;
        public bool isMandatory;
    }
}
