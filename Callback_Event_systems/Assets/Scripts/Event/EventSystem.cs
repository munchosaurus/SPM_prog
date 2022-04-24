using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;


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

        private Dictionary<System.Type, Dictionary<int, EventListener>> eventListeners;

        public void RegisterListener<T>(System.Action<T> listener, int instanceID) where T : EventInfo
        {
            System.Type eventType = typeof(T);
            if (eventListeners == null)
            {
                eventListeners = new Dictionary<System.Type, Dictionary<int, EventListener>>();
            }

            if (eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
            {
                eventListeners[eventType] = new Dictionary<int, EventListener>();
            }

            EventListener wrapper = (eventInfo) => { listener((T) eventInfo); };
            eventListeners[eventType].Add(instanceID, wrapper);
        }

        public void UnregisterListener<T>(System.Action<T> listener, int instanceID) where T : EventInfo
        {
            System.Type eventType = typeof(T);

            // Comparison if there isn't any key of the passed on type
            if (eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
            {
                return;
            }
            
            // Removes the listener using the instance id of the object as key
            eventListeners[eventType].Remove(instanceID);
            
            // removes the type from the dictionary if there aren't any listeners for the specific event type
            if (eventListeners[eventType].Count < 1)
            {
                eventListeners.Remove(eventType);
            }
        }

        public void FireEvent(EventInfo eventInfo)
        {
            System.Type trueEventInfoClass = eventInfo.GetType();
            if (eventListeners == null || eventListeners[trueEventInfoClass] == null)
            {
                return;
            }

            foreach (var eventListener in eventListeners[trueEventInfoClass])
            {
                eventListener.Value(eventInfo);
            }
        }
    }
}