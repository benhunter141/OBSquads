using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    BaseUnit unit;
    public Movement(BaseUnit _unit)
    {
        unit = _unit;
    }

    public void MoveTowards(Vector3 position)
    {
        Vector3 displacement = position - unit.transform.position;
        displacement.Normalize();
        unit.refHolder.rb.AddForce(unit.data.moveForce * displacement);
    }
}
