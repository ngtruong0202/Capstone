using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseStates
{
    public override void EnterState(PlayerMovement movement)
    {
        movement.animator.SetBool("IsWalking", true);
    }

    public override void UpdateState(PlayerMovement movement)
    {
        Debug.Log("Walk");
        if (Input.GetKey(KeyCode.LeftShift)) {
            ExitState(movement, movement.Run);
            Debug.Log("Bat dau chay");
        }
        else if (movement.move.magnitude < 0.1f)
        {
            ExitState(movement, movement.Idle);
        }

        if (movement.VerticalInput > 0 ||  movement.HorizontalInput > 0)
        {
            movement.currentSpeed = movement.playerSpeed;
        }
    }

    public void ExitState(PlayerMovement movement, MovementBaseStates state)
    {
        movement.animator.SetBool("IsWalking", false);
        movement.SwitchState(state);
    }
}
