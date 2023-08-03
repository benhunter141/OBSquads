using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameObject fighterPrefab;
    public List<BaseUnit> unitList;
    public GameObject teamAStart, teamBStart;

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

        var blueUnits = new List<GameObject>();
        var redUnits = new List<GameObject>();

        blueUnits.Add(Instantiate(fighterPrefab));
        blueUnits.Add(Instantiate(fighterPrefab));
        redUnits.Add(Instantiate(fighterPrefab));

        var unitObjects = new List<GameObject>();
        unitObjects.AddRange(blueUnits);
        unitObjects.AddRange(redUnits);
        
        foreach(var uO in unitObjects)
        {
            unitList.Add(uO.GetComponent<BaseUnit>());
        }

        PlaceTeam(blueUnits, blue, teamAStart, chase);
        PlaceTeam(redUnits, red, teamBStart, idle);
    }

    void PlaceTeam(List<GameObject> units, TeamData teamData, GameObject home, Tactics tactics)
    {
        //Spacing
        float spacing = 1f;
        float firstPosX = (units.Count - 1) * spacing / 2;
        Vector3 homeLocation = home.transform.position;
        //firstPos is furthest left unit
        for(int i = 0; i < units.Count; i++)
        {
            BaseUnit unit = units[i].GetComponent<BaseUnit>();
            Vector3 position = new Vector3(homeLocation.x - firstPosX + spacing * i, 
                                            homeLocation.y, 
                                            homeLocation.z);
            unit.transform.position = position;
            unit.transform.rotation = home.transform.rotation;
            unit.teamData = teamData;
            unit.tactics = tactics;
        }
        home.SetActive(false);
    }

}
