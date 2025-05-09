using _Tin.Scripts.Characters.Player.Data.Layers;
using _Tin.Scripts.Characters.Player.Data.ScriptableObjects;
using _Tin.Scripts.Characters.Player.StateMachine.Movement;
using _Tin.Scripts.Characters.Player.Utilities.Colliders;
using _Tin.Scripts.Characters.Player.Utilities.Input;
using UnityEngine;

namespace _Tin.Scripts.Characters.Player
{
    [RequireComponent(typeof(PlayerArcherInputs))]
    public class PlayerArcher : MonoBehaviour
    {
        [field: Header("References")]
        [field: SerializeField] public PlayerArcherSo Data { get; private set; }
        
        [field: Header("Collision")]
        [field: SerializeField] public PlayerArcherCapsuleColliderUtility ArcherCapsuleColliderUtility { get; private set; }
        [field: SerializeField] public PlayerArcherLayerData ArcherLayerData { get; private set; }
        
        public Rigidbody Rigidbody { get; private set; }
        public Transform MainCameraTransform { get; private set; }
        public PlayerArcherInputs ArcherInput { get; private set; }
        // This will present the player's state machine
        private PlayerArcherStateMachine ArcherStateMachine { get; set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            ArcherInput = gameObject.AddComponent<PlayerArcherInputs>();
            
            if (Camera.main != null) 
                MainCameraTransform = Camera.main.transform;
            
            ArcherCapsuleColliderUtility.Initialize(gameObject);
            ArcherCapsuleColliderUtility.CalculateCapsuleColliderDimensions();
            
            ArcherStateMachine = new PlayerArcherStateMachine(this);
        }

        private void OnValidate()
        {
            ArcherCapsuleColliderUtility.Initialize(gameObject);
            ArcherCapsuleColliderUtility.CalculateCapsuleColliderDimensions();
        }

        // After the player state machine is created, we will change the state to the idling state by default
        private void Start() => ArcherStateMachine.ChangeState(ArcherStateMachine.ArcherIdlingState);

        private void Update()
        {
            ArcherStateMachine.HandleInput();
            ArcherStateMachine.Update();
        }

        private void FixedUpdate() => ArcherStateMachine.PhysicsUpdate();
    }
}