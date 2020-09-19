using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool Hovered { get; set; }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Hovered = true;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Hovered = false;
    }
}
