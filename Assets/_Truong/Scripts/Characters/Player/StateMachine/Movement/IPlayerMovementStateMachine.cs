using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayerMovementStateMachine : IStateMachine
{
    public IPlayerIdleingState idleingState { get; }

    public IPlayerRunningState runningState { get; }
    public IPlayerSprintingState sprintingState { get; }
    public IPlayerWalkingState walkingState { get; }

    public IPlayerMovementStateMachine()
    {
        idleingState = new IPlayerIdleingState();

        runningState = new IPlayerRunningState();
        walkingState = new IPlayerWalkingState();
        sprintingState = new IPlayerSprintingState();
    }
}
