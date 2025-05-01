using UnityEngine;

public class GhostStateMachine
{
    public GhostState CurrentState { get; private set; }

    public void Initialize(GhostState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(GhostState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void Update() => CurrentState?.Update();
}