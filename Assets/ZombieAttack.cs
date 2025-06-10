using UnityEngine;

public class ZombieAttack : StateMachineBehaviour
{
    private float attackRange = 1.7f;

    private Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerRespawnManager.instance.IsPlayerDead())
        {
            OnStateExit(animator, stateInfo, layerIndex);
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator.transform.LookAt(player);
    } 

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerRespawnManager.instance.IsPlayerDead())
        {
            OnStateExit(animator, stateInfo, layerIndex);
        }

        animator.transform.LookAt(player);
        float distanceToPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceToPlayer > attackRange)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
