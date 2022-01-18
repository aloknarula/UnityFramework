using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SimpleFramework
{
    public class SimpleEventListener : MonoBehaviour
    {
        // Public //
        // Protected //
        [SerializeField]
        protected string m_event;
        [SerializeField]
        protected UnityEvent m_onTrigger;
        // Private //
        // Access //

        private void Start()
        {
            if (m_event != "")
            {
                SimpleEventHandler.Instance.RegisterToEvent(m_event, Trigger);
            }
        }

        private void Trigger()
        {
            m_onTrigger.Invoke();
        }

        private void OnDestroy()
        {
            if (m_event != "")
            {
                if (SimpleEventHandler.Instance != null)
                {
                    SimpleEventHandler.Instance.UnegisterToEvent(m_event, Trigger);
                }
            }
        }
    }
}
