using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    public delegate void LeftClick();
    public delegate void MiddleClick();
    public delegate void RightClick();

    public LeftClick leftClick;
    public MiddleClick middleClick;
    public RightClick rightClick;

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
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            if (middleClick != null)
            {
                Debug.Log("Middle click");
                middleClick();
            }               
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (rightClick!= null)
            {
                Debug.Log("Right click");
                rightClick();
            }
        }
    }
}
