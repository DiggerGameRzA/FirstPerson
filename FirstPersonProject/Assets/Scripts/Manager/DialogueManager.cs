using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    public Queue<string> Sentences;
    public Queue<string> Name;
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        Sentences = new Queue<string>();
        Name = new Queue<string>();
    }

    public void StartConversation(Dialogue dialogue)
    {
        if(Sentences != null)
        {
            Sentences.Clear();
            Name.Clear();
        }

        foreach (string _sentence in dialogue.sentence)
        {
            Sentences.Enqueue(_sentence);
        }
        foreach (string _name in dialogue.name)
        {
            Name.Enqueue(_name);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(Sentences.Count == 0)
        {
            return;
        }
        string _sentence = Sentences.Dequeue();
        string _name = Name.Dequeue();

        uiManager.DialogueUpdateName(_name);
        StartCoroutine(uiManager.TypeSentence(_sentence));
    }
}
