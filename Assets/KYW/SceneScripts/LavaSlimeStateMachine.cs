public class LavaSlimeStateMachine
{
    public LavaSlimeState CurrentState { get; private set; }

    public void Initialize(LavaSlimeState initialState)
    {
        CurrentState = initialState;
        CurrentState.Enter();
    }

    public void ChangeState(LavaSlimeState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
} 