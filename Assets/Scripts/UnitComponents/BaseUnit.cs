using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public UnitData unitData;
    public TeamData teamData;
    public ReferenceHolder refHolder;
    public Tactics tactics;

    BaseUnit target;

    int healthPoints;

    private void Start() //has to be after Awake so referenceHolder is set up
    {
        SetTeamColor(teamData);
        tactics = ServiceLocator.Instance.soManager.idle;
        healthPoints = unitData.healthPoints;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) refHolder.animator.Play("MeleeAttack");

        tactics.Perform(this);
    }

    void SetTeamColor(TeamData teamData)
    {
        refHolder.SetColors(teamData.teamColor, teamData.accentColor);
    }

    public bool HasTarget() => target is not null && !target.IsDead();
    public BaseUnit GetTarget()
    {
        //closest enemy that isn't dead
        float minDistance = float.MaxValue;
        foreach(var unit in EnemyTeam())
        {
            if (unit.IsDead()) continue;
            float distance = Vector3.Distance(transform.position, unit.transform.position);
            if (distance > minDistance) continue;
            else
            {
                minDistance = distance;
                target = unit;
            }
        }
        if (target is not null)
        {
            refHolder.targetIndicator.SetTarget(target);
            return target;
        }
        Debug.Log($"Target not found. Enemy team count:{EnemyTeam().Count}");
        throw new System.Exception();
    }

    List<BaseUnit> EnemyTeam() => ServiceLocator.Instance.unitManager.EnemyTeam(this);
    public bool IsDead() => healthPoints <= 0;

    public void MoveTowards(Vector3 position)
    {
        Vector3 displacement = position - transform.position;
        displacement.Normalize();
        refHolder.rb.AddForce(unitData.moveForce * displacement);
    }

    public Vector3 AttackLocation()
    {
        //on line between self and target
        //AR from target
        Vector3 displacement = DisplacementToTarget();
        Vector3 location = target.transform.position - displacement.normalized * unitData.attackRange;
        return location;
    }

    public Vector3 DisplacementToTarget() => target.transform.position - transform.position;
    public bool InRangeOfTarget() => Vector3.Distance(target.transform.position, transform.position) < unitData.attackRange;
}
