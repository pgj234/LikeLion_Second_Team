using UnityEngine;

public class PlayerDoubleJumpState : PlayerState
{
    private float Jumptimer;
    public PlayerDoubleJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.doubleJumpPower);
        Jumptimer = player.DoubleJumpTime;
    }

    public override void Update()
    {
        base.Update();

        // 점프 유지 중
        if (InputManager.instance.jumpHold && Jumptimer > 0)
        {
            // 점프 힘 보간: 점점 줄어듦
            float t = 1 - (Jumptimer / player.DoubleJumpTime); // 0 → 1
            float jumpForce = Mathf.Lerp(player.doubleJumpPower, 0, t); // 점점 줄어듦
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            Jumptimer -= Time.deltaTime;
        }

        // 점프 키 뗐을 때 강제 컷
        if (InputManager.instance.jumpReleased)
        {
            Jumptimer = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }
        // 벽감지
        if (player.isWalled)
        {
            stateMachine.ChangeState(player.wallslideState);
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
    }
}
