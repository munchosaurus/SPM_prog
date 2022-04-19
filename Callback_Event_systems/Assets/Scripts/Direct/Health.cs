using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void OnDeathCallbackDelegate(UnitDeathInfo unitDeathInfo);
    static public event OnDeathCallbackDelegate OnDeathListeners;

    public void RegisterEvent(OnDeathCallbackDelegate func)
    {
        OnDeathListeners += func;
    }
        
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }

    void Die()
    {
        if (OnDeathListeners != null)
        {
            UnitDeathInfo udi = gameObject.AddComponent<UnitDeathInfo>();
            udi.deadUnitGO = gameObject;
            OnDeathListeners(udi);
        }

        Destroy(gameObject);
    }
}