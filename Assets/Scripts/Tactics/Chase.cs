using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tactics/Chase")]
public class Chase : Tactic
{
    public override void Perform(BaseUnit unit)
    {
        unit.AttemptRetarget();
        unit.AttemptMove();
        unit.AttemptAttack();
    }
}
