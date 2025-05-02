using UnityEngine;

public class GhostState
{
    protected Ghost ghost;
    protected string animationTrigger;

    public GhostState(Ghost ghost, string animationTrigger)
    {
        this.ghost = ghost;
        this.animationTrigger = animationTrigger;
    }

    public virtual void Enter() 
    {
        ghost.animator.SetBool(animationTrigger, true);
    }

    public virtual void Update() { }

    public virtual void Exit() 
    {
        ghost.animator.SetBool(animationTrigger, false);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            ghost.TakeDamage();
            // 검과 충돌한 방향의 반대 방향으로 넉백
            Vector2 knockbackDirection = (ghost.transform.position - other.transform.position).normalized;
            ghost.stateMachine.ChangeState(ghost.stunnedState);
        }
    }
} 