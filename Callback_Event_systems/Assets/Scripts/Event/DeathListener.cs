using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    public class DeathListener : MonoBehaviour
    {
        private void Start()
        {
            EventSystem.Current.RegisterListener(typeof(UnitDeathEventInfo), OnUnitDied);
        }

        // void OnUnitSpawned(Health health)
        // {
        //     Debug.Log("Spawnat");
        //     health.OnDeathListeners += OnUnitDied;
        // }

        void OnUnitDied(EventInfo eventInfo)
        {
            UnitDeathEventInfo unitDeathEventInfo = (UnitDeathEventInfo) eventInfo;
            Debug.Log("UNIT WHO DIED: " + unitDeathEventInfo.EventUnitGO.name);
        }
    }

}
