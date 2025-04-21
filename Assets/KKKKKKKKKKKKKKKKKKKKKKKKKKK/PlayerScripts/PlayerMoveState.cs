using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.DoubleJumpCount = player.MaxDoubleJumpCount;
    }

    public override void Update()
    {
        base.Update();
       

        if (InputManager.instance.xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (InputManager.instance.jumpPressed)
        {
            player.stateMachine.ChangeState(player.jumpState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
