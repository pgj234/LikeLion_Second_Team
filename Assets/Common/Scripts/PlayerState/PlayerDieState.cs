using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (InputManager.instance.rInput)
        {
            if (0 == string.Compare(SceneManager.GetActiveScene().name, "Team9"))
            {
                SceneManager.LoadScene("Team9");
            }
            else
            {
                EventManager.instance.PublishPlayerRespawned();
                EventManager.instance.PublishPlayerDamaged(0);
                rb.linearVelocity = Vector2.zero;
                player.transform.position = player.lastSavePointPos;
                stateMachine.ChangeState(player.idleState);
            }

            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
