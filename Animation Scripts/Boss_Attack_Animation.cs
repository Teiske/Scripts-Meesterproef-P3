using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack_Animation : StateMachineBehaviour {


    [SerializeField] private float timer;
    [SerializeField] private float min_time;
    [SerializeField] private float max_time;
    [SerializeField] private float boss_speed;

    private Transform player_pos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        player_pos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = Random.Range(min_time, max_time);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (timer <= 0) {
            animator.SetTrigger("Boss_Idle");
        }
        else {
            timer -= Time.deltaTime;
        }

        Vector2 target = new Vector2(player_pos.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, boss_speed * Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }

}
