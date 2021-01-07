using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.Slide = true;
        Player.movementSpeed = 8;


        Player.Instance.playerHitbox.size = new Vector2(Player.ToSingle(0.7795854),Player.ToSingle(0.7795855));
        Player.Instance.playerHitbox.offset = new Vector2(Player.ToSingle(-0.376302),Player.ToSingle(-0.4546083));
        // Player.Instance.playerHitbox.direction = CapsuleDirection2D.Horizontal;   
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.Slide = false;
        animator.ResetTrigger("slide");
        Player.movementSpeed = 7;


        Player.Instance.playerHitbox.size = new Vector2(Player.ToSingle(0.7795854),Player.ToSingle(1.459395));
        Player.Instance.playerHitbox.offset = new Vector2(Player.ToSingle(-0.106302),Player.ToSingle(-0.07683676));
        // Player.Instance.playerHitbox.direction = CapsuleDirection2D.Vertical;
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
