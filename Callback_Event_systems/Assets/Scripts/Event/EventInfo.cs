using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    // Main class, contains a description
    public abstract class EventInfo
    {
        public string EventDescription;
    }

    
    // Die event class
    public class UnitDeathEventInfo : EventInfo
    {
        public GameObject EventUnitGo;
        public float KillTimer;
    }

    public class DebugEventInfo : EventInfo
    {
        
    }
}