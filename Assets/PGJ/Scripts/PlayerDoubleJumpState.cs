using UnityEngine;

public class PlayerDoubleJumpState : PlayerState
{
    public PlayerDoubleJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.linearVelocity = new Vector2(rb.linearVelocityX, player.doubleJumpPower);
    }

    public override void Update()
    {
        base.Update();

        if (0 != InputManager.instance.xInput)
        {
            player.SetVelocity(InputManager.instance.xInput * player.moveSpd , rb.linearVelocityY);
        }

        if (0 == rb.linearVelocityY)
        {
            stateMachine.ChangeState(player.idleState);
        }
        //하강 감지 → 낙하 상태로 전환
        if (rb.linearVelocityY < 0f)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
