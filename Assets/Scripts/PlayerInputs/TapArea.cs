using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LNE.Inputs
{
  public class TapArea : MonoBehaviour, IPointerDownHandler
  {
    public event Action PointerDownEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
      PointerDownEvent?.Invoke();
    }
  }
}
