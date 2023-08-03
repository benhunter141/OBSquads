using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public UnitData unitData;
    public TeamData teamData;
    public ReferenceHolder refHolder;
    public Tactics tactics;
    

    private void Start() //has to be after Awake so referenceHolder is set up
    {
        SetTeamColor(teamData);
        tactics = ServiceLocator.Instance.soManager.idle;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) refHolder.animator.Play("MeleeAttack");
        tactics.Perform(this);
    }

    void SetTeamColor(TeamData teamData)
    {
        refHolder.SetColors(teamData.teamColor, teamData.accentColor);
    }
}
