using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ClickDetector : MonoBehaviour , IPointerDownHandler
{
    bool isUnit;
    BaseUnit unit;

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(eventData.position);
        Click click;
        if(isUnit)
        {
            click = new Click(unit, p);
        }
        else //terrain
        {
            click = new Click(p);
        }
        ServiceLocator.Instance.inputManager.ReceiveClick(click);
    }

    private void Start()
    {
        if (TryGetComponent<BaseUnit>(out BaseUnit _unit))
        {
            isUnit = true;
            unit = _unit;
        }
        else
        {
            isUnit = false;
        }

    }


}
