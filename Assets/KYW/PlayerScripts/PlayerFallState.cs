using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        // 낙하 시작 시 중력 증가
        rb.gravityScale = player.fallGravity;
    }

    public override void Update()
    {
        base.Update();

        // 착지감지
        if (player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }

        // 벽감지
        if (player.isWalled)
        {
            stateMachine.ChangeState(player.wallslideState);
        }

        // 2단 점프 감지
        if (InputManager.instance.jumpPressed && player.DoubleJumpCount>0)    // 만약 점프횟수가 남아있으면 점프키 또 누르면 2단 점프
        {
            player.DoubleJumpCount--;//점프할때 점프카운트-1
            stateMachine.ChangeState(player.doubleJumpState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        // 중력 원래대로 복원
        rb.gravityScale = player.JumpGravity;
    }
}
