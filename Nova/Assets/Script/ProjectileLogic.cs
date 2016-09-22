using Assets.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class ProjectileLogic : MonoBehaviour
    {
        [SerializeField]
        private float areaOfDamage;
        [SerializeField]
        private int lifetime;

        void Start() {
            Destroy(gameObject, lifetime);
        }

        void OnCollisionEnter(Collision collision)
        {

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, areaOfDamage);
            int i = 0;
            while (i < hitColliders.Length)
            {
     
                IInteractWithToolProjectile interactWithToolPorjectile = hitColliders[i].gameObject.GetComponent<IInteractWithToolProjectile>();
                if (interactWithToolPorjectile != null)
                    interactWithToolPorjectile.ToolProjectileAction();

                Debug.Log(hitColliders[i].transform.name);
                i++;
            }

            Destroy(gameObject);
        }
    }
}
