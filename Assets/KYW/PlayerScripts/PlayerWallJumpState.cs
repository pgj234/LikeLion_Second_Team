using System.Collections;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    private float Jumptimer;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        rb.gravityScale = 0;
        Jumptimer = player.wallJumpDuration;

        // 벽에서 튕기는 방향: 입력 반대 방향 + 위쪽
        float x = -player.faceDir; // 벽 방향 기준
        float y = 1; // 위쪽으로 튀게

        player.walljumpDirection = new Vector2(x, y).normalized;
    }

    public override void Update()
    {

        Jumptimer -= Time.deltaTime;

        player.SetVelocity(
            player.walljumpDirection.x * player.wallJumpSpeed,
            player.walljumpDirection.y * player.wallJumpSpeed * 0.75f
        );

        if (Jumptimer <= 0f)
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
