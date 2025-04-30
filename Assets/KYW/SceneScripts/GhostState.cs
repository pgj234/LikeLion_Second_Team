using UnityEngine;

public abstract class GhostState
{
    protected Ghost ghost;

    public GhostState(Ghost ghost)
    {
        this.ghost = ghost;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
} 