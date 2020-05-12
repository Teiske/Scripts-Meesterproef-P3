﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Intro_Animation : StateMachineBehaviour {

    private int random;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        random = Random.Range(0, 2);

        if (random == 0) {
            animator.SetTrigger("Boss_Idle");
        }
        else {
            animator.SetTrigger("Boss_Attack");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }
}
