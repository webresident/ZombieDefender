using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool isOpen = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        anim.SetBool("useDoor", isOpen);
    }

    public void Interact(Transform interactorTransform)
    {
        ToggleDoor();
    }

    public string GetInteractText()
    {
        return "Door";
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
