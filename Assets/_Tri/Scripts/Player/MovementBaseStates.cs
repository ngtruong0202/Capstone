using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementBaseStates 
{
    public abstract void EnterState(PlayerMovement movement);

    public abstract void UpdateState(PlayerMovement movement);
}
