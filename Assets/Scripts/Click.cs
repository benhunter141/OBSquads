using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click
{
    public Vector3 location;
    public BaseUnit unit;

    public Click(Vector3 _location)
    {
        location = _location;
    }

    public Click(BaseUnit _unit, Vector3 _location)
    {
        unit = _unit;
        location = _location;
    }
}
