using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHardLandingState : PlayerLandingState
{
    public PlayerHardLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        stateMachine.Player.Input.PlayerActions.Movement.Disable();

        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        ResetVelocity();
    }

    public override void Exit()
    {
        base.Exit();

        stateMachine.Player.Input.PlayerActions.Movement.Enable();
    }

    public override void OnAnimationExitEvent()
    {
        stateMachine.Player.Input.PlayerActions.Movement.Enable();
    }
    #endregion

    #region Reusable Methods
    protected override void AddInputActionsCallBack()
    {
        base.AddInputActionsCallBack();

        stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
    }

    protected override void RemoveInputActionsCallBack()
    {
        base.RemoveInputActionsCallBack();

        stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;
    }

    protected override void OnMove()
    {
        if (stateMachine.ReusableData.ShouldWalk)
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.RunningState);
    }
    #endregion

    #region Input Methods
    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        
    }
    private void OnMovementStarted(InputAction.CallbackContext context)
    {
        OnMove();
    }
    #endregion
}
