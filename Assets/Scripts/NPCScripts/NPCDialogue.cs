using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public bool[] endDialogueLines; //mark where dialogue is meant to end
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
    //public AudioClip voiceSound; //will use these later when sound and music is implemented
    //public float voicePitch = 1f;

    public DialogueChoice[] choices;
}

[System.Serializable]

public class DialogueChoice
{
    public int dialogueIndex; //Dialogue line where choices appear
    public string[] choices; // player response option
    public int[] nextDialogueIndexes; //where choice leads

    public ChoiceAction[] actions;
}

public enum ChoiceAction
{
    None,
    OpenShop,
    // other things like TriggerQuest, GiveItem, etc
}