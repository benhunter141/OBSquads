using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad
{
    public string squadName;
    public Tactic tactic;
    public List<BaseUnit> units;
    public TeamData teamData;

    public Squad(TeamData _teamData, List<BaseUnit> _units, Tactic _tactic)
    {
        teamData = _teamData;
        units = _units;
        tactic = _tactic;
    }

    public void IssueOrders()
    {
        foreach(var u in units)
        {
            u.tactics = tactic;
        }
    }

    public bool IsDead()
    {
        bool allDead = true;
        foreach(var u in units)
        {
            if (!u.IsDead()) allDead = false;
        }
        if (allDead) Debug.Log("Squad is all dead");
        else Debug.Log("Squad is not all dead");
        return allDead;
    }

    //Later:
    //Formation information (Dictionary?)
}
