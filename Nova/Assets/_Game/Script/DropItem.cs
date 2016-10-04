using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Script
{
    public class DropItem : MonoBehaviour, IDropHandler
    {

        [SerializeField]
        private ItemSpawner itemSpawner;

        public void OnDrop(PointerEventData eventData)
        {

            ItemBase itemBase = eventData.pointerDrag.GetComponent<SlotContainer>().ItemBase;

            Transform t = eventData.pointerDrag.transform;
            Destroy(t.gameObject);

            itemSpawner.SpawnItem(itemBase);
        }

    }
}
