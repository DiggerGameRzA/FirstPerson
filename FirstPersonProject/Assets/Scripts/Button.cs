using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void ResetGame()
    {
        GameManager.instance.ResetGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadCutscene01()
    {
        GameManager.instance.LoadCutscene01();
    }
    public void LoadLevel01()
    {
        GameManager.instance.LoadLevel01();
    }
}
