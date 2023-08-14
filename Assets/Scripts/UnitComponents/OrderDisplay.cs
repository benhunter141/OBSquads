using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderDisplay
{
    BaseUnit unit;
    GameObject anchor;
    public OrderDisplay(BaseUnit u, GameObject _anchor)
    {
        unit = u;
        anchor = _anchor;
        anchor.SetActive(false);
    }

    public void Display(Order order)
    {
        //can flatten displacement::
        //for bumps later...?
        anchor.SetActive(true);
        anchor.transform.rotation = Quaternion.LookRotation(order.displacement, Vector3.up);

        Vector3 scale = anchor.transform.localScale;
        scale.z = order.displacement.magnitude;
        anchor.transform.localScale = scale;
        Debug.Log("Order display anchor is: ", anchor);
    }
}
