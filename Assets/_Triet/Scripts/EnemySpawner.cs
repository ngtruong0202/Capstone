using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnerManager manager;
    public EnemyDataSO enemyDataSO;
    [SerializeField] private EnemyRace enemyWantToSpawn;
    public List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private bool isSpawned; // true đã spawn, false chưa spawn
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private float timeRespawn;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(manager.playerPositon.position, transform.position) < 20f)
        {
            CheckAndSpawn();
        }
        else if (Vector3.Distance(manager.playerPositon.position, transform.position) > 100f)
        {
            if (enemies.Count != 0)
            {
                foreach (var item in enemies)
                    item.DestroyEnemy();
            }
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        enemyData = enemyDataSO.datas.Find(e => e.race == enemyWantToSpawn);
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

        if (enemies.Count == 0) // Kiểm tra lại nếu số lượng quái bằng 0
        {
            StartCoroutine(SpawnEnemies()); // Spawn lại quái
        }
    }
    private IEnumerator SpawnEnemies()
    {
        var randomAmount = Random.Range(1, enemyData.maxSpawnAmount + 1);
        for(int i = 0; i< randomAmount; i++)
        {
            var enemy = Instantiate(enemyData.spawnPrefab[Random.Range(0, enemyData.spawnPrefab.Count)],
                transform.position + new Vector3(transform.position.x + Random.Range(-i, i + 1), 0, transform.position.z + Random.Range(-i, i + 1)),
                Quaternion.identity, transform);
            enemy.GetComponent<Enemy>().manager = this;
            enemies.Add(enemy.GetComponent<Enemy>());
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void RemoveEnemySpawned(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

}
