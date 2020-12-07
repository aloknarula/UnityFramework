/**
=====================
// license at:
https://github.com/aloknarula/UnityFramework
Name:			SimpleState.cs
Version:		1.0
Update Date:	08, December, 2010
Author:			Alok Narula
Description:	
=====================
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using System.Security.Principal;

public class SimpleAnimationEvent : StateMachineBehaviour
{
    public string m_eventName;
    [Range(0f,1f)]
    public float m_triggerAt;

    int m_triggered;
    SimpleStateMachineMono m_stateMachine;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_triggered = 0;
        if (m_stateMachine == null)
        {
            m_stateMachine = animator.GetComponent<SimpleStateMachineMono>();
            if(m_stateMachine == null)
            {
                Debug.LogError(animator.gameObject.name + " no state machine component.");
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float normalizeTime = stateInfo.normalizedTime % 1;
        int loop = Mathf.FloorToInt(stateInfo.normalizedTime);
        /*if(m_loopEvent && normalizeTime >= m_resetLoopAt)
        {
            Debug.Log("-reset");
            m_triggered++;
        }*/

        if(m_stateMachine == null)
        {
            return;
        }

        if(loop == m_triggered && normalizeTime >= m_triggerAt)
        {
            m_triggered++;
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
