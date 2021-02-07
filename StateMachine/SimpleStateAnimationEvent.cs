/**
=====================
License at:
https://github.com/aloknarula/UnityFramework/tree/master/StateMachine
Name:			SimpleStateMachine.cs
Version:		1.1
Update Date:	17, May, 2015
Author:			Alok Narula
Description:	
=====================
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using System.Security.Principal;

public class SimpleStateAnimationEvent : StateMachineBehaviour
{
    public string m_eventName;
    [Range(0f,1f)]
    public float m_triggerAt;

    bool m_triggered;
    SimpleStateMachineMono m_stateMachine;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_triggered = false;
        if (m_stateMachine == null)
        {
            m_stateMachine = animator.GetComponent<SimpleStateMachineMono>();
            
            if(m_stateMachine == null)
            {
                m_stateMachine = animator.GetComponentInParent<SimpleStateMachineMono>();
            }

            if(m_stateMachine == null)
            {
                Debug.LogError(animator.gameObject.name + " no state machine on or above animator");
                return;
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(m_stateMachine == null)
        {
            return;
        }

        float normalizeTime = stateInfo.normalizedTime % 1;
        int loop = Mathf.FloorToInt(stateInfo.normalizedTime);


        if(!m_triggered && normalizeTime >= m_triggerAt)
        {
            m_triggered = true;
            m_stateMachine.AnimationEvent(m_eventName);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
