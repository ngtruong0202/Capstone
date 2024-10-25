using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public string enemyName { get; private set; }
    [Header("status")]
    public EnemyRace race;
    public float enemyMaxHp;
    public float enemyCurrentHp;
    public float enemyAtk;
    public float enemyAtkSpeed;
    public float enemyDef;
    public float enemyCriticalRate;
    public float enemyCriticalDmg;

    [Header("State machine")]
    [SerializeField] private EnemyState currentState;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float timeToPatrolling;
    public EnemySpawner spawner;

    private void GetEnemyData()
    {
        var enemyInfo = spawner.enemyDataSO.datas.Find(data => data.race == race);
    }

    private void InitEnemyData()
    {
        int[] multi = new int[5] { 1, 2, 5, 7, 9 };

    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = EnemyState.Idle;
        StateController();
        GetEnemyData();
    }
    private void Update()
    {
        StateController();
    }
    public void DestroyEnemy()
    {
        spawner.RemoveEnemySpawned(this);
        Destroy(gameObject);
    }
    //điều khiển idle state
    private void HandleIdleState()
    {
        //if (Vector3.Distance(spawner.playerPositon, transform.position)
    }
    private IEnumerator TimerIdleState()
    {
        var parentPosition = spawner.transform.position;
        yield return new WaitForSecondsRealtime(timeToPatrolling);
        if(currentState == EnemyState.Idle)
        {
            var randomX = Random.Range(-10, 11);
            var randomZ = Random.Range(-10, 11);
            if(transform.position.x != randomX || transform.position.z != randomZ)
            {
                navMeshAgent.SetDestination(parentPosition + new Vector3(randomX, 0, randomZ));
            }
        }
        yield return null;
    }
    //điều khiển Patrol state
    private void HandlePatrolState()
    {

    }
    //
    private void HandleChaseState()
    {

    }
    private void HandleAttackState()
    {

    }
    private void HandleDeadState()
    {

    }
    private void StateController()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdleState();
                break;
            case EnemyState.Patrol:
                HandlePatrolState();
                break;
            case EnemyState.Chase:
                HandleChaseState();
                break;
            case EnemyState.Attack:
                HandleAttackState();
                break;
            case EnemyState.Dead:
                HandleDeadState();
                break;
        }
    }
    public void ChangeState(EnemyState stateChanged)
    {
        currentState = stateChanged;
        StateController();
    }
}

public enum EnemyRace
{
    None,
    Undead,
    Dragon,
    Devil,
    Wolf
}

public enum EnemyRarity
{
    Normal,
    Elite,
    Rare,
    Epic,
    Legendary
}

public enum EnemyState
{
    Idle,
    Patrol,
    Warning,
    Chase,
    Attack,
    Dead
}