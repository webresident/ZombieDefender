using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float interactionDistance = 2f;

    private bool isOpened = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            isOpened = !isOpened;
            anim.SetBool("useDoor", isOpened);
        }
    }
}
