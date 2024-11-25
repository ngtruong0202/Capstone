using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance;

        Player Player;

        public Dictionary<string, Queue<GameObject>> poolDictionary;

        void Awake()
        {
            Instance = this;
        }
        private void OnEnable()
        {
            GameManager.PlayerSpawned += GetOnPlayer;
        }

        private void OnDisable()
        {
            GameManager.PlayerSpawned -= GetOnPlayer;
        }

        private void GetOnPlayer(Player player)
        {
            Player = player;

            Starts();
        }

        void Starts()
        {
            
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in Player.Data.pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab, Player.transform);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject GetPooledObject(string tag)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.Log("Pool with tag " + tag + " doesn't exist.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
    }

}
