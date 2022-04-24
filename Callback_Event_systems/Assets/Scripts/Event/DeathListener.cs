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
            EventSystem.Current.RegisterListener<UnitDeathEventInfo> (OnUnitDied, gameObject.GetInstanceID());
        }



        void OnUnitDied(UnitDeathEventInfo unitDeathEventInfo)
        {
            StartCoroutine(EnlargeAndDestroy(unitDeathEventInfo));
        }
        
        IEnumerator EnlargeAndDestroy(UnitDeathEventInfo unitDeathEventInfo)
        {
            float timer = 0;
            while (timer < unitDeathEventInfo.KillTimer) // this could also be a condition indicating "alive or dead"
            {
                timer += Time.deltaTime;
                unitDeathEventInfo.EventUnitGo.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 1.01f;
                yield return null;
            }
            EventInfo debugEventInfo = new DebugEventInfo
            {
                EventDescription = unitDeathEventInfo.EventDescription
            };
            
            EventSystem.Current.FireEvent(debugEventInfo);
            
            Destroy(unitDeathEventInfo.EventUnitGo);
            
        }
    }

}
