using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace Event
{
    public class EventSystem : MonoBehaviour
    {
        void OnEnable()
        {
            _current = this;
        }

        public static EventSystem Current
        {
            get
            {
                if (_current == null)
                {
                    _current = GameObject.FindObjectOfType<EventSystem>();
                }

                return _current;
            }
        }
        private static EventSystem _current;
        delegate void EventListener(EventInfo eventInfo);
        private Dictionary<System.Type, List<EventListener>> eventListeners;

        public void RegisterListener<T>(System.Action<T> listener) where T : EventInfo
        {
            System.Type eventType = typeof(T);
            if (eventListeners == null)
            {
                eventListeners = new Dictionary<System.Type, List<EventListener>>();
            }

            if ( eventListeners.ContainsKey(eventType) == false ||  eventListeners[eventType] == null)
            {
                eventListeners[eventType] = new List<EventListener>();
            }

            EventListener wrapper = (eventInfo) => { listener((T) eventInfo); };
            
            eventListeners[eventType].Add(wrapper);
        }

        public void UnregisterListener<T>(System.Action<T> listener) where T : EventInfo
        {
            System.Type eventType = typeof(T);

            if ( eventListeners.ContainsKey(eventType) == false ||  eventListeners[eventType] == null)
            {
                return;
            }

            EventListener wrapper = (eventInfo) => { listener((T) eventInfo); };
            
            eventListeners[eventType].Remove(wrapper);
            
        }

        public void FireEvent(EventInfo eventInfo)
        {
            System.Type trueEventInfoClass = eventInfo.GetType();
            if (eventListeners == null || eventListeners[trueEventInfoClass] == null)
            {
                return;
            }

            foreach (EventListener eventListener in eventListeners[trueEventInfoClass])
            {
                eventListener(eventInfo);
            }
        }
    }
}