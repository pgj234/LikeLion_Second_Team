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
        PlayerManager.Instance.DecreaseStamina(player.wallSlideStaminaCost);

        // 스태미나가 0 이하일 때 fallState로 전환
        if (PlayerManager.Instance.CurrentStamina <= 0)
        {
            stateMachine.ChangeState(player.fallState);
            return;
        }

        // 점프 입력 → 벽점프 상태로 전환
        if (InputManager.instance.jumpPressed)
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        //벽 없음 감지 → 낙하 상태로 전환
        if (!player.isWalled)
        {
            stateMachine.ChangeState(player.fallState);
            return;
        }

        //땅 감지 → idle 상태로 전환
        if (player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        if (InputManager.instance.DashPressed)
        {
            stateMachine.ChangeState(player.dashState);
            return;
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
