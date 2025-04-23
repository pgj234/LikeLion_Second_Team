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

        // ī�޶� ���ɿ� ���̱�
        SceneMaster.instance.cineCam.Follow = ghost.transform;
    }

    public override void Update()
    {
        ghostTimer -= Time.deltaTime;

        // ��ü��Ż Ű ����
        if (InputManager.instance.fInputReleased)
        {
            isGhostKeyReleased = true;
        }

        if (true == isGhostKeyReleased)
        {
            if (maxGhostTime - minGhostTime > ghostTimer)       // Ű�� maxGhostTime���� ���� �� ��� �ǵ��ư���
            {
                ReturnGhost();
            }
        }

        if (0 > ghostTimer)                            // Ű�� maxGhostTime���� �� ������ �ִ� ��� ���� �ǵ��ư���
        {
            ReturnGhost();
        }

        ghost.transform.position += new Vector3(InputManager.instance.xInput, InputManager.instance.yInput) * ghostSpd * Time.deltaTime;
    }

    public override void Exit()
    {
        base.Exit();

        // �÷��̾ ī�޶� ���̱�
        SceneMaster.instance.cineCam.Follow = player.transform;

        ghost.SetActive(false);
    }

    void ReturnGhost()
    {
        // �޿;� �������� ����Ʈ������ �ָ� ������

        ghost.transform.localPosition = originalLocalPos;

        stateMachine.ChangeState(player.idleState);
    }
}
