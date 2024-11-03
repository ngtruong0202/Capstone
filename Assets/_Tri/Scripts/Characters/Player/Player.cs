using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerInputs))]

public class Player : MonoBehaviour
{
    [field: Header("Referrence")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Colllisions")]
    [field: SerializeField] public CapsuleColliderUtility ColliderUtility { get; private set; }
    [field: SerializeField] public PlayerLayerData LayerData { get; private set; }

    public PlayerInputs Input { get; private set; }

    public Rigidbody Rigidbody { get; private set; }

    public Transform MainCameraTransform { get; private set; }

    private PlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();

        Input = GetComponent<PlayerInputs>();

        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimensions();

        MainCameraTransform = Camera.main.transform;

        movementStateMachine = new PlayerMovementStateMachine(this);
    }

    private void OnValidate()
    {
        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimensions();
    }

    // Start is called before the first frame update
    private void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.IdlingState);
    }

    // Update is called once per frame
    private void Update()
    {
        movementStateMachine.HandleInput();

        movementStateMachine.Update();
    }
     
    private void FixedUpdate()
    {
        movementStateMachine.PhysicsUpdate();
    }
}
