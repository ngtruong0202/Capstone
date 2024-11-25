using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Truong
{


    public class Teleporter : MonoBehaviour
    {
        public Object destinationScene;
        public string destSpawnName;

        private SpawnPoint cachedSpawnPoint;
        private Teleportable _teleportable;


        void OnTriggerEnter(Collider collider)
        {
            Teleportable teleportable = collider.GetComponent<Teleportable>();
            if (teleportable != null)
            {
                OnEnter(teleportable);
            }
        }

        public void OnEnter(Teleportable teleportable)
        {
            if (!teleportable.canTeleport)
            {
                return;
            }

            teleportable.canTeleport = false;

            if (_teleportable == null)
            {
                _teleportable = teleportable.GetComponent<Teleportable>();
            }

            if (SceneManager.GetActiveScene().name == destinationScene.name)
            {
                Teleport(teleportable);
            }
            else
            {
                StartCoroutine(TeleportToNewScene(destinationScene.name, teleportable));
            }
        }

        private IEnumerator TeleportToNewScene(string sceneName, Teleportable teleportable)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            AsyncOperation newSceneAsyncLoad = SceneManager.LoadSceneAsync(destinationScene.name, LoadSceneMode.Additive);

            while (!newSceneAsyncLoad.isDone)
            {
                yield return null;
            }

            Scene otherScene = GetSceneActive();
            GameManager.instance.LoadScene(sceneName, otherScene.name);

            Scene newScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.MoveGameObjectToScene(teleportable.gameObject, newScene);

            // Lấy sẵn SpawnPoint trong scene mới để không phải tìm kiếm mỗi lần
            cachedSpawnPoint = GetSpawnPointInScene(newScene, destSpawnName);
            Teleport(teleportable);

            //SceneManager.UnloadSceneAsync(currentScene);
            yield return null;

        }

        private void Teleport(Teleportable teleportable)
        {
            if (cachedSpawnPoint != null && _teleportable != null)
            {
                _teleportable.TeleportTo(cachedSpawnPoint.transform);
            }
            teleportable.canTeleport = true;
        }

        // Tìm SpawnPoint trong scene mới chỉ một lần
        private SpawnPoint GetSpawnPointInScene(Scene scene, string spawnName)
        {
            // Duyệt tất cả các root objects trong scene để tìm spawn point
            GameObject[] rootObjects = scene.GetRootGameObjects();
            foreach (GameObject obj in rootObjects)
            {
                SpawnPoint spawnPoint = obj.GetComponent<SpawnPoint>();
                if (spawnPoint != null && spawnPoint.spawnName == spawnName)
                {
                    return spawnPoint;
                }
            }
            return null;
        }

        private Scene GetSceneActive()
        {
            int sceneCount = SceneManager.sceneCount;
            for (int i = 0; i < sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name != "MainScene")
                    return scene;
            }
            return GetSceneActive();
        }
    }

}