using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float interactionDistance = 2f;

    [SerializeField] private TextMeshProUGUI interacteText;

    private bool isOpened = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= interactionDistance)
        {
            interacteText.text = "[E] - Door";
            interacteText.gameObject.SetActive(true);
        }
        else
        {
            interacteText.gameObject.SetActive(false);
        }

        if (dist < interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            isOpened = !isOpened;
            anim.SetBool("useDoor", isOpened);
        }
    }
}
