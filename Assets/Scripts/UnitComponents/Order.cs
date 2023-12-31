using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public BaseUnit unit;
    public Vector3 displacement;
    public Order(BaseUnit u, Vector3 d)
    {
        unit = u;
        displacement = d;
        //flatten displacement
        displacement.y = 0;
    }

    public void DisplayOrder()
    {
        unit.orderDisplay.Display(this);
    }

    public void ExecuteOrder()
    {
        unit.refHolder.rb.AddForce(displacement * unit.data.moveForce, ForceMode.Impulse);
        unit.orderDisplay.TurnOff();
        unit.order = null;
    }
}
