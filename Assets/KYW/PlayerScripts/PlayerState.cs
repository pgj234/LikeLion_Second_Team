using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected Rigidbody2D rb;

    private string animBoolName;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
    }

    public virtual void Update()
    {
<<<<<<<< HEAD:Assets/PGJ/Scripts/PlayerState.cs
        stateTimer -= Time.deltaTime;
        
        player.anim.SetFloat("yVelocity", rb.linearVelocityY);
========

        if(InputManager.instance.DashPressed)
        {
            stateMachine.ChangeState(player.dashState);
        }

        //이동제어
        if (0 != InputManager.instance.xInput)
        {
            player.SetVelocity(InputManager.instance.xInput * player.moveSpd, rb.linearVelocityY);
        }

        //이동제어
        if (0 == InputManager.instance.xInput)
        {
            player.SetVelocity(0, rb.linearVelocityY);
        }

>>>>>>>> KYW:Assets/KYW/PlayerScripts/PlayerState.cs
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

}
