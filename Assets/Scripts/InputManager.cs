using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //receive clicks
    //from unit colliders (provide unit and v3 click position)
    //from terrain collider (provide v3 click position)
    //store active selection
    //OrderDisplay shows active selection
    //2nd click sets order displacement
    //OrderDisplay shows order
    //escape cancels active selection
    BaseUnit activeSelection;
    public void ReceiveClick(Click click)
    {
        if(activeSelection is null && click.unit is not null)
        {
            activeSelection = click.unit;
            click.unit.selectionDisplay.TurnOn();
        }
        else if(activeSelection is null && click.unit is null)
        {
            Debug.Log("no active selection AND no unit stored in the click received");
        }
        else
        {
            if (click is null) Debug.Log("click is null");
            if (activeSelection is null) Debug.Log("active selection is null");
            Vector3 displacement = click.location - activeSelection.transform.position;
            Order order = new Order(activeSelection, displacement);
            activeSelection.order = order;
            order.DisplayOrder();
            activeSelection.selectionDisplay.TurnOff();
            activeSelection = null;
        }
    }
}


