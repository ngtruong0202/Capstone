using HutongGames.PlayMaker.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashingState : PlayerGroundedState
{
    private PlayerDashData dashData;

    private float startTime;

    private int consecutiveDashesUsed;

    private bool shouldKeepRotating;

    public PlayerDashingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        dashData = movementData.DashData;
    }

    #region IState Methods
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = dashData.SpeedModifier;

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.DashParameterHash);

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StrongForce;

        stateMachine.ReusableData.RotationData = dashData.RotationData;

        AddForceOnTransitionFromStationaryState();

        shouldKeepRotating = stateMachine.ReusableData.MovementInput != Vector2.zero;

        UpdateConsecutiveDashes();

        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.DashParameterHash);

        SetBaseRotationData();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!shouldKeepRotating)
        {
            return;
        }

        RotateTowardsTargetRotation();
    }

    public override void OnAnimationTransitionEvent()
    {
        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.HardStoppingState); 
            return;
        }

        stateMachine.ChangeState(stateMachine.SprintingState);
    }
    #endregion

    #region Main Methods
    private void AddForceOnTransitionFromStationaryState()
    {
        Vector3 dashDirection = stateMachine.Player.transform.forward;

        dashDirection.y = 0f;

        UpdateTargetRotation(dashDirection, false);

        if (stateMachine.ReusableData.MovementInput != Vector2.zero)
        {
            UpdateTargetRotation(GetMovementInputDirection());

            dashDirection = GetTargetRotationDirection(stateMachine.ReusableData.CurrentTargetRotation.y);
        }

        stateMachine.Player.Rigidbody.velocity = dashDirection * GetMovementSpeed(false);
    }

    private void UpdateConsecutiveDashes()
    {
        if (!IsConsecutive())
        {
            consecutiveDashesUsed = 0;
        }

        ++consecutiveDashesUsed;

        if (consecutiveDashesUsed == dashData.ConsecutiveDashesLimitAmount)
        {
            consecutiveDashesUsed = 0;

            stateMachine.Player.Input.DisableActionFor(stateMachine.Player.Input.PlayerActions.Dash, dashData.DashLimitReachedCooldown);
        }
    }

    private bool IsConsecutive()
    {
        return Time.time < startTime + dashData.TimeToBeConsideredConsecutive;
    }
    #endregion

    #region Reusable Methods
    protected override void AddInputActionsCallBack()
    {
        base.AddInputActionsCallBack();

        stateMachine.Player.Input.PlayerActions.Movement.performed += OnMovementPerformed;
    }

    protected override void RemoveInputActionsCallBack()
    {
        base.RemoveInputActionsCallBack();

        stateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformed;
    }
    #endregion

    #region Input Methods
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        shouldKeepRotating = true;
    }

    protected override void OnDashStarted(InputAction.CallbackContext context)
    {
    }
    #endregion
}
