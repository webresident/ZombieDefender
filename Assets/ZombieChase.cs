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

        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = 5f;
        agent.isStopped = false;
        agent.SetDestination(player.position);
        agent.transform.LookAt(player);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerRespawnManager.instance.IsPlayerDead())
        {
            OnStateExit(animator, stateInfo, layerIndex);
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
