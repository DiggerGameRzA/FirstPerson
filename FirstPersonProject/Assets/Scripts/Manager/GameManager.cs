using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject loadingPrefab;
    public GameObject bar;
    [SerializeField] float totalProgress;

    WeaponManager weaponManager;
    InputManager inputManager;
    CameraManager cameraManager;
    DialogueManager dialogueManager;

    List<AsyncOperation> sceneLoading = new List<AsyncOperation>();
    private void Awake()
    {
        instance = this;
        weaponManager = FindObjectOfType<WeaponManager>();
        inputManager = FindObjectOfType<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    private void Start()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
    public void LoadCutscene()
    {
        loadingPrefab.SetActive(true);

        sceneLoading.Add(SceneManager.UnloadSceneAsync(1));
        sceneLoading.Add(SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgess(false));
    }
    public void LoadGame()
    {
        loadingPrefab.SetActive(true);

        sceneLoading.Add(SceneManager.UnloadSceneAsync(2));
        sceneLoading.Add(SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgess(true));
    }

    public IEnumerator GetSceneLoadProgess(bool level)
    {
        for (int i = 0; i < sceneLoading.Count; i++)
        {
            while (!sceneLoading[i].isDone)
            {
                totalProgress = 0;
                foreach (AsyncOperation operation in sceneLoading)
                {
                    totalProgress += operation.progress;
                }

                totalProgress = (totalProgress / sceneLoading.Count) * 100f;

                bar.GetComponent<RectTransform>().sizeDelta = new Vector2(totalProgress, 100);

                yield return null;
            }
        }

        loadingPrefab.SetActive(false);
        Debug.Log("Load game successful!");

        if (level)
        {
            cameraManager.enabled = true;
            weaponManager.enabled = true;
            inputManager.enabled = true;
            dialogueManager.enabled = true;
        }
    }
}
