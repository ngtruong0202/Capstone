using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    [Header("State machine")]
    [SerializeField] private EnemyState currentState;
    public EnemySpawner spawner;
    [SerializeField] EnemyInfomation enemyInfomation;
    [SerializeField] EnemyBehaviourTree enemyBehaviour;
    public Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [Header("Time")]
    [SerializeField] float timerIdle;
    [SerializeField] float timerPatrol;
    [SerializeField] float timeChangePatrol;
    [SerializeField] private float patrollingTime;
    [Header("Patrolling")]
    [SerializeField] bool havePatrolPoint;
    [SerializeField] Vector3 patrolPoint;
    [SerializeField] Vector3 enemyDirection;
    [SerializeField] Vector3 spawnPoint;
    [SerializeField] float maxDistance;
    [Header("Attack")]
    public bool isDead;
    private void Start()
    {
        currentState = EnemyState.Idle;
        spawnPoint = transform.position;
    }
    private void FixedUpdate()
    {
        if (isDead)
        {
            ChangeState(EnemyState.Dead);
        }
        StateController();
    }
    public void DestroyEnemy()
    {
        spawner.RemoveEnemySpawned(this);
        Destroy(gameObject);
    }
    private void ResetTimer()
    {
        timerIdle = 0;
        timerPatrol = 0;
    }
    private bool ShouldChangeState(float timer, float timeChange)
    {
        return timer >= timeChange;
    }
    private void LoadAnim(string name, float value)
    {
        animator.SetFloat(name, value);
    }
    private void LoadAnim(string name, bool value)
    {
        animator.SetBool(name, value);
    }
    // kiểm tra khoảng cách với mục tiêu
    private float CheckDistanceToTarget(Vector3 target)
    {
        return Vector3.Distance(target, transform.position);
    }
    // kiểm tra player vào vùng nào (warning, chase, atk,...)
    private bool PlayerEnterArea(float area)
    {
        return CheckDistanceToTarget(spawner.playerPosition.position) <= area;
    }
    // xoay enemy về hướng player
    private void RotateToTarget(Vector3 target)
    {
        //tính toán hướng
        enemyDirection = target - transform.position;
        // xoay enemy
        Quaternion rotation = Quaternion.LookRotation(enemyDirection);
        // Giữ nguyên góc xoay trên trục x nếu không phải đơn vị bay
        if (!enemyInfomation.FlyingUnit)
        {
            rotation = Quaternion.Euler(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
        }
        //Thực hiện xoay dần dần để tạo cảm giác mượt mà
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }
    private void MoveToTarget(float speed, Vector3 target)
    {
        agent.speed = speed;
        Vector3 targetPosition = target; // Vị trí hợp lệ trên NavMesh
        //Debug.Log("enemyInfomation.FlyingUnit " + enemyInfomation.FlyingUnit);
        if (!enemyInfomation.FlyingUnit)
        {
            targetPosition.y = transform.position.y; // Giữ nguyên y nếu không phải là đơn vị bay
        }
        agent.SetDestination(targetPosition); // Đặt mục tiêu
    }

    #region Enemy State Logic
    //điều khiển idle state
    #region Idle
    private void IdleState()
    {
        if (PlayerEnterArea(enemyInfomation.warningArea))
        {
            Debug.Log("Change warning state");
            LoadAnim("isWarning", true);
            ChangeState(EnemyState.Warning);
        }
        else
        {
            IdleCoroutine();
        }
    }
    // vòng lặp chính của idle state
    private void IdleCoroutine()
    {
        timerIdle += Time.deltaTime;
        if (!ShouldChangeState(timerIdle, timeChangePatrol))
        {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            if (currentState.normalizedTime >= 1 && !animator.IsInTransition(0))
            {
                LoadAnim("IdleState", 1);
            }
        }
        else
        {
            Debug.Log("Change patrolling state");
            LoadAnim("isMove", true);
            ResetTimer();
            ChangeState(EnemyState.Patrol);
        }
    }
    #endregion
    //điều khiển warning state
    #region Warning
    private void WarningState()
    {
        RotateToTarget(spawner.playerPosition.position);
        // nếu player tiến vào vùng truy đuổi
        if (PlayerEnterArea(enemyInfomation.chaseArea))
        {
            agent.Resume();
            Debug.Log("Change chase state");
            LoadAnim("isMove", true);
            LoadAnim("isWarning", false);
            ChangeState(EnemyState.Chase);
        }
        //nếu player ra khỏi vùng cảnh giác
        if (!PlayerEnterArea(enemyInfomation.warningArea))
        {
            agent.Resume();
            Debug.Log("Change idle state");
            LoadAnim("IdleState", 1);
            LoadAnim("isMove", false);
            LoadAnim("isWarning", false);
            ChangeState(EnemyState.Idle);
        }
    }
    #endregion

    //điều khiển Patrol state
    #region patrolling
    private void PatrolState()
    {
        if (PlayerEnterArea(enemyInfomation.warningArea))
        {
            Debug.Log("Change warning state");
            LoadAnim("isWarning", true);
            LoadAnim("isMove", false);
            ChangeState(EnemyState.Warning);
        }
        else
        {
            EnemyPatrolling();
        }

    }
    // vòng lặp của patrolling
    private void EnemyPatrolling()
    {
        timerPatrol += Time.deltaTime;
        if (!ShouldChangeState(timerPatrol, patrollingTime))
        {
            if (!havePatrolPoint)
            {
                RandomPatrolPpoint();
                havePatrolPoint = true;
            }
            else
            {
                MoveToTarget(enemyInfomation.EnemySpeed, patrolPoint);
                if (CheckDistanceToTarget(patrolPoint) <= 1f)
                {
                    havePatrolPoint = false;
                    return;
                }
                var moveX = enemyDirection.x >= 0 ? 1 : -1; // vì patrolling (tuần tra) nên sẽ đi bộ nên set = 1 và -1
                var moveZ = enemyDirection.z >= 0 ? 1 : -1;
                LoadAnim("MoveX", moveX);
                LoadAnim("MoveZ", moveZ);
            }
        }
        else
        {
            agent.ResetPath();
            ResetTimer();
            LoadAnim("IdleState", 1);
            LoadAnim("isMove", false);
            ChangeState(EnemyState.Idle);
        }
    }
    Vector3 RandomPatrolPpoint()
    {
        do
        {
            float randomX = Random.Range(-maxDistance, maxDistance);
            float randomZ = Random.Range(-maxDistance, maxDistance);
            patrolPoint = new Vector3(randomX, 0, randomZ) + spawnPoint;
        }
        while (CheckDistanceToTarget(patrolPoint) >= maxDistance);
        return patrolPoint;

    }
    #endregion
    //
    #region chase
    private void ChaseState()
    {
        // chạy quá xa hoặc khoảng cách giữa 2 bên ngày càng xa thì phải đổi state
        if (CheckDistanceToTarget(spawnPoint) >= maxDistance)
        {
            Debug.Log("max Distance Change back spawn point state");
            ChangeState(EnemyState.BackSpawnPoint);
            return;
        }
        // nếu vào đúng khoảng cách tấn công thì đổi state
        if (PlayerEnterArea(enemyInfomation.AtkRange))
        {
            agent.ResetPath();
            ChangeState(EnemyState.Attack);
        }
        else
        {
            MoveToTarget(enemyInfomation.EnemySpeed * 2, spawner.playerPosition.position);
            var moveX = enemyDirection.x >= 0 ? 2 : -2; // vì chase (truy duổi) nên sẽ chạy nên set = 2 và -2
            var moveZ = enemyDirection.z >= 0 ? 2 : -2;
            LoadAnim("MoveX", moveX);
            LoadAnim("MoveZ", moveZ);
        }
    }
    private void BackSpawnPointState()
    {
        if (CheckDistanceToTarget(spawnPoint) <= 0.1f)
        {
            ResetTimer();
            ChangeState(EnemyState.Patrol);
        }
        MoveToTarget(enemyInfomation.EnemySpeed, spawnPoint);
        var moveX = enemyDirection.x >= 0 ? 1 : -1; // vì patrolling (tuần tra) nên sẽ đi bộ nên set = 1 và -1
        var moveZ = enemyDirection.z >= 0 ? 1 : -1;
        LoadAnim("MoveX", moveX);
        LoadAnim("MoveZ", moveZ);

        if (PlayerEnterArea(enemyInfomation.warningArea))
        {
            Vector3 enemyDirection = spawnPoint - transform.position;
            float angle = Vector3.Angle(transform.forward, enemyDirection);
            if (angle < 10f)
            {
                agent.ResetPath();
                Debug.Log("Change warning state");
                LoadAnim("isWarning", true);
                LoadAnim("isMove", false);
                ChangeState(EnemyState.Warning);
            }
        }
    }
    #endregion
    private void AttackState()
    {
        enemyBehaviour.CountdownTimer();
        if (!PlayerEnterArea(enemyInfomation.AtkRange))
        {
            enemyBehaviour.ExitAttackState();
            ChangeState(EnemyState.Chase);
            return;
        }
        Vector3 enemyDirection = spawner.playerPosition.position - transform.position;
        float angle = Vector3.Angle(transform.forward, enemyDirection);
        // lưu ý angle
        if(angle > 20f)
        {
            RotateToTarget(spawner.playerPosition.position);
        }
        if (!enemyBehaviour.CanAttack(enemyInfomation.EnemyAtkCd) || !enemyBehaviour.CompletedPreAtk()) return;
        enemyBehaviour.EnemyAttack();
    }
    private void RetreatState()
    {

    }
    private void DeadState()
    {
        if (isDead)
        {
            isDead = false;
            animator.enabled = false;
            animator.enabled = true;
            animator.Play("Death", 0, 0f);
            StartCoroutine(WaitingDeadEnd());
        }
    }
    IEnumerator WaitingDeadEnd()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 3f);
        DestroyEnemy();
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
            case EnemyState.BackSpawnPoint:
                BackSpawnPointState();
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
            case EnemyState.Retreat:
                RetreatState();
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
    BackSpawnPoint,
    Chase,
    Attack,
    Retreat,
    Dead
}