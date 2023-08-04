using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    //points triangle at target, updates every update

    //if no target, pointer is shut off
    [SerializeField]
    GameObject pointerAnchor;
    BaseUnit target;

    private void Update()
    {
        transform.position = transform.parent.position - new Vector3(0, 0.5f, 0);
        transform.rotation = Quaternion.identity;

        if (HasTarget())
        {
            PointAtTarget();
        }
        else if (pointerAnchor.activeSelf) pointerAnchor.SetActive(false);
    }

    void PointAtTarget()
    {
        if (!pointerAnchor.activeSelf) pointerAnchor.SetActive(true);
        Vector3 centre = transform.position;
        Vector3 displacement = target.transform.position - centre;
        pointerAnchor.transform.rotation = Quaternion.LookRotation(displacement);
    }
    public void SetTarget(BaseUnit _target)
    {
        target = _target;
    }

    public bool HasTarget() => target is not null && !target.IsDead();

    
}
