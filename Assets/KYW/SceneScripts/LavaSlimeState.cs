using UnityEngine;

public abstract class LavaSlimeState
{
    protected LavaSlime lavaSlime;
    protected string animationParamName;

    protected LavaSlimeState(LavaSlime lavaSlime, string animationParamName)
    {
        this.lavaSlime = lavaSlime;
        this.animationParamName = animationParamName;
    }

    public virtual void Enter()
    {
        lavaSlime.animator.SetBool(animationParamName, true);
    }

    public virtual void Exit()
    {
        lavaSlime.animator.SetBool(animationParamName, false);
    }

    public virtual void Update()
    {
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
    }
} 