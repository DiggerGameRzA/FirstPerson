using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject loadingPrefab;

    List<AsyncOperation> sceneLoading = new List<AsyncOperation>();
    private void Awake()
    {
        instance = this;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
    public void LoadGame()
    {
        loadingPrefab.SetActive(true);

        sceneLoading.Add(SceneManager.UnloadSceneAsync(1));
        sceneLoading.Add(SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgess());
    }

    public IEnumerator GetSceneLoadProgess()
    {
        for (int i = 0; i < sceneLoading.Count; i++)
        {
            while (!sceneLoading[i].isDone)
            {
                yield return null;
            }
        }

        loadingPrefab.SetActive(false);
    }
}
