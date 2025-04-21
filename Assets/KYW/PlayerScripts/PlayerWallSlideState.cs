using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    int dir;
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        // y속도를 고정 (아래로 천천히 미끄러지도록)
        if (rb.linearVelocityY < -player.wallSlideSpeed)
        {
            player.SetVelocity(rb.linearVelocityX, -player.wallSlideSpeed);
        }

        // 점프 입력 → 벽점프 상태로 전환
        if (InputManager.instance.jumpPressed)
        {
            stateMachine.ChangeState(player.wallJumpState);
        }

        //벽 없음 감지 → 낙하 상태로 전환
        if (!player.isWalled)
        {

            stateMachine.ChangeState(player.fallState);
        }

        //땅 감지 → idle 상태로 전환
        if (player.isGrounded)
        {

            stateMachine.ChangeState(player.idleState);
        }

        if (InputManager.instance.DashPressed)
        {
            stateMachine.ChangeState(player.dashState);
        }

        //이동제어
        if (0 != InputManager.instance.xInput)
        {
            rb.linearVelocity = new Vector2(InputManager.instance.xInput * player.moveSpd, rb.linearVelocityY);
        }

        //이동제어
        if (0 == InputManager.instance.xInput)
        {
            player.SetVelocity(0, rb.linearVelocityY);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
