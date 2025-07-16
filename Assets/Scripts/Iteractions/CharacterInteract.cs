using System.Collections.Generic;
using UnityEngine;

public class CharacterInteract : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IInteractable interactable = GetInteractableObject();
            if (interactable != null)
            {
                interactable.Interact(transform);
            }
        }
    }

    public IInteractable GetInteractableObject()
    {
        List<IInteractable> interactableList = new List<IInteractable>();
        float interactRange = 4f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactableList.Add(interactable);
            }
        }

        IInteractable closestNPCInteractable = null;
        foreach (IInteractable npcInteractable in interactableList)
        {
            if (closestNPCInteractable == null)
            {
                closestNPCInteractable = npcInteractable;
            }
            else
            {
                if (Vector3.Distance(transform.position, npcInteractable.GetTransform().position) <
                    Vector3.Distance(transform.position, closestNPCInteractable.GetTransform().position))
                {
                    closestNPCInteractable = npcInteractable;
                }
            }
        }

        return closestNPCInteractable;
    }
}
