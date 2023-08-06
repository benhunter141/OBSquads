using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameObject fighterPrefab;
    public GameObject teamAStart, teamBStart;
    public Squad firstSquad, secondSquad;

    private void Start()
    {
        PlaceUnits();
    }

    private void PlaceUnits()
    {
        TeamData blue = ServiceLocator.Instance.soManager.blueTeam;
        TeamData red = ServiceLocator.Instance.soManager.redTeam;
        Tactic chase = ServiceLocator.Instance.soManager.chase;

        var blueUnits = new List<BaseUnit>();
        blueUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());
        blueUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());
        blueUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());
        blueUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());
        blueUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());
        blueUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());
        firstSquad = new Squad(blue, blueUnits, chase);

        var redUnits = new List<BaseUnit>();
        redUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());
        redUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());
        redUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());
        redUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());
        redUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());
        redUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());
        secondSquad = new Squad(red, redUnits, chase);

        PlaceTeam(firstSquad, teamAStart);
        PlaceTeam(secondSquad, teamBStart);
    }

    void PlaceTeam(Squad squad, GameObject home)
    { 
       //Spacing
        float spacing = 1f;
        float firstPosX = (squad.units.Count - 1) * spacing / 2;
        Vector3 homeLocation = home.transform.position;
        //firstPos is furthest left unit
        for(int i = 0; i < squad.units.Count; i++)
        {
            BaseUnit unit = squad.units[i];
            Vector3 position = new Vector3(homeLocation.x - firstPosX + spacing * i, 
                                            homeLocation.y, 
                                            homeLocation.z);
            InitializeUnit(unit, position, home.transform.rotation, squad.teamData, ServiceLocator.Instance.soManager.idle);
        }
        home.SetActive(false);
    }

    void InitializeUnit(BaseUnit unit, Vector3 position, Quaternion rotation, TeamData teamData, Tactic tactics)
    {
        unit.transform.position = position;
        unit.transform.rotation = rotation;
        unit.teamData = teamData;
        unit.tactics = tactics;
    }

    public List<BaseUnit> EnemyTeam(BaseUnit unit)
    {
        if (firstSquad.units.Contains(unit)) return secondSquad.units;
        if (secondSquad.units.Contains(unit)) return firstSquad.units;
        throw new ArgumentException();
    }

    public void RetargetDeadUnit(BaseUnit unit)
    {
        var otherTeam = EnemyTeam(unit);
        foreach(var u in otherTeam)
        {
            if (u.target == unit) u.GetTarget();
        }
    }
    
    public bool IsTeamDead(List<BaseUnit> team)
    {
        bool allDead = true;
        foreach(var u in team)
        {
            if (!u.IsDead()) allDead = false;
        }
        return allDead;
    }
}
