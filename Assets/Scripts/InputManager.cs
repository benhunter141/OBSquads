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
        Debug.Log($"Click received at location: {click.location.x}, {click.location.y}, {click.location.z}");
        if (activeSelection is null && click.unit is not null)
        {
            activeSelection = click.unit;
            click.unit.selectionDisplay.TurnOn();
        }
        else if (activeSelection is null && click.unit is null)
        {
            Debug.Log("no active selection AND no unit stored in the click received");
        }
        else if (activeSelection is not null)
        {
            //this code should be elsewhere
            //maybe OrderManager
            
            if (click is null) Debug.Log("click is null");
            if (click.unit is not null && click.unit == activeSelection)
            {
                //clicked self, should leave order as is, leave activeSelection as is
                return;
            }
            Vector3 displacement = click.location - activeSelection.transform.position;
            Order order = new Order(activeSelection, displacement);


            //if (order is null) Debug.Log("Order is null");
            //if (ServiceLocator.Instance is null) Debug.Log("Service Locator is null");
            //if (ServiceLocator.Instance.stopGoManager is null) Debug.Log("SGManager is null");
            ServiceLocator.Instance.stopGoManager.AddToOrders(order);
            //Debug.Log("added to orders");
            ServiceLocator.Instance.stopGoManager.RemoveFromOrders(activeSelection.order);
            activeSelection.order = order;
            order.DisplayOrder();
            activeSelection.selectionDisplay.TurnOff();
            activeSelection = null;
        }
        else Debug.Log("Weird error... investigate");
    }
}


