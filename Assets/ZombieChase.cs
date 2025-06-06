using UnityEngine;
using UnityEngine.AI;

public class ZombieChase : StateMachineBehaviour
{
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float chasingRange = 12;

    private NavMeshAgent agent;
    private Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = 3.5f;
        agent.isStopped = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);

        float distanceToPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceToPlayer > 12)
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
        agent.isStopped = true;
    }
}
