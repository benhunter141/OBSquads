using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    //SO data
    public UnitData data;
    public TeamData teamData;
    public ReferenceHolder refHolder;
    public Tactic tactics;
    public Status status;

    //functions
    public Movement movement;
    public Targeting targeting;
    public Attacking attacking;
    public Health health;
    public Order order;
    public LowVelocityStopper lowVelocityStopper;

    //displays
    public StatusBar healthBar;
    public TargetIndicator targetIndicator;
    public OrderDisplay orderDisplay;
    public SelectionDisplay selectionDisplay;

    private void Start() //has to be after Awake so referenceHolder is set up
    {
        //Dependency Injection / SOLID:
        movement = new Movement(this);
        targeting = new Targeting(this);
        attacking = new Attacking(this);
        health = new Health(this);
        healthBar = new StatusBar(refHolder.healthBarObject);
        targetIndicator = new TargetIndicator(this, refHolder.everythingAnchor, refHolder.pointerAnchor, refHolder.pointer);
        orderDisplay = new OrderDisplay(this, refHolder.orderDisplayAnchor);
        selectionDisplay = new SelectionDisplay(this, refHolder.selectionDisplayRing);
        lowVelocityStopper = new LowVelocityStopper(this, refHolder.rb, data.lowVelocityThreshold, data.lowVelocityStopTime);

        SetTeamColor(teamData);
    }

     void Update()
    {
        tactics.Perform(this);
        targetIndicator.Update(); //necessary to keep ring in place, move target arrow indicator
        lowVelocityStopper.StopIfLowVelocity();
    }

    public void AttemptRetarget()
    {
        if (!targeting.HasTarget())
        {
            targeting.GetTarget();
        }
    }

    public void AttemptMove() //code below attempts to move to attack location (bad). We want to move depending on tactics
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
