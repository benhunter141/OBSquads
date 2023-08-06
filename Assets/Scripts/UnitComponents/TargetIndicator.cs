using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    //points triangle at target, updates every update

    //if no target, pointer is shut off
    [SerializeField]
    GameObject pointerAnchor, pointer;
    BaseUnit target;

    private void Update()
    {
        //fix in place below ball:
        transform.position = transform.parent.position - new Vector3(0, 0.5f, 0);
        transform.rotation = Quaternion.identity;

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

        Vector3 centre = transform.position;
        Vector3 displacement = target.transform.position - centre;
        displacement.y = 0;
        //make disaplcement flat

        pointerAnchor.transform.rotation = Quaternion.LookRotation(displacement, Vector3.up);
    }
    public void SetTarget(BaseUnit _target)
    {
        target = _target;
    }

    public bool HasTarget() => target is not null && !target.IsDead();

    
}
