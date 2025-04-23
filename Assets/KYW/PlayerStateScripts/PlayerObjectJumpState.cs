using UnityEngine;

public class PlayerObjectJumpState : PlayerState
{
    public PlayerObjectJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
} 