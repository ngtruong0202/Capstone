using Cinemachine;
using System.Threading.Tasks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Truong
{

    public class GameManager : MonoBehaviour
    {

        public static GameManager instance;

        public GameObject playerPrefabs;
        public GameObject cameraPrefab;

        public delegate void OnPlayerSpawned(Player player);
        public static event OnPlayerSpawned PlayerSpawned;

        public GameObject loadingScreen;
        public Slider bar;
        public Image _bar;
        public TextMeshProUGUI barTxt;

        List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

        void Awake()
        {
            instance = this;
            loadingScreen.SetActive(false);

        }

        private void Start()
        {
            LoadGame();
        }

        public async void LoadGame()
        {
            loadingScreen.SetActive(true);

            if (!SceneManager.GetSceneByName("Login").isLoaded)
            {
                scenesLoading.Add(SceneManager.LoadSceneAsync("Login", LoadSceneMode.Additive));
            }

            await GetSceneLoadPropressAsync();
        }

        public async void StartGame()
        {
            loadingScreen.SetActive(true);
            if (SceneManager.GetSceneByName("Login").isLoaded)
            {
                scenesLoading.Add(SceneManager.UnloadSceneAsync("Login"));
            }
            if (!SceneManager.GetSceneByName("MainScene").isLoaded)
            {
                scenesLoading.Add(SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive));
            }

            if (!SceneManager.GetSceneByName("Map").isLoaded)
            {
                scenesLoading.Add(SceneManager.LoadSceneAsync("Map", LoadSceneMode.Additive));
            }
            if (!SceneManager.GetSceneByName("UIScene").isLoaded)
            {
                scenesLoading.Add(SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive));
            }

            await GetSceneLoadPropressAsync();

            SpawnPlayer("UIScene");
        }

        public async void LoadScene(string newScene, string currentScene)
        {
            loadingScreen.SetActive(true);

            if (!SceneManager.GetSceneByName("MainScene").isLoaded)
            {
                scenesLoading.Add(SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive));
            }
            if (!SceneManager.GetSceneByName("UIScene").isLoaded)
            {
                scenesLoading.Add(SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive));
            }
            if (SceneManager.GetSceneByName(currentScene).isLoaded)
            {
                scenesLoading.Add(SceneManager.UnloadSceneAsync(currentScene));
            }

            await GetSceneLoadPropressAsync();
        }

        public async Task GetSceneLoadPropressAsync()
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
                barTxt.text = totaltxt.ToString("0") + "%";

                if (totalProgress >= scenesLoading.Count)
                {
                    loadingScreen.SetActive(false);
                    scenesLoading.Clear();
                }

                await Task.Yield();
            }
        }

        private void SpawnPlayer(string scene)
        {
            Vector3 pos = new Vector3(0, 1, 35);

            var currentPlayer = Instantiate(playerPrefabs, pos, Quaternion.identity);
            var currentCamera = Instantiate(cameraPrefab, pos, Quaternion.identity);

            Player player = currentPlayer.GetComponent<Player>(); 

            Scene mapScene = SceneManager.GetSceneByName(scene);

            SceneManager.MoveGameObjectToScene(currentPlayer, mapScene);
            SceneManager.MoveGameObjectToScene(currentCamera, mapScene);

            PlayerSpawned?.Invoke(player);
        }
    }
}
