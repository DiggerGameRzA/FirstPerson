using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartConversation(dialogue);
    }
    public void DisplayNextSentence()
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }
}
