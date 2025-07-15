using TMPro;
using UnityEngine;

public class CharacterInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject interactionContainer;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private CharacterInteract playerInteract;

    private void Update()
    {
        if (playerInteract.GetInteractableObject() !=  null)
        {
            Show(playerInteract.GetInteractableObject());
        }
        else
        {
            Hide();
        }
    }

    private void Show(IInteractable interactable)
    {
        interactionContainer.SetActive(true);
        interactText.text = "[E] - " + interactable.GetInteractText();
    }

    private void Hide()
    {
        interactionContainer?.SetActive(false);
    }
}
