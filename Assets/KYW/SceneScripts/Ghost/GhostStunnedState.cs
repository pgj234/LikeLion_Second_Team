using UnityEngine;
using DG.Tweening;

public class GhostStunnedState : GhostState
{
    private float stunTimer = 0f;
    private Sequence knockbackSequence;

    public GhostStunnedState(Ghost ghost, string animationTrigger) : base(ghost, animationTrigger)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stunTimer = 0f;
        
        // Material 변경
        ghost.spriteRenderer.material = ghost.stunMaterial;
        
        // 파티클 시스템 생성
        if (ghost.hitParticlePrefab != null)
        {
            GameObject hitParticle = Object.Instantiate(ghost.hitParticlePrefab, ghost.transform.position, Quaternion.identity);
            Object.Destroy(hitParticle, 1f);
        }

        // DOTween을 사용한 넉백 효과
        Vector3 startPos = ghost.transform.position;
        Vector3 knockbackPos = startPos + new Vector3(-ghost.currentDirection.x, 0, 0) * ghost.knockbackForce;
        
        knockbackSequence = DOTween.Sequence();
        knockbackSequence.Append(ghost.transform.DOMove(knockbackPos, 0.2f).SetEase(Ease.OutQuad))
                        .Append(ghost.transform.DOMove(startPos, 0.3f).SetEase(Ease.InQuad));
    }

    public override void Update()
    {
        stunTimer += Time.deltaTime;

        // 스턴 시간이 지나면 Idle 상태로 전환
        if (stunTimer >= ghost.stunDuration)
        {
            ghost.stateMachine.ChangeState(ghost.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        // Material 원래대로 복구
        ghost.spriteRenderer.material = ghost.originalMaterial;
        
        // DOTween 시퀀스 정리
        if (knockbackSequence != null)
        {
            knockbackSequence.Kill();
        }
    }
} 