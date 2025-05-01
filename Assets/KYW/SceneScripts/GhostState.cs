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

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            ghost.TakeDamage();
            // 검과 충돌한 방향의 반대 방향으로 넉백
            Vector2 knockbackDirection = (ghost.transform.position - other.transform.position).normalized;
            ghost.ChangeState(new GhostStunnedState(ghost, knockbackDirection));
        }
    }
} 