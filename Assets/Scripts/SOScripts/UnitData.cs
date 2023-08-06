using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitData : ScriptableObject
{
    public int healthPoints;
    public float attackRange; //melee is 1.1 !!! what about larger units? attack range should be from this.edge to foe edge + cushion of 0.05 or sth
    public float moveForce;
    public int damage;
    public float timeTilHit;
    public float attackPeriod;
}
