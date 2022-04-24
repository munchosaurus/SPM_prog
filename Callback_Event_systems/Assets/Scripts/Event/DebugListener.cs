using UnityEngine;

namespace Event
{
    public class DebugListener : MonoBehaviour
    {
        private void Start()
        {
            EventSystem.Current.RegisterListener<DebugEventInfo>(OnUnitDied, gameObject.GetInstanceID());
            
        }
        
        

        void OnUnitDied(DebugEventInfo debugEventInfo)
        {
            
            Debug.Log(debugEventInfo.EventDescription);
        }


    }
}