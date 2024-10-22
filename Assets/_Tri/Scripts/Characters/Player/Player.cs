using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerInputs))]

public class Player : MonoBehaviour
{
    public PlayerInputs Input { get; private set; }

    public Rigidbody Rigidbody { get; private set; }

    private PlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();

        Input = GetComponent<PlayerInputs>();

        movementStateMachine = new PlayerMovementStateMachine(this);
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
