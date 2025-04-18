using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        player.jumpCount = player.MaxjumpCount;//땅state에 도달하면 점프횟수초기화
        base.Enter();
    }

    public override void Update()
    {
        base.Update();



        if (InputManager.instance.jumpPressed)
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
