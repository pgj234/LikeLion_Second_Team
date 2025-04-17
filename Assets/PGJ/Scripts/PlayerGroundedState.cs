using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (0 < InputManager.instance.jump)
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
