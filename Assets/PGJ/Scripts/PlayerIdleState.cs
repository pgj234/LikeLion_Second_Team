using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.ZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (0 != InputManager.instance.xInput)
        {
            player.stateMachine.ChangeState(player.moveState);
        }

        //if (InputManager.instance.)
        //{
        
        //}
    }

    public override void Exit()
    {
        base.Exit();
    }
}
