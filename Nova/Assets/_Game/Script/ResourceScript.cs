using Assets.Script.ItemSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script
{
    public class ResourceScript : MonoBehaviour, IInteractWithPlayerRaycast
    {

        [SerializeField]
        private ItemBlock itemBlock;
        [SerializeField]
        private int Count;

        void Start() {

        }

        void Update() {

        }

        public void RaycastAction()
        {


            throw new NotImplementedException();
        }
    }
}
