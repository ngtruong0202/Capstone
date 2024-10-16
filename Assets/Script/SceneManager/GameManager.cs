using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static GameManager instance;
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

        //scenesLoading.Add(SceneManager.UnloadSceneAsync(0));
        scenesLoading.Add(SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadPropress());
    }

    public void LoadScene(int scene1, int scene2)
    {
        loadingScreen.gameObject.SetActive(true);
        if (SceneManager.GetSceneByName("Login").isLoaded)
        {
            scenesLoading.Add(SceneManager.UnloadSceneAsync("Login"));
        }
        scenesLoading.Add(SceneManager.LoadSceneAsync(scene1, LoadSceneMode.Additive));
        scenesLoading.Add(SceneManager.LoadSceneAsync(scene2, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadPropress());

    }

    public IEnumerator GetSceneLoadPropress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                float totalSceneProgress = 0;
                foreach (AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }
                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100;

                bar.value = (int)totalSceneProgress;
                //_bar.fillAmount = totalSceneProgress;

                barTxt.text = totalSceneProgress.ToString("0");
                yield return null;
            }
        }

        loadingScreen.gameObject.SetActive(false);

    }
}
