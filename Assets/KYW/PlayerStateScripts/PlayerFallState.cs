using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        rb.gravityScale = player.fallGravity;
    }

    public override void Update()
    {
        base.Update();

        // 착지 감지
        if (player.isGrounded)
        {
            player.DoubleJumpCount = player.MaxDoubleJumpCount; // 더블 점프 횟수 초기화
            stateMachine.ChangeState(player.idleState);
        }

        // 벽 감지
        if (player.isWalled)
        {
            stateMachine.ChangeState(player.wallslideState);
        }

        // 2단 점프 감지
        if (InputManager.instance.jumpPressed && player.DoubleJumpCount > 0)
        {
            player.DoubleJumpCount--;
            stateMachine.ChangeState(player.doubleJumpState);
        }
        
        //하강 감지 → 낙하 상태로 전환
        if (rb.linearVelocityY < 0f)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = player.JumpGravity;
    }
}