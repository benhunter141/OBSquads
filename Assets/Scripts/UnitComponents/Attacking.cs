using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking
{
    public BaseUnit unit { get; private set; }
    public Attacking(BaseUnit _unit)
    {
        unit = _unit;
    }

    public void AttackCurrentTarget()
    {
        if (!unit.targeting.InRangeOfTarget()) throw new System.Exception("attacking out of range");
        if (!unit.status.canAttack) throw new System.Exception("attacking when status says no");
        if (unit.targeting.target is null || unit.targeting.target.health.IsDead()) throw new System.Exception("target is null or dead");

        unit.status = ServiceLocator.Instance.soManager.attacking;
        unit.refHolder.animator.Play("MeleeAttack");

        unit.StartCoroutine(DealDamageAfterDelay(unit.targeting.target, unit.data.damage, unit.data.timeTilHit));
        unit.StartCoroutine(FinishAttack(unit.data.attackPeriod));
    }

    IEnumerator DealDamageAfterDelay(BaseUnit _target, int damage, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (unit.status != ServiceLocator.Instance.soManager.attacking) yield break; //should be attacking, if not, cancel out
        if (_target is null || _target.health.IsDead()) yield break;
        if (unit.health.IsDead()) yield break;
        if (_target != unit.targeting.target) yield break;

        unit.targeting.target.health.TakeDamage(unit.data.damage);
    }

    IEnumerator FinishAttack(float period)
    {
        yield return new WaitForSeconds(period);

        if (unit.health.IsDead()) yield break;
        if (unit.status != ServiceLocator.Instance.soManager.attacking) yield break;

        unit.status = ServiceLocator.Instance.soManager.ready;
    }
}
