using UnityEngine;
using Unity.Cinemachine;

public class PlayerOutofFluidState : PlayerState
{
    GameObject ghost;

    float minGhostTime = 0.5f;
    float maxGhostTime = 5;
    float ghostTimer;

    float ghostSpd = 5;

    bool isGhostKeyReleased;

    Vector2 originalLocalPos;

    public PlayerOutofFluidState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        originalLocalPos = new Vector2(0, 0.5f);
    }

    public override void Enter()
    {
        base.Enter();

        ghostTimer = maxGhostTime;
        isGhostKeyReleased = false;

        ghost = player.ghostPlayerObj;
        ghost.SetActive(true);

        // 카메라 유령에 붙이기
        SceneMaster.instance.cineCam.Follow = ghost.transform;
    }

    public override void Update()
    {
        ghostTimer -= Time.deltaTime;

        // 유체이탈 키 떼면
        if (InputManager.instance.ghostKeyReleased)
        {
            isGhostKeyReleased = true;
        }

        if (true == isGhostKeyReleased)
        {
            if (maxGhostTime - minGhostTime > ghostTimer)       // 키를 maxGhostTime보다 빨리 뗀 경우 되돌아가기
            {
                ReturnGhost();
            }
        }

        if (0 > ghostTimer)                            // 키를 maxGhostTime보다 더 누르고 있는 경우 강제 되돌아가기
        {
            ReturnGhost();
        }

        ghost.transform.position += new Vector3(InputManager.instance.xInput, InputManager.instance.yInput) * ghostSpd * Time.deltaTime;
    }

    public override void Exit()
    {
        base.Exit();

        // 플레이어에 카메라 붙이기
        SceneMaster.instance.cineCam.Follow = player.transform;

        ghost.SetActive(false);
    }

    void ReturnGhost()
    {
        // 쭈와압 빨려들어가는 이펙트같은거 주면 좋을듯

        ghost.transform.localPosition = originalLocalPos;

        stateMachine.ChangeState(player.idleState);
    }
}
