using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Script
{
    public class DeleteItem : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {

            Transform t = eventData.pointerDrag.transform;

            Destroy(t.gameObject);
        }
    }
}
