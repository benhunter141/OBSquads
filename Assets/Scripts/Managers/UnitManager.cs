using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameObject fighterPrefab;
    public List<BaseUnit> unitList;
    public GameObject teamAStart, teamBStart;
    public List<BaseUnit> blueUnits, redUnits;

    private void Start()
    {
        PlaceUnits();
    }

    private void PlaceUnits()
    {
        TeamData blue = ServiceLocator.Instance.soManager.blueTeam;
        TeamData red = ServiceLocator.Instance.soManager.redTeam;
        Tactics chase = ServiceLocator.Instance.soManager.chase;
        Tactics idle = ServiceLocator.Instance.soManager.idle;

        blueUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());
        blueUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());
        redUnits.Add(Instantiate(fighterPrefab).GetComponent<BaseUnit>());

        PlaceTeam(blueUnits, blue, teamAStart, chase);
        PlaceTeam(redUnits, red, teamBStart, idle);
    }

    void PlaceTeam(List<BaseUnit> units, TeamData teamData, GameObject home, Tactics tactics)
    {
        //Spacing
        float spacing = 1f;
        float firstPosX = (units.Count - 1) * spacing / 2;
        Vector3 homeLocation = home.transform.position;
        //firstPos is furthest left unit
        for(int i = 0; i < units.Count; i++)
        {
            BaseUnit unit = units[i];
            Vector3 position = new Vector3(homeLocation.x - firstPosX + spacing * i, 
                                            homeLocation.y, 
                                            homeLocation.z);
            InitializeUnit(unit, position, home.transform.rotation, teamData, tactics);
        }
        home.SetActive(false);
    }

    void InitializeUnit(BaseUnit unit, Vector3 position, Quaternion rotation, TeamData teamData, Tactics tactics)
    {
        unit.transform.position = position;
        unit.transform.rotation = rotation;
        unit.teamData = teamData;
        unit.tactics = tactics;
    }

    public List<BaseUnit> EnemyTeam(BaseUnit unit)
    {
        if (blueUnits.Contains(unit)) return redUnits;
        if (redUnits.Contains(unit)) return blueUnits;
        throw new ArgumentException();
    }
    
}
