using TMPro;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject interactionIcon;

    public GameObject interactKeyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionIcon.SetActive(false);
    }

    //use this approach instead since we are not using the input system
    void Update()
    {
        if (interactableInRange != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interaction with NPC!");
            interactableInRange?.Interact();
        }
    }


    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
            interactKeyText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
            interactKeyText.SetActive(false);
        }
    }
}
