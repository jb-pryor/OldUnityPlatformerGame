using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using System.Data.Common;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    private DialogueController dialogueUI;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    private PlayerMovement playerMovement;


    private void Start()
    {
        dialogueUI = DialogueController.Instance;
        Debug.Log("NPC connected to DialogueController: " + dialogueUI?.gameObject.name);
    }

    void Awake()
    {
        //dialogueUI = DialogueController.Instance;
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    public bool CanInteract()
    {
        return !isDialogueActive && !GameManager.Instance.IsShopOpen;
    }

    public void Interact()
    {
        // if no dialogue data
        if (dialogueData == null)
        {
            Debug.Log("No dialogue data assigned!");
            return;
        }

        // if the game is paused AND no dialogue is already active, prevent starting it
        // if (!isDialogueActive && PauseController.IsGamePaused)
        // {
        //     Debug.Log("Game is paused. No dialogue allowed.");
        //     return;
        // }

        if (isDialogueActive)
        {
            Debug.Log("B");
            NextLine();
        }
        else
        {
            Debug.Log("C");
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        dialogueUI.SetNPCInfo(dialogueData.npcName, dialogueData.npcPortrait );
        dialogueUI.ShowDialogueUI(true);
        playerMovement?.SetCanMove(false); 

        DisplayCurrentLine();
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }

        //clear existing choices
        dialogueUI.ClearChoices();
        
        //check enddialogueline
        if(dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            EndDialogue();
            playerMovement?.SetCanMove(true);
            return;
        }

        //check if we have any choices & display
        foreach(DialogueChoice dialogueChoice in dialogueData.choices)
        {
            if(dialogueChoice.dialogueIndex == dialogueIndex)
            {
                DisplayChoices(dialogueChoice);
                return;
            }
        }

        if(++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            //if another line, type next line
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
            playerMovement?.SetCanMove(true);
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText("");

        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
            yield return new WaitForSeconds(dialogueData.typingSpeed);

        }

        isTyping = false;

        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    void DisplayChoices(DialogueChoice choice)
    {
        Debug.Log($"Choices Length: {choice.choices.Length}, NextDialogueIndexes Length: {choice.nextDialogueIndexes?.Length ?? 0}");
        for (int i = 0; i < choice.choices.Length; i++)
        {
            if (choice.nextDialogueIndexes == null || i >= choice.nextDialogueIndexes.Length)
            {
                Debug.LogError($"Invalid nextDialogueIndexes array for choice index {i}. Ensure the array is properly configured.");
                continue; // Skip this choice if the index is invalid
            }

            int nextIndex = choice.nextDialogueIndexes[i];
            ChoiceAction action = (choice.actions != null && i < choice.actions.Length) ? choice.actions[i] : ChoiceAction.None;

            // Create a local copy of the loop variable
            int localIndex = i;

            Debug.Log($"Creating button for choice {localIndex}: {choice.choices[localIndex]} with nextIndex: {nextIndex}");

            dialogueUI.CreateChoiceButton(choice.choices[localIndex], () =>
            {
                Debug.Log($"Button {choice.choices[localIndex]} clicked");
                dialogueUI.ClearChoices(); // Clean up buttons

                switch (action)
                {
                    case ChoiceAction.OpenShop:
                        GetComponent<ShopNPC>()?.OpenShop(); // Open shop
                        EndDialogue();
                        break;

                    case ChoiceAction.None:
                    default:
                        ChooseOption(nextIndex); // Continue to the next dialogue
                        break;
                }
            });
        }
    }

    void ChooseOption(int nextIndex)
    {
        dialogueIndex = nextIndex;
        dialogueUI.ClearChoices();
        DisplayCurrentLine();
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    public void EndDialogue() //x-ing out of dialogue box causes player to attack, so i need to disable player movement further until its gone
    {
        Debug.Log("EndDialogue called for NPC: " + gameObject.name);
        StopAllCoroutines();
        isDialogueActive = false;
        if (dialogueUI != null)
        {
            dialogueUI.SetDialogueText("");
            dialogueUI.ShowDialogueUI(false);
        }
        else
        {
            Debug.LogWarning("Dialogue UI is null in EndDialogue.");
        }
        playerMovement?.SetCanMove(true);
    }
}
