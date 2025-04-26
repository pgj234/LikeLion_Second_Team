using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    int dir;
    float bugtime;

    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        bugtime = 0.01f;
        rb.gravityScale = 0;
        // y속도를 고정
        player.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();

        // 스태미나 감소
        EventManager.instance.PublishStaminaChanged(-player.wallSlideStaminaCost);

        // 점프 입력 → 벽점프 상태로 전환
        if (InputManager.instance.jumpPressed)
        {
            stateMachine.ChangeState(player.wallJumpState);
        }
        // bugtime -= Time.deltaTime;
        // if (bugtime > 0f)
        //     return;

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
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = player.JumpGravity;
    }
}
