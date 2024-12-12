using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyDataSO enemyDataSO;
    public Transform playerPosition;
    [SerializeField] private EnemyRace enemyWantToSpawn;
    public List<EnemyStateMachine> enemies = new List<EnemyStateMachine>();
    [SerializeField] private bool isSpawned; // true đã spawn, false chưa spawn
    [SerializeField] private bool isInSpawner;
    public EnemyData enemyData;
    [SerializeField] private float timeRespawn;
    [SerializeField] private int countEnemySpawned;
    [SerializeField] private int enemyRemoved;
    public int enemyDomainX;
    public int enemyDomainZ;

    private void Awake()
    {
        enemyData = enemyDataSO.datas.Find(e => e.race == enemyWantToSpawn);
    }
    private void Start()
    {
        enemyRemoved = 0;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public int GetEnemyComboList()
    {
        return enemyData.amountComboList;
    }

    public int GetEnemyWaitingComboList()
    {
        return enemyData.amoutWaitingCombolist;
    }

    public void CheckAndSpawn()
    {
        if (!isSpawned)
        {
            isSpawned = true; // Đánh dấu là quái đã được spawn
            StartCoroutine(SpawnEnemies());
        }
        else if(isSpawned && enemies.Count == 0)
        {
            StartCoroutine(SpawnEnemiesAfterDelay());
        }
    }
    IEnumerator SpawnEnemiesAfterDelay()
    {
        yield return new WaitForSeconds(timeRespawn); // Đợi 1 khoảng thời gian

        if (enemies.Count == 0 && isInSpawner) // Kiểm tra lại nếu số lượng quái bằng 0 và trong vùng spawn
        {
            StartCoroutine(SpawnEnemies()); // Spawn lại quái
        }
    }
    private IEnumerator SpawnEnemies()
    {
        countEnemySpawned = Random.Range(1, enemyData.maxSpawnAmount + 1);
        for (int i = 0; i< countEnemySpawned; i++)
        {
            var enemy = Instantiate(enemyData.spawnPrefab[Random.Range(0, enemyData.spawnPrefab.Count)],
                new Vector3(transform.position.x + (countEnemySpawned / 2) + (i + 1), 0, transform.position.z + Random.Range(-i, i + 1)),
                Quaternion.identity, transform);
            enemy.GetComponent<EnemyStateMachine>().spawner = this;
            enemies.Add(enemy.GetComponent<EnemyStateMachine>());
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void RemoveEnemySpawned(EnemyStateMachine enemy)
    {
        enemies.Remove(enemy);
        enemyRemoved += 1;
        if(enemyRemoved >= countEnemySpawned)
        {
            enemyRemoved = 0;
            CheckAndSpawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInSpawner = true;
            CheckAndSpawn();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (enemies.Count != 0)
            {
                for(int i = enemies.Count -1; i >= 0; i--)
                {
                    enemies[i].DestroyEnemy();
                }
            }
            isInSpawner = false;
        }
    }
}
