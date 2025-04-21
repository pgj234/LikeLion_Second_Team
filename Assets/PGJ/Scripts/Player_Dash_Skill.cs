using UnityEngine;

public class Player_Dash_Skill : PlayerSkill
{
    [Header("대시 정보")]
    [SerializeField] internal float dashSpd;
    [SerializeField] internal float dashDuration;

    protected override void Update()
    {
        base.Update();
    }

    internal override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    internal override void UseSkill()
    {
        base.UseSkill();
    }
}
