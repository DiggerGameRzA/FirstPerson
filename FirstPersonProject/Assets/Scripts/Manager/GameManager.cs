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
    
    [Header("Managers")]
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] InputManager inputManager;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Inventory inventory;

    List<AsyncOperation> sceneLoading = new List<AsyncOperation>();
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            print("Destroy myself");
            Destroy(this.gameObject);
        }
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        SceneManager.LoadSceneAsync((int)SceneEnum.MainMenu, LoadSceneMode.Additive);
    }
    public IEnumerator GetSceneLoadProgess(bool level, Scene scene)
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
        SceneManager.SetActiveScene(scene);
        totalProgress = 100f;
        bar.GetComponent<RectTransform>().sizeDelta = new Vector2(totalProgress, 100);
        percent.GetComponent<Text>().text = Mathf.RoundToInt(totalProgress) + " %";

        LeanTween.alphaCanvas(loadingPrefab.GetComponent<CanvasGroup>(), 0f, 0.5f).setOnComplete(FinishedLoading);

        if (level)
        {
            EnableManagers(level);
        }
    }
    public IEnumerator GetSceneLoadProgess(bool level,Scene scene, Vector3 position)
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
        SceneManager.SetActiveScene(scene);
        totalProgress = 100f;
        bar.GetComponent<RectTransform>().sizeDelta = new Vector2(totalProgress, 100);
        percent.GetComponent<Text>().text = Mathf.RoundToInt(totalProgress) + " %";

        GameObject.FindObjectOfType<Player>().transform.position = position;

        LeanTween.alphaCanvas(loadingPrefab.GetComponent<CanvasGroup>(), 0f, 0.5f).setOnComplete(FinishedLoading);

        if (level)
        {
            EnableManagers(level);
        }
    }
    void FinishedLoading()
    {
        loadingPrefab.SetActive(false);
        loadingPrefab.GetComponent<CanvasGroup>().alpha = 1;
        Debug.Log("Load scene successful!");
    }
    void EnableManagers(bool enable)
    {
        cameraManager.enabled = enable;
        weaponManager.enabled = enable;
        inputManager.enabled = enable;
        dialogueManager.enabled = enable;
        if (enable)
        {
            cameraManager.Restart();
            weaponManager.Restart();
            inputManager.Restart();
            dialogueManager.Restart();
        }
    }
    public void LoadCutscene01()
    {
        LoadCutscene((int)SceneEnum.MainMenu, (int)SceneEnum.Cutscene01);
    }
    public void LoadLevel01()
    {
        LoadScene((int)SceneEnum.Cutscene01, (int)SceneEnum.Level01);
    }
    public void LoadMedRoom()
    {
        LoadScene((int)SceneEnum.Level01, (int)SceneEnum.MedicalRoom);
    }
    public void LoadMedToLevel01()
    {
        LoadScene((int)SceneEnum.MedicalRoom, (int)SceneEnum.Level01, new Vector3(19, 4, 72));
    }
    void LoadCutscene(int unload, int load)
    {
        Scene getScene = SceneManager.GetSceneByBuildIndex(load);
        loadingPrefab.SetActive(true);

        sceneLoading.Add(SceneManager.UnloadSceneAsync(unload);
        sceneLoading.Add(SceneManager.LoadSceneAsync(load, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgess(false, getScene));
    }
    void LoadScene(int unload, int load)
    {
        Scene getScene = SceneManager.GetSceneByBuildIndex(load);
        loadingPrefab.SetActive(true);

        sceneLoading.Add(SceneManager.UnloadSceneAsync(unload);
        sceneLoading.Add(SceneManager.LoadSceneAsync(load, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgess(true, getScene));
    }
    void LoadScene(int unload, int load, Vector3 position)
    {
        Scene getScene = SceneManager.GetSceneByBuildIndex(load);
        loadingPrefab.SetActive(true);

        sceneLoading.Add(SceneManager.UnloadSceneAsync(unload);
        sceneLoading.Add(SceneManager.LoadSceneAsync(load, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgess(true, getScene, position));
    }
}
