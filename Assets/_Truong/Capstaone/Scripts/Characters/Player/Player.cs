using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Truong
{
    [RequireComponent(typeof(PlayerInputs))]
    public class Player : MonoBehaviour
    {

        [field: SerializeField] public PlayerSO Data { get; private set; }
        [field: SerializeField] public PlayerAnimation PlayerAnimation { get; private set; }

        public PlayerInputs Inputs { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }

        public Transform MainCameraTransform { get; private set; }

        private PlayerMovementStateMachine movementStateMachine;

        private void Awake()
        {
            Inputs = GetComponent<PlayerInputs>();
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();

            PlayerAnimation.Initilize();
            movementStateMachine = new PlayerMovementStateMachine(this);

            MainCameraTransform = Camera.main.transform;
        }

        private void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdlingState);
        }

        private void Update()
        {
            movementStateMachine.HandleInput();
            movementStateMachine.UpDate();
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicUpdate();
        }
    }

}
