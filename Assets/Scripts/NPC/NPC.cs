using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("Messages")]
    [SerializeField] protected NPCData data;
    [SerializeField] protected GameObject container;
    [SerializeField] protected GameObject questionPrefab;
    [SerializeField] protected GameObject textPrefab;

    [Header("Player Interaction")]
    [SerializeField] private Transform pointDefault;
    [SerializeField] private bool isInteraction = false;
    private Transform player;

    [Header("NPC Elements")]
    [SerializeField] private Animator anim;


    private void Update()
    {
        LookAtTarget(player);
    }

    public virtual void Interact(Transform target)
    {
        anim.SetTrigger("isTalk");
        isInteraction = true;
        player = target;
        LookAtTarget(player);

        Debug.Log(name + " interaction");
    }

    private void LookAtTarget(Transform target)
    {
        Vector3 targetPosition;

        if (isInteraction)
        {
            targetPosition = target.position;
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

    public void FillQuestion(string description)
    {
        GameObject question = Instantiate(questionPrefab, container.transform);
        TextMeshProUGUI questionDesctiption = question.GetComponentInParent<TextMeshProUGUI>();
        if (questionDesctiption != null)
        {
            questionDesctiption.text = description;
        }
    }

    public void FillText(string description)
    {
        GameObject conversation = Instantiate(textPrefab, container.transform);
        if (conversation.TryGetComponent(out TextMeshProUGUI conversationDescription))
        {
            conversationDescription.text = description;
        }
    }

    public string GetInteractText()
    {
        return name;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
