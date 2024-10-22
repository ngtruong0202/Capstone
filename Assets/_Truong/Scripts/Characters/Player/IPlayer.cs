using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayer : MonoBehaviour
{
    private IPlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
        movementStateMachine = new IPlayerMovementStateMachine();
    }

    private void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.idleingState);
    }

    private void Update()
    {
        movementStateMachine.HandleInput();
        movementStateMachine.Update();
    }

    private void FixedUpdate()
    {
        movementStateMachine.PhysicUpsdate();
    }
}
