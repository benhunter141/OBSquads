using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tactics/Chase")]
public class Chase : Tactics
{
    public override void Perform(BaseUnit unit)
    {
        //Targeting
        if (!unit.HasTarget()) unit.GetTarget();
        //Movement
        if(!unit.InRangeOfTarget()) unit.MoveTowards(unit.AttackLocation());


        //Attack

    }
}
