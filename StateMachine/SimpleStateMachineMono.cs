/**
=====================
Name:			SimpleStateMachine.cs
Version:		0.1
Update Date:	17, May, 2015
Author:			Alok Narula
Description:	
=====================
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleStateMachineMono : MonoBehaviour
{
    [SerializeField]
    protected string m_defaultState;

    protected Dictionary<string, SimpleState> m_states;

    protected SimpleState m_lastState;
    protected SimpleState m_currentState;
    protected SimpleState m_requestedState;

    public Transform m_transform { get; private set; }
    public float m_stateTime { get; private set; }

    public virtual void Awake()
    {
        m_transform = transform;
        m_states = new Dictionary<string, SimpleState>();
    }

    public virtual void RequestState(string stateName)
    {
        SimpleState state = FindState(stateName);

        if (state == null)
        {
            Debug.LogError("Requesting NULL state " + stateName);
            return;
        }
        m_requestedState = state;
    }

    public string CurrentStateName()
    {
        if(m_currentState != null)
        {
            return m_currentState.StateName;
        }
        return "";
    }

    public void RequestState(SimpleState state)
    {
        if (state == null)
        {
            Debug.LogError("Requesting NULL state");
            return;
        }
        m_requestedState = state;
    }

    void SwitchState()
    {
        if (m_requestedState == m_currentState)
        {
            return;
        }

        if (m_currentState != null)
        {
            m_currentState.OnExit();
        }
        m_stateTime = 0.0f;
        m_requestedState.OnEnter();
        m_lastState = m_currentState;
        m_currentState = m_requestedState;
    }

    public void RegisterState(SimpleState state)
    {
        if (FindState(state.StateName) != null)
        {
            Debug.LogError("Can't re-register " + state);
            return;
        }

        //Debug.Log("Registered " + state.StateName);
        m_states.Add(state.StateName, state);
    }

    public void UnregisterState(SimpleState state)
    {
        if (FindState(state.StateName) == null)
        {
            Debug.LogError("Can't un-register as the state is not there in state machine " + state);
            return;
        }

        //Debug.Log("Unregistered " + state);
        m_states.Remove(state.StateName);
    }

    public SimpleState FindState(string stateName)
    {
        if (!m_states.ContainsKey(stateName))
            return null;

        return m_states[stateName];
    }
    
    public virtual void Start()
    {
        Invoke("DelayedSet", 0.1f);
	}

    void DelayedSet()
    {
        RequestState(m_defaultState);
    }
	
	// Update is called once per frame
    public virtual void Update()
    {
        if (m_currentState != null)
        {
            m_stateTime += Time.deltaTime;
            m_currentState.OnUpdate();
        }

        SwitchState();
	}

    /*
    public virtual void FixedUpdate()
    {
        if (m_currentState != null)
        {
            m_currentState.OnFixedUpdate();
        }
        SwitchState();
	}
    */
}
