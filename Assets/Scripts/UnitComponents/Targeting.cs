using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting
{
    public BaseUnit unit { get; private set; }
    public BaseUnit target { get; private set; }

    public Targeting(BaseUnit _unit)
    {
        unit = _unit;
    }

    public bool HasTarget() => target is not null && !target.health.IsDead();
    public BaseUnit GetTarget()
    {
        float minDistance = float.MaxValue;
        foreach (var nme in EnemyTeam())
        {
            if (nme.health.IsDead()) continue;
            float distance = Vector3.Distance(unit.transform.position, nme.transform.position);
            if (distance > minDistance) continue;
            else
            {
                minDistance = distance;
                target = nme;
            }
        }
        if (target is not null)
        {
            unit.targetIndicator.SetTarget(target);
            return target;
        }
        Debug.Log($"Target not found. Enemy team count:{EnemyTeam().Count}");
        throw new System.Exception();
    }

    public Vector3 AttackLocation()
    {
        //on line between self and target
        //AR from target
        Vector3 displacement = DisplacementToTarget();
        Vector3 location = target.transform.position - displacement.normalized * unit.data.attackRange;
        return location;
    }

    public Vector3 DisplacementToTarget() => target.transform.position - unit.transform.position;
    public bool InRangeOfTarget() => Vector3.Distance(target.transform.position, unit.transform.position) < unit.data.attackRange;
    public List<BaseUnit> EnemyTeam() => ServiceLocator.Instance.unitManager.EnemyTeamOf(unit);
}
