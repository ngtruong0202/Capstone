using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;

    protected Vector2 movementInput;
    protected float baseSpeed = 5f;
    protected float speedModifier = 1f;


    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
        stateMachine = playerMovementStateMachine;
    }

    #region IState Method
    public virtual void Enter()
    {
        Debug.Log("State: " + GetType().Name);
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void Update()
    {
    }
    
    public virtual void PhysicsUpdate()
    {
        Move();
    }
    #endregion

    #region Main Method
    private void ReadMovementInput()
    {
        movementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        if (movementInput == Vector2.zero || speedModifier == 0f)
        {
            return;
        }

        Vector3 movementDirection = GetMovementInputDirection();

        float movementSpeed = GetMovementSpeed();

        Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

        stateMachine.Player.Rigidbody.AddForce( movementSpeed * 0.5f * movementDirection - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
    }
    #endregion

    #region Reusable Methods
    protected Vector3 GetMovementInputDirection()
    {
        return new Vector3(movementInput.x, 0f, movementInput.y);
    }

    protected float GetMovementSpeed()
    {
        return baseSpeed * speedModifier;
    }

    protected Vector3 GetPlayerHorizontalVelocity()
    {
        Vector3 playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;

        playerHorizontalVelocity.y = 0f;

        return playerHorizontalVelocity;
    }
    #endregion
}
