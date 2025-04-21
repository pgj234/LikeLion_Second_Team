using UnityEngine;

public class PlayerWallFallState : PlayerState
{
    public PlayerWallFallState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (0 != InputManager.instance.xInput)
        {
            player.SetVelocity(InputManager.instance.xInput * player.moveSpd * 0.85f, rb.linearVelocityY);
        }

        // if (0 == rb.linearVelocityY && player.IsGroundDetected())
        // {
        //     stateMachine.ChangeState(player.idleState);
        // }
    }

    public override void Exit()
    {
        base.Exit();
    }    
}
