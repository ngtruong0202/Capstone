
public abstract class IStateMachine
{

    protected IIState currentState;

    public void ChangeState(IIState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Update()
    {
        currentState?.Update();
    }
    public void PhysicUpsdate()
    {
        currentState?.PhysicsUpdate();
    }
}
