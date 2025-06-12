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
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 5f;
        agent.isStopped = false;
        agent.SetDestination(player.position);
        LookAtPlayerByY(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!PlayerManager.instance.IsPlayerDead())
        {
            LookAtPlayerByY(animator);
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
        else
        {
            Debug.Log("Player is dead, switching to partol from chasing");
            animator.SetBool("isChasing", false);
            animator.SetBool("isPatrolling", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void LookAtPlayerByY(Animator animator)
    {
        Vector3 direction = player.position - animator.transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            animator.transform.rotation = targetRotation;
        }
    }
}
