using UnityEngine;

public class PlayerJumpState : PlayerState
{
    internal bool jumpIng;
    internal bool dashCome = false;         

    float jumpTime = 0.4f;

    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        jumpIng = true;

        stateTimer = jumpTime;
    }

    public override void Update()
    {
        base.Update();

        if (true == jumpIng)
        {
            // 점프키 누르고 있는 동안
            if (1 == InputManager.instance.jumpInput)
            {
                if (0 < stateTimer)
                {
                    player.SetVelocity(rb.linearVelocityX, player.jumpPower * (0.5f + stateTimer * 0.3f));
                }
            }
            else        // 점프키 뗌
            {
                jumpIng = false;
            }
        }
        else        // 점프키 뗐을 때 2단 점프 가능
        {
            if (1 == InputManager.instance.jumpInput)    // 점프키 또 누르면 2단 점프
            {
                stateMachine.ChangeState(player.doubleJumpState);
            }
        }

        if (0 != InputManager.instance.xInput)
        {
            if (player.IsWallDetected())
            {
                stateMachine.ChangeState(player.playerWallStickState);
            }
            else
            {
                player.SetVelocity(InputManager.instance.xInput * player.moveSpd * 0.85f, rb.linearVelocityY);
            }
        }

        if (0 == rb.linearVelocityY && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
