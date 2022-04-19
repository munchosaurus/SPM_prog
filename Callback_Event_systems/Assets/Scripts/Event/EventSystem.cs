using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace Event
{
    public class EventSystem : MonoBehaviour
    {
        public delegate void EventListener(EventInfo ei);

        static private EventSystem __current;

        static public EventSystem Current
        {
            get
            {
                if (__current == null)
                {
                    __current = GameObject.FindObjectOfType<EventSystem>();
                }

                return __current;
            }
        }

        private Dictionary<System.Type, List<EventListener>> eventListeners;

        public void RegisterListener(System.Type eventType, EventListener listener)
        {
            if (eventListeners == null)
            {
                eventListeners = new Dictionary<System.Type, List<EventListener>>();
            }

            if (eventListeners[eventType] == null || eventListeners.ContainsKey(eventType) == false)
            {
                eventListeners[eventType] = new List<EventListener>();
            }

            eventListeners[eventType].Add(listener);
        }

        public void UnregisterListener(System.Type eventType, EventListener listener)
        {
            // to do
        }

        public void FireEvent(EventInfo eventInfo)
        {
            System.Type trueEventInfoClass = eventInfo.GetType();
            if (eventListeners == null || eventListeners[trueEventInfoClass] == null)
            {
                return;
            }

            foreach (EventListener el in eventListeners[trueEventInfoClass])
            {
                el(eventInfo);
            }
        }
    }
}