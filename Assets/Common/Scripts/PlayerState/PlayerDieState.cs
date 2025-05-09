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
                SceneManager.LoadScene("Final_2 1111", LoadSceneMode.Additive);
                SceneManager.LoadScene("JIH_Final 1111", LoadSceneMode.Additive);
                SceneManager.LoadScene("KYW_FinalLava1111", LoadSceneMode.Additive);
                SceneManager.LoadScene("PGJ_Final 1111", LoadSceneMode.Additive);
                SceneManager.LoadScene("YSME_Final 1111", LoadSceneMode.Additive);
            }

            EventManager.instance.PublishPlayerRespawned();
            EventManager.instance.PublishPlayerDamaged(0);
            rb.linearVelocity = Vector2.zero;
            player.transform.position = player.lastSavePointPos;
            stateMachine.ChangeState(player.idleState);

            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
