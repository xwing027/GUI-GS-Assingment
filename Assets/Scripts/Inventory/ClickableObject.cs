using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    public delegate void LeftClick();

    public LeftClick leftClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (leftClick!=null)
            {
                Debug.Log("Left click");
                leftClick();
            }        
        }
    }
}
