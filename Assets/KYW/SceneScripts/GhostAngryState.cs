using UnityEngine;

public class GhostAngryState : GhostState
{
    private float angryTimer = 0f;
    private Transform targetPlayer;

    private const string IS_ANGRY = "IsAngry";

    public GhostAngryState(Ghost ghost) : base(ghost)
    {
    }

    public override void Enter()
    {
        // Angry 애니메이션 재생
        ghost.animator.SetBool(IS_ANGRY, true);
        angryTimer = 0f;
    }

    public override void Update()
    {
        angryTimer += Time.deltaTime;

        // 플레이어 추적
        if (ghost.DetectPlayer())
        {
            targetPlayer = ghost.GetDetectedPlayer();
            if (targetPlayer != null)
            {
                // 플레이어 방향으로 돌진
                Vector2 direction = (targetPlayer.position - ghost.transform.position).normalized;
                ghost.transform.position += new Vector3(direction.x, direction.y, 0) * ghost.moveSpeed * ghost.angryMoveSpeed * Time.deltaTime;
                ghost.SetDirection(direction);
            }
        }

        // 3초 후 Idle 상태로 전환
        if (angryTimer >= ghost.angryDuration)
        {
            ghost.ChangeState(new GhostIdleState(ghost));
        }
    }

    public override void Exit()
    {
        // Angry 애니메이션 종료
        ghost.animator.SetBool(IS_ANGRY, false);
    }
} 