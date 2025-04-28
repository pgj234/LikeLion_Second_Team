using System.Collections;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    private float Jumptimer;
    private float keytimer;
    private bool check;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        Jumptimer = player.wallJumpDuration;
        keytimer = 0.3f;
        check = false;
        // 벽에서 튕기는 방향: 입력 반대 방향 + 위쪽
        float x = -player.faceDir; // 벽 방향 기준
        float y = 1; // 위쪽으로 튀게

        player.walljumpDirection = new Vector2(x, y).normalized;
    }

    public override void Update()
    {

        Jumptimer -= Time.deltaTime;
        keytimer -= Time.deltaTime;

        if (InputManager.instance.DashPressed)
        {
            stateMachine.ChangeState(player.dashState);
        }
        if (check == false)
        {
            player.SetVelocity(
            player.walljumpDirection.x * player.wallJumpSpeed,
            player.walljumpDirection.y * player.wallJumpSpeed
        );
        }


        if (Jumptimer <= 0f)
        {
            if (check == false)
            {
                player.SetVelocity(0, -2);
                check = true;
            }
        }
        if(keytimer <=0f)
        { 
            // 벽이 왼쪽에 있고 왼쪽 키를 누르거나, 벽이 오른쪽에 있고 오른쪽 키를 누를 때
            if ((player.faceDir == -1 && InputManager.instance.xInput < 0) || 
                (player.faceDir == 1 && InputManager.instance.xInput > 0))
            {
                stateMachine.ChangeState(player.wallslideState);
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
            }

        }

    }
    public override void Exit()
    {
        base.Exit();
    }
}
