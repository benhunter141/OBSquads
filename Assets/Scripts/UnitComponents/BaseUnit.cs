using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public UnitData unitData;
    public TeamData teamData;
    public ReferenceHolder refHolder;
    public Tactic tactics;
    public Status status;

    public BaseUnit target;

    public int healthPoints;

    private void Start() //has to be after Awake so referenceHolder is set up
    {
        SetTeamColor(teamData);
        tactics = ServiceLocator.Instance.soManager.idle;
        healthPoints = unitData.healthPoints;
        refHolder.healthBar.UpdateBar(healthPoints, unitData.healthPoints);
    }

    private void Update()
    {
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
        foreach (var unit in EnemyTeam())
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
    public void AttackCurrentTarget()
    {
        if (!InRangeOfTarget()) throw new System.Exception("attacking out of range");
        if (!status.canAttack) throw new System.Exception("attacking when status says no");
        if (target is null || target.IsDead()) throw new System.Exception("target is null or dead");
        
        status = ServiceLocator.Instance.soManager.attacking;
        refHolder.animator.Play("MeleeAttack");

        StartCoroutine(DealDamageAfterDelay(target, unitData.damage, unitData.timeTilHit));
        StartCoroutine(FinishAttack(unitData.attackPeriod));
    }

    IEnumerator DealDamageAfterDelay(BaseUnit _target, int damage, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (status != ServiceLocator.Instance.soManager.attacking) yield break; //should be attacking, if not, cancel out
        if (_target is null || _target.IsDead()) yield break;
        if (IsDead()) yield break;
        if (_target != target) yield break;

        target.TakeDamage(unitData.damage);
    }

    IEnumerator FinishAttack(float period)
    {
        yield return new WaitForSeconds(period);

        if (IsDead()) yield break;
        if (status != ServiceLocator.Instance.soManager.attacking) yield break;

        status = ServiceLocator.Instance.soManager.ready;
    }
    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        refHolder.healthBar.UpdateBar(healthPoints, unitData.healthPoints);
        //show damage with floating kenney font numbers
        if (IsDead())
        {
            Die();
        }
    }

    void Die()
    {
        //killing blow red numbers
        //light anti-gravity (no grav for now)
        //if last on team to die, Victory();
        refHolder.rb.useGravity = false;
        float antigravForce = 5;
        refHolder.rb.AddForce(Vector3.up * antigravForce);
        refHolder.collider.enabled = false;
        status = ServiceLocator.Instance.soManager.dead;
        ServiceLocator.Instance.unitManager.RetargetDeadUnit(this);
        var renderers = refHolder.AllRenderers();
        float fadeTime = 2;

        StartCoroutine(FadeOutRenderers(renderers, fadeTime));
        StartCoroutine(DisableUnit(fadeTime));
        ServiceLocator.Instance.encounterManager.CheckForVictory();
    }

    IEnumerator FadeOutRenderers(List<Renderer> renderers, float fadeTime)
    {
        int frames = (int)(fadeTime * 30);
        float fraction = 1f / frames;
        for(int i = 0; i < frames; i++)
        {
            float alpha = 1f - fraction * i;
            foreach(var r in renderers)
            {
                Color color = r.material.color;
                color.a = alpha;
                r.material.color = color; //won't this same material fade on other units?
            }
            yield return Helpers.EndOfFrame;
        }
    }

    IEnumerator DisableUnit(float fadeTime)
    {
        int frames = (int)(fadeTime * 30);
        for(int i = 0; i < frames; i++)
        {
            yield return Helpers.EndOfFrame;
        }
        yield return new WaitForSeconds(fadeTime);
        gameObject.SetActive(false);
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
