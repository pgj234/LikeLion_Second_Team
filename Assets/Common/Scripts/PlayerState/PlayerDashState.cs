using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        SkillManager.instance.DashSkill.StartDash(player, rb);

        // 대시가 시작되지 않았다면 (스태미나 부족) 대시 상태를 종료
        if (!SkillManager.instance.DashSkill.IsDashing())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Update()
    {
        base.Update();
        SkillManager.instance.DashSkill.UpdateDash(player, rb);

        if (!SkillManager.instance.DashSkill.IsDashing())
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        SkillManager.instance.DashSkill.EndDash(player.rb, player.JumpGravity); 
    }
}
