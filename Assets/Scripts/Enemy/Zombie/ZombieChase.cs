using UnityEngine;
using UnityEngine.AI;

public class ZombieChase : StateMachineBehaviour
{
    private float attackRange = 2f;

    private NavMeshAgent agent;
    private Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerRespawnManager.instance.IsPlayerDead())
        {
            OnStateExit(animator, stateInfo, layerIndex);
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject is not null)
        {
            player = playerObject.transform;
        }
        else
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isPatrolling", true);
            return;
        }

        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 5f;
        agent.isStopped = false;
        agent.SetDestination(player.position);
        agent.transform.LookAt(player);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player is null)
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isPatrolling", true);
            return;
        }

        agent.transform.LookAt(player);
        agent.SetDestination(player.position);

        float distanceToPlayer = Vector3.Distance(player.position, agent.transform.position);
        if (distanceToPlayer > 8)
        {
            animator.SetBool("isChasing", false);
        }

        if (distanceToPlayer < attackRange)
        {
            animator.SetBool("isAttacking", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
