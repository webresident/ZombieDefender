using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("User Interface")]
    [SerializeField] private GameObject conversationView;
    [SerializeField] private GameObject tradeView;


    [Header("Player Interaction")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform pointDefault;
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private bool isInteraction = false;

    [Header("NPC Elements")]
    [SerializeField] private Animator anim;

    private float distanceToPlayer = 0f;

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    
        Interact();
        DisableInteraction();
        LookAtTarget();
    }

    private void Interact()
    {
        if (distanceToPlayer < interactionDistance && Input.GetKeyDown(KeyCode.E) && !isInteraction)
        {
            anim.SetTrigger("isTalk");
            isInteraction = true;
        }
    }

    private void DisableInteraction()
    {
        if (distanceToPlayer > interactionDistance)
        {
            isInteraction = false;
        }
    }

    private void LookAtTarget()
    {
        Vector3 targetPosition;

        if (isInteraction)
        {
            targetPosition = player.position;
        }
        else
        {
            targetPosition = pointDefault.position;
        }

        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }
}
