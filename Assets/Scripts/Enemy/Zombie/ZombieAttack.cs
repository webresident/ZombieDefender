using UnityEngine;

public class ZombieAttack : StateMachineBehaviour
{
    private float attackRange = 1.7f;

    private Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        LookAtPlayerByY(animator);
    } 

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!PlayerManager.instance.IsPlayerDead())
        {
            LookAtPlayerByY(animator);
            float distanceToPlayer = Vector3.Distance(player.position, animator.transform.position);
            if (distanceToPlayer > attackRange)
            {
                animator.SetBool("isAttacking", false);
            }
        }
        else
        {
            Debug.Log("Player is dead, switching to partol");
            animator.SetBool("isAttacking", false);
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
