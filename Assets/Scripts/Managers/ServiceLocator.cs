using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance { get; private set; }
    public UnitManager unitManager { get; private set; }
    public SOManager soManager { get; private set; }
    public EncounterManager encounterManager { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        unitManager = GetComponentInChildren<UnitManager>();
        soManager = GetComponentInChildren<SOManager>();
        encounterManager = GetComponentInChildren<EncounterManager>();
    }
}
