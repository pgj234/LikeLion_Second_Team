using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.player_Dash_Skill.dashDuration;
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.player_Dash_Skill.dashSpd * player.dashDir, 0);

        if (stateTimer < 0)     // 대쉬 끝
        {
            if (false == player.IsGroundDetected())       // 공중
            {
                stateMachine.ChangeState(player.jumpState);
            }
            else if (true == player.IsGroundDetected())       // 땅
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
