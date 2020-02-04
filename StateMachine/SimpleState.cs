/**
=====================
Name:			SimpleState.cs
Version:		0.1
Update Date:	17, May, 2015
Author:			Alok Narula
Description:	
=====================
*/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;

public class SimpleState : MonoBehaviour
{
    [SerializeField]
    private string m_stateName;
    public string StateName { get { return m_stateName; }}

    public UnityEvent m_onEnter;
    public UnityEvent m_onExit;

    protected SimpleStateMachineMono m_stateMachine;

    public virtual void Start()
    {
        m_stateMachine.RegisterState(this);
    }

    public virtual void Awake()
    {
        if(m_stateName == "")
        {
            Debug.LogError(name + " state name not set");
            return;
        }
        SimpleStateMachineMono sm = GetComponent<SimpleStateMachineMono>();
        if(sm == null)
        {
            sm = GetComponentInParent<SimpleStateMachineMono>();
        }
        if(sm == null)
        {
            Debug.LogError(name + " State machine not present.");
            return;
        }
	    m_stateMachine = sm;
	}
	
    public virtual void Update()
    {
	
	}

    public virtual void OnEnter()
    {
        m_onEnter.Invoke();
    }

    public virtual void OnExit()
    {
        m_onExit.Invoke();
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnFixedUpdate()
    {
    }
}
