using UnityEngine;

public class ZombieIdle : StateMachineBehaviour
{
    private float timer;
    private float chaseRange = 8;
    private Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject is not null)
        {
            player = playerObject.transform;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        if (timer > 5)
        {
            animator.SetBool("isPatrolling", true);
        }

        if (player is not null)
        {
            float distanceToPlayer = Vector3.Distance(player.position, animator.transform.position);
            if (distanceToPlayer < chaseRange)
            {
                animator.SetBool("isChasing", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
