using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieWalk : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private List<Transform> waypoints = new List<Transform>();
    private Transform nextWaypoint;

    private float chaseRange = 8;
    private Transform player;

    private float waitTime = 5f;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.speed = 2f;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        GameObject go = GameObject.FindGameObjectWithTag("PatrollWaypoints");
        waypoints.Clear();
        foreach (Transform transform in go.transform)
        {
            waypoints.Add(transform);
        }

        SetNextDestination();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceToPlayer = Vector3.Distance(player.position, agent.transform.position);
        if (distanceToPlayer < chaseRange)
        {
            animator.SetBool("isChasing", true);
        }

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                SetNextDestination();
            }
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                StartWaiting();
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    private void SetNextDestination()
    {
        if (waypoints.Count == 0)
        {
            return;
        }

        nextWaypoint = waypoints[Random.Range(0, waypoints.Count)];
        agent.SetDestination(nextWaypoint.position);

        agent.GetComponent<Animator>().SetBool("isPatrolling", true);
    }

    private void StartWaiting()
    {
        isWaiting = true;
        waitTimer = 0f;
        agent.isStopped = true;

        agent.GetComponent<Animator>().SetBool("isPatrolling", false);
    }
}
