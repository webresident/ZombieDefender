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
    }

    private void Interact()
    {
        if (distanceToPlayer < interactionDistance && Input.GetKeyDown(KeyCode.E) && !isInteraction)
        {
            transform.LookAt(player);
            anim.SetTrigger("isTalk");
            isInteraction = true;
            DialogsOnOff(true);
        }
    }

    private void DisableInteraction()
    {
        if (distanceToPlayer > interactionDistance)
        {
            isInteraction = false;
            transform.LookAt(pointDefault);
            DialogsOnOff(false);
        }
    }

    private void DialogsOnOff(bool state)
    {
        //conversationView.SetActive(state);
    }

    private void ShowTrades()
    {

    }
}
