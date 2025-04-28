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
        
        if (InputManager.instance.fInput && true == player.ghostAvailable)
        {
            stateMachine.ChangeState(player.outofFluidState);
            return;
        }

        if (InputManager.instance.DashPressed)
        {
            stateMachine.ChangeState(player.dashState);
            return;
        }

        if (InputManager.instance.xInput !=0)
        {
            player.stateMachine.ChangeState(player.moveState);
            return;
        }

        if (InputManager.instance.jumpPressed)
        {
            player.stateMachine.ChangeState(player.jumpState);
            return;
        }

        //하강 감지 → 낙하 상태로 전환
        if (rb.linearVelocityY < 0f)
        {
            stateMachine.ChangeState(player.fallState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
