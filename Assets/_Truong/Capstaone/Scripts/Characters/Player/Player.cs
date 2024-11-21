using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Truong
{
    [RequireComponent(typeof(PlayerInputs))]
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public PlayerSO Data { get; private set; }   
        [field: SerializeField] public PlayerAnimation PlayerAnimation { get; private set; }
        [field: SerializeField] public PlayerSkillManager SkillManager { get; private set; }

        public EnemyDetection EnemyDetection { get; private set; }
        public PlayerInputs Inputs { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public NavMeshAgent Agent { get; private set; }
        

        public Transform MainCameraTransform { get; private set; }

        public PlayerMovementStateMachine movementStateMachine;
        public PlayerAttackStateMachine attackStateMachine;

        [Header("Attack")]
        [field: SerializeField] public Transform attack4Pos;

        private void Awake()
        {
            Inputs = GetComponent<PlayerInputs>();
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();
            Agent = GetComponent<NavMeshAgent>();
            EnemyDetection = GetComponent<EnemyDetection>();
            SkillManager = GetComponent<PlayerSkillManager>();

            PlayerAnimation.Initilize();
            movementStateMachine = new PlayerMovementStateMachine(this);
            attackStateMachine = new PlayerAttackStateMachine(this);

            MainCameraTransform = Camera.main.transform;
        }

        private void Start()
        {
            CancelAttack();
            
        }

        private void Update()
        {
            movementStateMachine.HandleInput();
            movementStateMachine.UpDate();

            attackStateMachine.HandleInput();
            attackStateMachine.UpDate();

            if(Input.GetKeyDown(KeyCode.F))
            {
                
            }
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicUpdate();
            attackStateMachine.PhysicUpdate();
        }

        public void CancelAttack()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdlingState);
            attackStateMachine.ChangeState(attackStateMachine.UnAttack);
        }

        public void Attack()
        {
            if (EnemyDetection.currentTarget == null)
            {
                if (EnemyDetection.detecedEnemies.Count == 0)
                {
                    attackStateMachine.ChangeState(attackStateMachine.BasicAttackState);
                }

                else
                {
                    EnemyDetection.GetClosestEnemy();
                    StartCoroutine(attackStateMachine.BasicAttackState.EnemyDistance());
                }

            }
            else
            {
                float enemyDistance = Vector3.Distance(Agent.transform.position,
                    EnemyDetection.currentTarget.transform.position);

                if (enemyDistance <= Agent.stoppingDistance)
                {
                    Agent.updateRotation = false;

                    attackStateMachine.BasicAttackState.RotateTowardsTarget(EnemyDetection.currentTarget.position);

                    Agent.updateRotation = true;

                    attackStateMachine.ChangeState(attackStateMachine.BasicAttackState);
                }
                else
                {
                    StartCoroutine(attackStateMachine.BasicAttackState.EnemyDistance());
                }
            }
        }

        public void Attack4()
        {
            attackStateMachine.PlayerAttackCombo4.Attack4(attack4Pos);
        }

        

        public void Test()
        {
            GameObject obj = PoolManager.Instance.GetPooledObject("Attack4");
            if (obj != null)
            {
                Vector3 pos = new Vector3(transform.position.x,
                    transform.position.y + 1, transform.position.z);

                obj.transform.position = pos;
                obj.transform.rotation = transform.rotation;
                obj.SetActive(true);
            }
        }

    }

}
