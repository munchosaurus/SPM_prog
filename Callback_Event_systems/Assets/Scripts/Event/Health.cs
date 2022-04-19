using System;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    public class Health : MonoBehaviour
    {
        public delegate void OnDeathCallbackDelegate(EventInfo eventInfo);
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
            UnitDeathEventInfo unitDeathEventInfo = new UnitDeathEventInfo
            {
                EventUnitGO = gameObject,
                EventDescription = "Unit " + gameObject.name + " has died."
            };
            EventSystem.Current.FireEvent(unitDeathEventInfo);
            Destroy(gameObject);
        }
    }
}

