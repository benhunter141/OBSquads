using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tactics/Chase")]
public class Chase : Tactic
{
    public override void Perform(BaseUnit unit)
    {
        //Targeting
        if (!unit.HasTarget()) unit.GetTarget();
        //Movement
        if (unit is null) Debug.Log("unit is null");
        if(unit.status.canMove && !unit.InRangeOfTarget()) unit.MoveTowards(unit.AttackLocation());
        //Attack
        else if(unit.status.canAttack)
        {
            unit.AttackCurrentTarget();
        }
    }
}
