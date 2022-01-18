using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFramework
{
    public class SimpleEventHandler : MonoBehaviour
    {
        // Public //
        // Protected //
        [SerializeField]
        protected List<string> m_events;

        protected Dictionary<string, Delegates> m_eventsDictionary;
        // Private //
        // Access //
        public static SimpleEventHandler Instance { get; private set; }

        public delegate void EventDelegate();

        public class Delegates
        {
            public string m_eventName;
            public EventDelegate Delegate;
            public float m_lastCalledAt = -1f;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError(name + "duplicate instance.");
            }
            Instance = this;

            m_eventsDictionary = new Dictionary<string, Delegates>();
            // Intialize //
            for (int i = 0; i < m_events.Count; i++)
            {
                if (m_eventsDictionary.ContainsKey(m_events[i]))
                {
                    Debug.LogError("Duplicate events " + m_events[i]);
                    continue;
                }

                Delegates addme = new Delegates();
                addme.m_eventName = m_events[i];

                m_eventsDictionary.Add(m_events[i], addme);
            }
        }

        private void OnDestroy()
        {
            if (Instance != this)
            {
                Debug.LogError(name + "duplicate instance.");
                return;
            }
            Instance = null;
        }

        private void Start()
        {

        }

        public void RegisterToEvent(string eventName, EventDelegate eventDelegate)
        {
            if (!m_eventsDictionary.ContainsKey(eventName))
            {
                Debug.LogError(eventName + " is not registered.");
                return;
            }

            m_eventsDictionary[eventName].Delegate += eventDelegate;
        }

        public void UnegisterToEvent(string eventName, EventDelegate eventDelegate)
        {
            if (!m_eventsDictionary.ContainsKey(eventName))
            {
                Debug.LogError(eventName + " is not registered.");
                return;
            }

            m_eventsDictionary[eventName].Delegate -= eventDelegate;
        }

        public void CallEvent(string eventName)
        {
            if (!m_eventsDictionary.ContainsKey(eventName))
            {
                Debug.LogError(eventName + " is not registered.");
                return;
            }

            m_eventsDictionary[eventName].m_lastCalledAt = Time.time;
            if (m_eventsDictionary[eventName].Delegate != null)
            {
                m_eventsDictionary[eventName].Delegate.Invoke();
            }
        }

        public float EventCalledTime(string eventName)
        {
            if (!m_eventsDictionary.ContainsKey(eventName))
            {
                Debug.LogWarning(eventName + " is not registered.");
                return -1f;
            }

            return m_eventsDictionary[eventName].m_lastCalledAt;
        }
    }
}
