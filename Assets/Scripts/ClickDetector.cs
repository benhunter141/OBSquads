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
        Camera cam = ServiceLocator.Instance.cameraController.cam;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(eventData.position);

        Click click;
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            Vector3 position = hit.point;
            if(isUnit)
            {
                click = new Click(unit, position);
            }
            else
            {
                click = new Click(position);
            }
            
        }
        else throw new System.Exception();
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
