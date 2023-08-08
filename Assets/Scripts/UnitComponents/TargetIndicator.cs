using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator
{
    BaseUnit unit;
    GameObject ring, pointerAnchor, pointer;
    BaseUnit target;

    public TargetIndicator(BaseUnit _unit, GameObject _ring, GameObject _pointerAnchor, GameObject _pointer)
    {
        unit = _unit;
        ring = _ring;
        pointerAnchor = _pointerAnchor;
        pointer = _pointer;
    }

    public void Update() //this isn't a monobehaviour idiot, you gotta call it from unit (or somewhere)
    {
        //fix (ring)in place below ball:
        ring.transform.position = unit.transform.position - new Vector3(0, 0.5f, 0);
        ring.transform.rotation = Quaternion.identity;

        if (HasTarget())
        {
            PointAtTarget();
        }
        else if (pointer.activeSelf) pointer.SetActive(false);
    }

    void PointAtTarget()
    {
        //should be flat-forward towards target and
        //should be at same pos.y as parent
        if (!pointer.activeSelf) pointer.SetActive(true);

        Vector3 centre = ring.transform.position;
        Vector3 displacement = target.transform.position - centre;
        displacement.y = 0;
        //make disaplcement flat

        pointerAnchor.transform.rotation = Quaternion.LookRotation(displacement, Vector3.up);
    }
    public void SetTarget(BaseUnit _target)
    {
        target = _target;
    }

    public bool HasTarget() => target is not null && !target.health.IsDead();

    
}
