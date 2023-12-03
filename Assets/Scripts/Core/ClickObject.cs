using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Colorado.Core
{
    public class ClickObject : MonoBehaviour, IPointerDownHandler
    {
        public event Action<GameObject> OnClick;

        public void OnPointerDown(PointerEventData eventData) => OnClick.Invoke(gameObject);
    }
}