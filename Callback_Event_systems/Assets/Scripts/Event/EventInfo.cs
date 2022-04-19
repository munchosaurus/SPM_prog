using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    public abstract class EventInfo
    {
        public string EventDescription;
    }

    public class UnitDeathEventInfo : EventInfo
    {
        public GameObject EventUnitGO;
    }

    public class DebugEventInfo
    {
        public int VerbosityLevel;
    }
}