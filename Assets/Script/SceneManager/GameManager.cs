using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject playerPrefabs;

    public GameObject loadingScreen;
    public Slider bar;
    public Image _bar;
    public TextMeshProUGUI barTxt;

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    void Awake()
    {
        instance = this;
        loadingScreen.gameObject.SetActive(false);

    }

    private void Start()
    {
        LoadGame();
    }

    public void LoadGame()
    {
        loadingScreen.gameObject.SetActive(true);

        if (!SceneManager.GetSceneByName("Login").isLoaded)
        {
            scenesLoading.Add(SceneManager.LoadSceneAsync("Login", LoadSceneMode.Additive));
        }
        
        StartCoroutine(GetSceneLoadPropress());
    }

    public void StartGame(string scene)
    {
        loadingScreen.gameObject.SetActive(true);
        if (SceneManager.GetSceneByName("Login").isLoaded)
        {
            scenesLoading.Add(SceneManager.UnloadSceneAsync("Login"));
        }
        if (!SceneManager.GetSceneByName("MainScene").isLoaded)
        {
            scenesLoading.Add(SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive));
        }

        if (!SceneManager.GetSceneByName(scene).isLoaded)
        {
            scenesLoading.Add(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive));
        }

        StartCoroutine(GetSceneLoadPropress());

        Vector3 vector3 = new Vector3(-2, -12, 35);

        Instantiate(playerPrefabs, vector3, Quaternion.identity);
    }

    public void LoadScene(string newScene, string currentScene)
    {
        loadingScreen.gameObject.SetActive(true);

        if (!SceneManager.GetSceneByName("MainScene").isLoaded)
        {
            scenesLoading.Add(SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive));
        }

        //if (!SceneManager.GetSceneByName(newScene).isLoaded)
        //{
        //    scenesLoading.Add(SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive));
        //}

        if (SceneManager.GetSceneByName(currentScene).isLoaded)
        {
            scenesLoading.Add(SceneManager.UnloadSceneAsync(currentScene));
        }

        StartCoroutine(GetSceneLoadPropress());
    }

    public IEnumerator GetSceneLoadPropress()
    {
        float totalProgress = 0f;
        while (scenesLoading.Count > 0)
        {
            totalProgress = 0f;

            foreach (AsyncOperation operation in scenesLoading)
            {
                totalProgress += operation.progress;
            }

            float progressPercentage = totalProgress / scenesLoading.Count;
            _bar.fillAmount = progressPercentage;

            float totaltxt = progressPercentage * 100;
            barTxt.text = totaltxt.ToString("0");

            if (totalProgress >= scenesLoading.Count)
            {
                loadingScreen.SetActive(false);
                scenesLoading.Clear();
            }

            yield return null;
        }
    }
}
