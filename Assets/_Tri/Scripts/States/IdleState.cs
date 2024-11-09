using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementBaseStates
{
    public override void EnterState(PlayerMovement movement)
    {

    }

    public override void UpdateState(PlayerMovement movement)
    {
        movement.currentSpeed = 0f;

        if (movement.move.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement.SwitchState(movement.Run);
            }
            else
            {
                movement.SwitchState(movement.Walk);
            }
        }
    }
}
