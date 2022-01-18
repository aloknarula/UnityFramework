/**
=====================
// license at:
https://github.com/aloknarula/UnityFramework
Name:			SimpleState.cs
Version:		1.1
Update Date:	17, May, 2015
Author:			Alok Narula
Description:	
=====================
*/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;

namespace SimpleFramework
{
    public class SimpleState : MonoBehaviour
    {
        [Header("Simple State")]
        [SerializeField]
        private string m_stateName;
        public string StateName { get { return m_stateName; } }
        [SerializeField]
        private string m_exitState;
        public string ExitState { get { return m_exitState; } }

        [SerializeField]
        private UnityEvent m_onEnter;
        [SerializeField]
        private UnityEvent m_onExit;
        [SerializeField]
        protected TimedEvent[] m_timedEvents;
        public Transform Trans { get; private set; }

        [System.Serializable]
        public class TimedEvent
        {
            public string m_name;
            public float m_time;
            public UnityEvent m_events;
            [HideInInspector]
            public bool m_triggered;
        }

        protected SimpleStateMachineMono m_stateMachine;

        public virtual void Start()
        {
            m_stateMachine.RegisterState(this);
        }

        public virtual void Awake()
        {
            if (m_stateName == "")
            {
                Debug.LogError(name + " state name not set");
                return;
            }
            SimpleStateMachineMono sm = GetComponent<SimpleStateMachineMono>();
            if (sm == null)
            {
                sm = GetComponentInParent<SimpleStateMachineMono>();
            }
            if (sm == null)
            {
                Debug.LogError(name + " State machine not present.");
                return;
            }
            m_stateMachine = sm;
            Trans = m_stateMachine.transform;
        }

        [ContextMenu("ExitState")]
        void ContextRequestExitState()
        {
            if(ExitState == "")
            {
                return;
            }

            m_stateMachine.RequestState(ExitState);
        }

        public virtual void Update()
        {

        }

        public virtual void OnStateTriggerEnter(Collider other)
        {

        }
        
        public virtual void OnStateTriggerExit(Collider other)
        {

        }
        
        public virtual void OnStateCollisionEnter(Collision collision)
        {

        }
        
        public virtual void OnStateCollisionExit(Collision collision)
        {

        }

        public virtual void ApplyRootMotion(Vector3 deltaPos, Quaternion deltaRotation)
        {
        }

        public virtual void OnEnter()
        {
            m_onEnter.Invoke();
            
            for (int i = 0; i < m_timedEvents.Length; i++)
            {
                m_timedEvents[i].m_triggered = false;
            }
        }

        public virtual void OnExit()
        {
            m_onExit.Invoke();
        }

        public virtual void OnUpdate()
        {
            for (int i = 0; i < m_timedEvents.Length; i++)
            {
                if (m_timedEvents[i].m_triggered)
                {
                    continue;
                }

                if (m_stateMachine.m_stateTime >= m_timedEvents[i].m_time)
                {
                    m_timedEvents[i].m_triggered = true;
                    m_timedEvents[i].m_events.Invoke();
                }
            }
        }

        public virtual void OnLateUpdate()
        {
        }

        public virtual void OnFixedUpdate()
        {
        }

        public virtual void OnRootMotion(Vector3 deltaPos, Quaternion deltaRot)
        {
        }

        public virtual void AnimState(string eventName)
        {

        }

        public virtual void AnimationEvent(string eventName)
        {
        }
    }
}
