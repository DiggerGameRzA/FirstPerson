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
    public GameObject percent;
    [SerializeField] float totalProgress;

    WeaponManager weaponManager;
    InputManager inputManager;
    CameraManager cameraManager;
    DialogueManager dialogueManager;

    List<AsyncOperation> sceneLoading = new List<AsyncOperation>();
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        weaponManager = FindObjectOfType<WeaponManager>();
        inputManager = FindObjectOfType<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    private void Start()
    {
        SceneManager.LoadSceneAsync((int)SceneEnum.MainMenu, LoadSceneMode.Additive);
    }
    public void LoadCutscene(int scene)
    {
        loadingPrefab.SetActive(true);

        sceneLoading.Add(SceneManager.UnloadSceneAsync(1));
        sceneLoading.Add(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgess(false));
    }
    public void LoadGame(int scene)
    {
        loadingPrefab.SetActive(true);

        sceneLoading.Add(SceneManager.UnloadSceneAsync(2));
        sceneLoading.Add(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive));

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
                percent.GetComponent<Text>().text = Mathf.RoundToInt(totalProgress) + " %";

                yield return null;
            }
        }

        //Finished loading
        totalProgress = 100f;
        bar.GetComponent<RectTransform>().sizeDelta = new Vector2(totalProgress, 100);
        percent.GetComponent<Text>().text = Mathf.RoundToInt(totalProgress) + " %";

        LeanTween.alphaCanvas(loadingPrefab.GetComponent<CanvasGroup>(), 0f, 0.5f).setOnComplete(FinishedLoading);

        if (level)
        {
            cameraManager.enabled = true;
            weaponManager.enabled = true;
            inputManager.enabled = true;
            dialogueManager.enabled = true;
        }
    }
    void FinishedLoading()
    {
        loadingPrefab.SetActive(false);
        loadingPrefab.GetComponent<CanvasGroup>().alpha = 1;
        Debug.Log("Load game successful!");
    }
}
