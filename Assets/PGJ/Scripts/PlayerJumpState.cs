using UnityEngine;

public class PlayerJumpState : PlayerState
{

    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.jumpTime;
    }

    public override void Update()
    {
        base.Update();
        // 점프 시작
        if (InputManager.instance.jumpPressed)
        {
            stateTimer = player.jumpTime;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, player.jumpPower);
            player.jumpCount--;
        }

        // 점프 유지 중
        if (InputManager.instance.jumpHeld && stateTimer > 0)
        {
            // 점프 힘 보간: 점점 줄어듦
            float jumpForce = player.jumpPower * (stateTimer / player.jumpTime);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpPower + jumpForce);

            //stateTimer -= Time.deltaTime;
        }

        // 점프 키 뗐을 때 강제 컷
        if (InputManager.instance.jumpReleased)
        {
            stateTimer = 0f;
        }

        // 점프 카운트 남아있으면 누를때 2단 점프 가능

        if (InputManager.instance.jumpPressed && player.jumpCount>=1&& player.jumpCount<player.MaxjumpCount)    // 점프키 또 누르면 2단 점프
        {
            player.jumpCount--;//점프카운트--
            stateMachine.ChangeState(player.doubleJumpState);
        }

        if (0 != InputManager.instance.xInput)
        {
            player.SetVelocity(InputManager.instance.xInput * player.moveSpd, rb.linearVelocityY);
        }

        //하강 감지 → 낙하 상태로 전환
        if (rb.linearVelocityY < 0f)
        {
            stateMachine.ChangeState(player.fallState);
        }
        
        //fallstate에서 idlestate로 갈꺼라 주석처리
        //if (0 == rb.linearVelocityY)
        //{
        //    stateMachine.ChangeState(player.idleState);
        //}
    }

    public override void Exit()
    {
        base.Exit();
    }
}
