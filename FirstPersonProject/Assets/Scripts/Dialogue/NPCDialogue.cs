using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public void TriggerDialogue()
    {
        //DialogueManager.instance.StartConversation(dialogue);
    }
    public void DisplayNextSentence()
    {
        DialogueManager.instance.DisplayNextSentence();
    }
}
