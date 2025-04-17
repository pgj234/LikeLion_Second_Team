using UnityEngine;

public class PlayerJumpState : PlayerState
{
    bool jumpIng;

    float jumpTime = 0.4f;
    float jumpValue;

    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        jumpIng = true;

        stateTimer = jumpTime;
        jumpValue = 0;
    }

    public override void Update()
    {
        base.Update();

        if (true == jumpIng)
        {
            // 점프키 누르고 있는 동안
            if (jumpValue <= InputManager.instance.jump)
            {
                jumpValue = InputManager.instance.jump;

                if (0 < stateTimer)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocityX, player.jumpPower * (0.5f + stateTimer * 0.3f));
                }
            }
            else        // 점프키 뗌
            {
                jumpIng = false;
            }
        }

        if (0 != InputManager.instance.xInput)
        {
            player.SetVelocity(InputManager.instance.xInput * player.moveSpd * 0.85f, rb.linearVelocityY);
        }

        if (0 == rb.linearVelocityY)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
