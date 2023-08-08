using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public UnitData data;
    public TeamData teamData;
    public ReferenceHolder refHolder;
    public Tactic tactics;
    public Status status;

    public Movement movement;
    public Targeting targeting;
    public Attacking attacking;
    public Health health;
    public StatusBar healthBar;
    public TargetIndicator targetIndicator;

    private void Start() //has to be after Awake so referenceHolder is set up
    {
        //Dependency Injection / SOLID:
        movement = new Movement(this);
        targeting = new Targeting(this);
        attacking = new Attacking(this);
        health = new Health(this);
        healthBar = new StatusBar(refHolder.healthBarObject);
        targetIndicator = new TargetIndicator(this, refHolder.ring, refHolder.pointerAnchor, refHolder.pointer);

        SetTeamColor(teamData);
        tactics = ServiceLocator.Instance.soManager.idle;
    }

     void Update()
    {
        tactics.Perform(this);
        targetIndicator.Update();
    }

    public void AttemptRetarget()
    {
        if (!targeting.HasTarget())
        {
            targeting.GetTarget();
        }
    }

    public void AttemptMove()
    {
        if (status.canMove &&
            !targeting.InRangeOfTarget())
        {
            movement.MoveTowards(targeting.AttackLocation());
        }
    }

    public void AttemptAttack()
    {
        if (status.canAttack && targeting.InRangeOfTarget())
        {
            attacking.AttackCurrentTarget();
        }
    }

    void SetTeamColor(TeamData teamData)
    {
        refHolder.SetColors(teamData.teamColor, teamData.accentColor);
    }
}
