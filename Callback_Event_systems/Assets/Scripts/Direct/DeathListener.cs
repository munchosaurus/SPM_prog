using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DeathListener : MonoBehaviour
{
    private void Start()
    {
        Health.OnDeathListeners += OnUnitDied;
    }

    // void OnUnitSpawned(Health health)
    // {
    //     Debug.Log("Spawnat");
    //     health.OnDeathListeners += OnUnitDied;
    // }

    void OnUnitDied(UnitDeathInfo unitDeathInfo)
    {
        Debug.Log("UNIT WHO DIED: " + unitDeathInfo.deadUnitGO.name);
    }
}
