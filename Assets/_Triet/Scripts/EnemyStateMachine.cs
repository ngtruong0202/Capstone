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
    [SerializeField] float timeChangePatrol;
    [SerializeField] bool havePatrolPoint;
    [SerializeField] Vector3 patrolPoint;
    Vector3 enemyDirection;
    private void Start()
    {
        currentState = EnemyState.Idle;
        timeChangePatrol = 50f;
    }
    private void FixedUpdate()
    {
        StateController();
    }
    public void DestroyEnemy()
    {
        spawner.RemoveEnemySpawned(this);
        Destroy(gameObject);
    }

    private bool ShouldStartPatrolling()
    {
        idleTimer += Time.deltaTime;
        return idleTimer >= timeChangePatrol;
    }
    private void LoadAnim(string name, float value)
    {
        animator.SetFloat(name, value);
    }
    private void LoadAnim(string name, bool value)
    {
        animator.SetBool(name, value);
    }
    // kiểm tra khoảng cách với người chơi
    private float CheckDistanceToTarget(Vector3 target)
    {
        return Vector3.Distance(target, transform.position);
    }
    // xoay enemy về hướng player
    private void RotateToTarget(Vector3 target)
    {
        //tính toán hướng
        enemyDirection = target - transform.position;
        // xoay enemy
        Quaternion rotation = Quaternion.LookRotation(enemyDirection);
        //Thực hiện xoay dần dần để tạo cảm giác mượt mà
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    #region Enemy State Logic
    //điều khiển idle state
    private void IdleState()
    {
        if (CheckDistanceToTarget(spawner.playerPosition.position) <= 5)
        {
            Debug.Log("Change warning state");
            LoadAnim("isWarning", true);
            ChangeState(EnemyState.Warning);
        }
        if (!ShouldStartPatrolling())
        {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            if (currentState.normalizedTime >= 1 && !animator.IsInTransition(0))
            {
                string name = animator.GetFloat("IdleState") == 1 ? "IdleBreathe" : "IdleLookAround";
                animator.Play(name, 0, 0f);
                var random = Random.Range(1, 3);
                LoadAnim("IdleState", random);
            }
        }
        else
        {
            Debug.Log("Change patrolling state");
            idleTimer = 0;
            ChangeState(EnemyState.Patrol);
        }
    }
    //điều khiển warning state
    private void WarningState()
    {
        RotateToTarget(spawner.playerPosition.position);
        if (CheckDistanceToTarget(spawner.playerPosition.position) <= 3f)
        {
            Debug.Log("Change chase state");
            ChangeState(EnemyState.Chase);
        }
        else if (CheckDistanceToTarget(spawner.playerPosition.position) > 5)
        {
            Debug.Log("Change idle state");
            LoadAnim("IdleState", Random.Range(1, 3));
            LoadAnim("isWarning", false);
            ChangeState(EnemyState.Idle);
        }
    }
    //điều khiển Patrol state
    private void PatrolState()
    {
        if (!havePatrolPoint)
        {
            patrolPoint = new Vector3(Random.Range(-30f, 30f), 0, Random.Range(-30f, 30f));
            havePatrolPoint = true;
        }
        else
        {
            RotateToTarget(patrolPoint);
            Debug.Log(enemyDirection.normalized * 0.1f * Time.deltaTime);
            transform.position += enemyDirection.normalized * 1f * Time.deltaTime;
            if (CheckDistanceToTarget(patrolPoint) <= 0.1f)
            {
                havePatrolPoint = false;
                return;
            }
        }
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
    #endregion

    private void StateController()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleState();
                break;
            case EnemyState.Warning:
                WarningState();
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