using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    [Header("State machine")]
    [SerializeField] private EnemyState currentState;
    [SerializeField] private float timeToPatrolling;
    public EnemySpawner spawner;
    [SerializeField] private Animator animator;
    [SerializeField] float idleTimer;

    private void Start()
    {
        currentState = EnemyState.Idle;
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
    private void IdleState()
    {
        if(idleTimer >= 50f || Vector3.Distance(spawner.playerPosition.position, transform.position) <= 3)
        {
            idleTimer = 0;
            ChangeState(EnemyState.Patrol);
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            if(currentState.normalizedTime >= 1 && !animator.IsInTransition(0))
            {
                LoadAnimIdleState(Random.Range(1, 3));
            }
            Debug.Log("Change patrol state");
        }
        if(Vector3.Distance(spawner.playerPosition.position, transform.position) <= 3)
        {
            LoadAnimIdleState(0);
        }
        idleTimer += Time.deltaTime;
    }
    private void LoadAnimIdleState(float value)
    {
        animator.SetFloat("IdleState", value);
    }
    //điều khiển Patrol state
    private void PatrolState()
    {

    }
    //
    private void ChaseState()
    {

    }
    private void AttackState()
    {

    }
    private void DeadState()
    {

    }
    private void StateController()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleState();
                break;
            case EnemyState.Patrol:
                PatrolState();
                break;
            case EnemyState.Chase:
                ChaseState();
                break;
            case EnemyState.Attack:
                AttackState();
                break;
            case EnemyState.Dead:
                DeadState();
                break;
        }
    }
    public void ChangeState(EnemyState stateChanged)
    {
        currentState = stateChanged;
    }
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