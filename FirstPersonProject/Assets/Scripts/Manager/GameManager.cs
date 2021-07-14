﻿using System.Collections;
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
    [SerializeField] WeaponManager weaponManager = null;
    [SerializeField] InputManager inputManager = null;
    [SerializeField] CameraManager cameraManager = null;
    [SerializeField] DialogueManager dialogueManager = null;
    [SerializeField] Inventory inventory = null;

    List<AsyncOperation> sceneLoading = new List<AsyncOperation>();
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

        DontDestroyOnLoad(gameObject);

        /*
        weaponManager = WeaponManager.instance;
        inventory = Inventory.instance;
        cameraManager = CameraManager.instance;
        dialogueManager = DialogueManager.instance;
        inputManager = InputManager.instance;
        */
    }
    private void Start()
    {
        SceneManager.LoadSceneAsync((int)SceneEnum.MainMenu, LoadSceneMode.Additive);
        Invoke("SetActive", 0.2f);
    }
    public IEnumerator GetSceneLoadProgess(bool level, int scene)
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
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(scene));
        totalProgress = 100f;
        bar.GetComponent<RectTransform>().sizeDelta = new Vector2(totalProgress, 100);
        percent.GetComponent<Text>().text = Mathf.RoundToInt(totalProgress) + " %";

        LeanTween.alphaCanvas(loadingPrefab.GetComponent<CanvasGroup>(), 0f, 0.5f).setOnComplete(FinishedLoading);
        EnableManagers(level);
    }
    public IEnumerator GetSceneLoadProgess(bool level, int scene, Vector3 position)
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
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(scene));
        totalProgress = 100f;
        bar.GetComponent<RectTransform>().sizeDelta = new Vector2(totalProgress, 100);
        percent.GetComponent<Text>().text = Mathf.RoundToInt(totalProgress) + " %";

        FindObjectOfType<Player>().transform.position = position;
        FindObjectOfType<Player>().GetCharacterController().enabled = false;
        Invoke("ResetPos", 0.2f);

        LeanTween.alphaCanvas(loadingPrefab.GetComponent<CanvasGroup>(), 0f, 0.5f).setOnComplete(FinishedLoading);
        EnableManagers(level);
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
            inventory.Restart();

            if (UIManager.instance != null)
                UIManager.instance.ResetUI();
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
    public void LoadLevel02()
    {
        LoadScene((int)SceneEnum.Level01, (int)SceneEnum.Level02);
    }
    public void LoadMedRoom()
    {
        LoadScene((int)SceneEnum.Level01, (int)SceneEnum.MedicalRoom);
    }
    public void LoadSecRoom()
    {
        LoadScene((int)SceneEnum.Level01, (int)SceneEnum.SecurityRoom);
    }
    public void LoadMedToLevel01()
    {
        LoadScene((int)SceneEnum.MedicalRoom, (int)SceneEnum.Level01, new Vector3(19, 4, 74));
    }
    public void LoadSecToLevel01()
    {
        LoadScene((int)SceneEnum.SecurityRoom, (int)SceneEnum.Level01, new Vector3(-7, 4, 74));
    }
    void LoadCutscene(int unload, int load)
    {
        loadingPrefab.SetActive(true);

        sceneLoading.Add(SceneManager.UnloadSceneAsync(unload));
        sceneLoading.Add(SceneManager.LoadSceneAsync(load, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgess(false, load));
    }
    void LoadScene(int unload, int load)
    {
        loadingPrefab.SetActive(true);

        sceneLoading.Add(SceneManager.UnloadSceneAsync(unload));
        sceneLoading.Add(SceneManager.LoadSceneAsync(load, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgess(true, load));
    }
    void LoadScene(int unload, int load, Vector3 position)
    {
        loadingPrefab.SetActive(true);

        sceneLoading.Add(SceneManager.UnloadSceneAsync(unload));
        sceneLoading.Add(SceneManager.LoadSceneAsync(load, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgess(true, load, position));
    }
    public void ResetGame()
    {
        //SceneManager.LoadSceneAsync((int)SceneEnum.MainMenu, LoadSceneMode.Additive);
    }
    void ResetPos()
    {
        FindObjectOfType<Player>().GetCharacterController().enabled = true;
    }
    void SetActive()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    }
}
