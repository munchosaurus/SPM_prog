using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    public class Health : MonoBehaviour
    {
        public float waitTime;
        
        public void Die()
        {
            EventInfo unitDeathEventInfo = new UnitDeathEventInfo
            {
                EventUnitGo = gameObject,
                EventDescription = "Unit " + gameObject.name + " has died.",
                KillTimer = waitTime
            };
            
            
            EventSystem.Current.FireEvent(unitDeathEventInfo);
        }
    }
}