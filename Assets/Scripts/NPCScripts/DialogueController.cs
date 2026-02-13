using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; } //singleton instance
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    public Transform choiceContainer;
    public GameObject choiceButtonPrefab;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ResetInstance()
    {
        Instance = null; // Reset the static instance when entering play mode
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject); //make sure only one instance
    }

    private void Start()
    {
        //ResetDialoguePanel();
    }

    public void ShowDialogueUI(bool show)
    {
        dialoguePanel.SetActive(show); // Toggle visibility

        // Check if CanvasGroup exists before accessing it
        var canvasGroup = dialoguePanel.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = show;
        }
        else
        {
            Debug.LogWarning("CanvasGroup is missing on DialoguePanel. Raycast blocking will not be toggled.");
        }
    }

    public void SetNPCInfo (string npcName, Sprite portrait)
    {
        nameText.text = npcName;
        portraitImage.sprite = portrait;
    }

    public void SetDialogueText (string text)
    {
        dialogueText.text = text;
    }

    public void ClearChoices()
    {
        foreach (Transform child in choiceContainer) Destroy(child.gameObject);
    }

    public void CreateChoiceButton(string choicetext, UnityEngine.Events.UnityAction onClick)
    {
        Debug.Log("Creating choice button: " + choicetext);
        GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);
        choiceButton.GetComponentInChildren<TMP_Text>().text = choicetext;

        var button = choiceButton.GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("No Button component found on choiceButtonPrefab!");
        }
        else
        {
            button.onClick.RemoveAllListeners(); // Clear any existing listeners
            button.onClick.AddListener(onClick); // Add the custom action for the choice
            // Do NOT hide the dialogue UI here unless it's the last step
            // button.onClick.AddListener(() => ShowDialogueUI(false)); // Remove or move this
        }
    }

    private void ResetDialoguePanel()
    {
        ShowDialogueUI(false); // Ensure the panel is hidden
        ClearChoices();        // Clear any leftover buttons
        dialogueText.text = ""; // Clear dialogue text
    }

}
