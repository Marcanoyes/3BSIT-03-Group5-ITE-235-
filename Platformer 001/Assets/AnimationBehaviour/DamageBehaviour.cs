﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.GetComponent<Character>().TakingDamage = false;
       
       if(animator.gameObject.tag == "Player")
       {
           Player.movementSpeed = 0;
           Player.Instance.slash.SetActive(false);
           Player.Instance.SwordDisable();
       }
       else
       {
           EnemyDragon.EnemyMovementSpeed = 0;
       }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.gameObject.tag == "Player")
       {
           Player.movementSpeed = 7;
       }
       else
       {
           EnemyDragon.EnemyMovementSpeed = 2;
       }
       
    }

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
