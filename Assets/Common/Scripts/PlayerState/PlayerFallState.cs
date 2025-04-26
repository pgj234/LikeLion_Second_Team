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

        // 벽 감지 및 입력 방향 체크
        if (player.isWalled)
        {
            // 벽이 왼쪽에 있고 왼쪽 키를 누르거나, 벽이 오른쪽에 있고 오른쪽 키를 누를 때
            if ((player.faceDir == -1 && InputManager.instance.xInput < 0) || 
                (player.faceDir == 1 && InputManager.instance.xInput > 0))
            {
                stateMachine.ChangeState(player.wallslideState);
            }
        }

        // 2단 점프 감지
        if (InputManager.instance.jumpPressed && player.DoubleJumpCount > 0)
        {
            player.DoubleJumpCount--;
            stateMachine.ChangeState(player.doubleJumpState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = player.JumpGravity;
    }
}