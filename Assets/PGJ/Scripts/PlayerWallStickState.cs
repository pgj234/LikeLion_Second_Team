using UnityEngine;

public class PlayerWallStickState : PlayerState
{
    float wallStickMaxTime = 2.5f;

    public PlayerWallStickState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = wallStickMaxTime;

        player.rb.gravityScale = 0;
    }

    // public override void Update()
    // {
    //     base.Update();

    //     if (player.faceDir * -1 == InputManager.instance.xInput || 0 > InputManager.instance.yInput)
    //     {
    //         stateMachine.ChangeState(player.playerWallFallState);
    //         return;
    //     }

    //     if (0 > stateTimer)         // ���� ���� �پ��־ �ڵ����� ������
    //     {
    //         stateMachine.ChangeState(player.playerWallFallState);
    //         return;
    //     }

    //     player.ZeroVelocity();

    //     //if (0 == InputManager.instance.jump && true == player.jumpState.jumpIng)
    //     //{
    //     //    player.jumpState.jumpIng = false;
    //     //}

    //     //if (0 < InputManager.instance.jump && false == player.jumpState.jumpIng)
    //     //{
    //     //    stateMachine.ChangeState(player.jumpState);
    //     //    return;
    //     //}
    // }

    // public override void Exit()
    // {
    //     base.Exit();

    //     player.rb.gravityScale = player.originalGravityScale;
    // }
}
