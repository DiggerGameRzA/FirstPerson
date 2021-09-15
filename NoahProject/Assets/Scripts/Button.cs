using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    float loadDelay = 0.5f;

    AudioSource audioSource;
    public AudioClip onHover;
    public AudioClip onClick;
    public AudioClip onNext;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayHover()
    {
        audioSource.PlayOneShot(onHover);
    }
    public void PlayClick()
    {
        audioSource.PlayOneShot(onClick);
    }
    public void PlayNext()
    {
        audioSource.PlayOneShot(onNext);
    }

    public void ResetGame()
    {
        GameManager.instance.ResetGame();
    }
    public void QuitGame()
    {
        Invoke("ActualQuit", 0.2f);
    }
    void ActualQuit()
    {
        Application.Quit();
    }
    public void LoadCutscene01()
    {
        GameManager.instance.Invoke("LoadCutscene01", loadDelay);
    }
    public void LoadLevel01()
    {
        GameManager.instance.Invoke("LoadLevel01", loadDelay);
    }
    public void LoadTitle()
    {
        GameObject go = new GameObject("TheDestroyer");
        DontDestroyOnLoad(go);
        foreach (var root in go.scene.GetRootGameObjects())
        {
            Destroy(root);
        }
        SceneManager.LoadScene(0);
    }
    void ActualLoadTitle()
    {
        SceneManager.LoadScene(0);
    }
}
