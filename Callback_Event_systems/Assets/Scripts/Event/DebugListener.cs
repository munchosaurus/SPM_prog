using UnityEngine;

namespace Event
{
    public class DebugListener : MonoBehaviour
    {
        private void Start()
        {
            EventSystem.Current.RegisterListener<DebugEventInfo>(OnUnitDied);
            
        }
        
        

        void OnUnitDied(DebugEventInfo debugEventInfo)
        {
            
            Debug.Log(debugEventInfo.EventDescription);
        }


    }
}