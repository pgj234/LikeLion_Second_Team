using UnityEngine;

public class GhostIdleState : GhostState
{
    private const string IS_IDLE = "IsIdle";

    public GhostIdleState(Ghost ghost) : base(ghost)
    {
    }

    public override void Enter()
    {
        // Idle 애니메이션 재생
        ghost.animator.SetBool(IS_IDLE, true);
    }

    public override void Update()
    {
        // 플레이어 감지
        if (ghost.DetectPlayer())
        {
            ghost.ChangeState(new GhostAngryState(ghost));
        }
    }

    public override void Exit()
    {
        // Idle 애니메이션 종료
        ghost.animator.SetBool(IS_IDLE, false);
    }
} 