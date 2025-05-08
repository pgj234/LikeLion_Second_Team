using UnityEngine;

public class GhostAngryState : GhostState
{
    private Transform targetPlayer;

    public GhostAngryState(Ghost ghost, string animationTrigger) : base(ghost, animationTrigger)
    {
    }

    public override void Enter()
    {
        SoundManager.instance.PlaySFX(SFX_KYW.GhostChoir);
        // Angry 애니메이션 재생
        ghost.animator.SetBool("IsAngry", true);
    }

    public override void Update()
    {
        // 플레이어 추적
        if (ghost.DetectPlayer())
        {
            targetPlayer = ghost.GetDetectedPlayer();
            if (targetPlayer != null)
            {
                // 플레이어 방향으로 이동
                Vector2 direction = (targetPlayer.position - ghost.transform.position).normalized;
                ghost.transform.position += new Vector3(direction.x, direction.y, 0) * ghost.moveSpeed * ghost.angryMoveSpeed * Time.deltaTime;
                ghost.SetDirection(direction);
            }
        }
    }

    public override void Exit()
    {
        // Angry 애니메이션 종료
        ghost.animator.SetBool("IsAngry", false);
    }
} 