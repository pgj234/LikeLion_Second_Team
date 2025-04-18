using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Entity
{
    [Header("이동 속성")]
    [SerializeField] internal float moveSpd;
    [SerializeField] internal float jumpPower;
    [SerializeField] internal float doubleJumpPower;

    internal float dashDir { get; private set; } = 1;

    internal float originalJumpPower;
    internal float originalGravityScale;

    internal PlayerStateMachine stateMachine { get; private set; }

    // 상태
    internal PlayerIdleState idleState { get; private set; }
    internal PlayerMoveState moveState { get; private set; }
    internal PlayerJumpState jumpState { get; private set; }
    internal PlayerDoubleJumpState doubleJumpState { get; private set; }
    internal PlayerWallStickState playerWallStickState { get; private set; }
    internal PlayerWallFallState playerWallFallState { get; private set; }
    internal PlayerDashState playerDashState { get; private set; }

    // 스킬
    internal Player_Dash_Skill player_Dash_Skill { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        originalJumpPower = jumpPower;
        originalGravityScale = rb.gravityScale;

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        doubleJumpState = new PlayerDoubleJumpState(this, stateMachine, "Fly");
        playerWallStickState = new PlayerWallStickState(this, stateMachine, "WallStick");
        playerWallFallState = new PlayerWallFallState(this, stateMachine, "Fly");
        playerDashState = new PlayerDashState(this, stateMachine, "Fly");

        player_Dash_Skill = GetComponent<Player_Dash_Skill>();
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Init(idleState);
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        CheckDashInput();
    }

    internal void ZeroVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }

    internal bool IsGroundDetected() => Physics2D.Raycast(transform.position, Vector2.down, groundChkDistance, groundLayerMask);

    internal bool IsWallDetected() => Physics2D.Raycast(transform.position, Vector2.right * faceDir, wallChkDistance, wallLayerMask);

    void CheckDashInput()
    {
        if (InputManager.instance.dashInput && player_Dash_Skill.CanUseSkill())
        {
            if (0 < InputManager.instance.xInputRaw)
            {
                dashDir = 1;
            }
            else
            {
                dashDir = -1;
            }

            stateMachine.ChangeState(playerDashState);
        }
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundChkDistance));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + wallChkDistance * faceDir, transform.position.y));
    }
}
