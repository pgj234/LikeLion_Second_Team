using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.DoubleJumpCount = player.MaxDoubleJumpCount;
    }

    public override void Update()
    {
        if (InputManager.instance.ParryPressed)
        {
            stateMachine.ChangeState(player.parryingState);
            return;
        }
        if (InputManager.instance.fInput)
        {
            stateMachine.ChangeState(player.outofFluidState);
        }

        if (InputManager.instance.DashPressed)
        {
            stateMachine.ChangeState(player.dashState);
        }

        if (InputManager.instance.xInput !=0)
        {
            player.stateMachine.ChangeState(player.moveState);
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
