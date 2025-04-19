using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    float dashTimer;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        rb.gravityScale = 0;
        dashTimer = player.dashDuration;

        // 방향 설정
        // 입력 방향 or 바라보는 방향
        player.dashDirection = new Vector2(InputManager.instance.xInput, 0).normalized;
        if (player.dashDirection == Vector2.zero)
            player.dashDirection = Vector2.right * player.faceDir;
    }
    public override void Update()
    {
        dashTimer -= Time.deltaTime;
        rb.linearVelocity = player.dashDirection * player.dashSpeed;

        if (dashTimer <= 0f)
        {
            rb.gravityScale = player.JumpGravity;
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
