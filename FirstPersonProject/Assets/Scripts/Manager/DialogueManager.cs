using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] UIManager uiManager;
    public Queue<string> Sentences;
    public Queue<Sprite> Profile;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        Sentences = new Queue<string>();
        Profile = new Queue<Sprite>();

        //Restart();
    }
    private void Update()
    {

    }

    public void StartConversation(Dialogue dialogue)
    {
        print("Starting conversations");
        InputManager.instance.OnConversation(true);

        if(Sentences != null)
        {
            Sentences.Clear();
            Profile.Clear();
        }

        foreach (string _sentence in dialogue.sentence)
        {
            Sentences.Enqueue(_sentence);
        }
        foreach (Sprite _profile in dialogue.profile)
        {
            Profile.Enqueue(_profile);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        uiManager.ShowContinue(false);
        InputManager.instance.canSkip = false;

        if(Sentences.Count == 0)
        {
            EndConversation();
            return;
        }
        string _sentence = Sentences.Dequeue();
        Sprite _profile = Profile.Dequeue();

        uiManager.DialogueUpdateProfile(_profile);
        StartCoroutine(uiManager.TypeSentence(_sentence));
    }
    void EndConversation()
    {
        uiManager.EndSentence();
    }
    public void Restart()
    {
        uiManager = FindObjectOfType<UIManager>();
        print("restart");

        bool firstTime1 = false;
        bool firstTime2 = false;
        foreach (FirstTimeScene i in SaveManager.instance.firstTimeScenes)
        {
            if(i.scene == "Level01" && i.firstTime)
            {
                GameObject.Find("Con1").GetComponent<NPCDialogue>().Invoke("TriggerDialogue", 0.2f);
                if (!firstTime1)
                {
                    FindObjectOfType<SoundManager>().PlayBGM();
                    firstTime1 = true;
                }
                i.firstTime = false;
                break;
            }
            if(i.scene == "Final" && i.firstTime)
            {
                GameObject.Find("Con10").GetComponent<NPCDialogue>().Invoke("TriggerDialogue", 0.2f);
                if (!firstTime2)
                {
                    FindObjectOfType<SoundManager>().PlayBossBGM();
                    firstTime2 = true;
                }
                i.firstTime = false;
                break;
            }
        }
    }
}
