using UnityEngine;

public class GhostIdleState : GhostState
{
    public GhostIdleState(Ghost ghost, string animationTrigger) : base(ghost, animationTrigger)
    {
    }

    public override void Enter()
    {
        // Idle 애니메이션 재생
        ghost.animator.SetBool("IsIdle", true);
    }

    public override void Update()
    {
        // 플레이어 감지
        if (ghost.DetectPlayer())
        {
            ghost.stateMachine.ChangeState(ghost.angryState);
        }
    }

    public override void Exit()
    {
        // Idle 애니메이션 종료
        ghost.animator.SetBool("IsIdle", false);
    }
} 