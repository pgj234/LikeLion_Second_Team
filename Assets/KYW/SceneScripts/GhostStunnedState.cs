using UnityEngine;

public class GhostStunnedState : GhostState
{
    private float stunTimer = 0f;
    private float stunDuration = 0.5f;
    private Vector2 knockbackDirection;
    private float knockbackForce = 5f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private GameObject hitParticle;

    public GhostStunnedState(Ghost ghost, Vector2 direction) : base(ghost)
    {
        knockbackDirection = direction;
        spriteRenderer = ghost.spriteRenderer;
        originalColor = spriteRenderer.color;
    }

    public override void Enter()
    {
        stunTimer = 0f;
        spriteRenderer.color = Color.white;
        
        // 파티클 시스템 생성
        if (ghost.hitParticlePrefab != null)
        {
            hitParticle = Object.Instantiate(ghost.hitParticlePrefab, ghost.transform.position, Quaternion.identity);
            Object.Destroy(hitParticle, 1f);
        }
    }

    public override void Update()
    {
        stunTimer += Time.deltaTime;

        // 넉백 효과
        ghost.transform.position += new Vector3(knockbackDirection.x, knockbackDirection.y, 0) * knockbackForce * Time.deltaTime;

        // 스턴 시간이 지나면 Idle 상태로 전환
        if (stunTimer >= stunDuration)
        {
            ghost.ChangeState(new GhostIdleState(ghost));
        }
    }

    public override void Exit()
    {
        spriteRenderer.color = originalColor;
    }
} 