using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    [Header("이동 관련")]
    [SerializeField] internal float moveSpd;
    [SerializeField] internal float jumpPower;
    [SerializeField] internal float doubleJumpPower;

    [Header("점프조작감 관련 중력")]
    [SerializeField] internal float fallGravity;
    [SerializeField] internal float JumpGravity;
    [SerializeField] internal float jumpTime = 0.2f;
    [SerializeField] internal float DoubleJumpTime = 0.2f;

    [Header("최대더블점프횟수/남은더블점프횟수")]
    [SerializeField] internal int MaxDoubleJumpCount;
    [SerializeField] internal int DoubleJumpCount;

    [Header("대쉬 관련")]
    [SerializeField] internal Vector2 dashDirection;
    [SerializeField] internal float dashSpeed = 20f;
    [SerializeField] internal float dashDuration = 0.15f;

    [Header("바닥/벽 체크 관련")]
    [SerializeField] internal bool isGrounded;
    [SerializeField] internal bool isWalled;
    [SerializeField] internal float wallJumpPowerX;
    [SerializeField] internal float wallJumpPowerY;

    [Header("벽점프 관련")]
    [SerializeField] internal Vector2 walljumpDirection;
    [SerializeField] internal float wallJumpSpeed = 10f;
    [SerializeField] internal float wallJumpDuration = 0.15f;

    [SerializeField] internal float wallSlideSpeed = 2.5f;


    internal PlayerStateMachine stateMachine { get; private set; }

    // 상태들
    internal PlayerIdleState idleState { get; private set; }
    internal PlayerMoveState moveState { get; private set; }
    internal PlayerJumpState jumpState { get; private set; }
    internal PlayerDoubleJumpState doubleJumpState { get; private set; }
    internal PlayerFallState fallState { get; private set; }
    internal PlayerDashState dashState { get; private set; }
    internal PlayerWallSlideState wallslideState { get; private set; }
    internal PlayerWallJumpState wallJumpState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        doubleJumpState = new PlayerDoubleJumpState(this, stateMachine, "DoubleJump");
        fallState = new PlayerFallState(this, stateMachine, "Fall");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallslideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "WallJump");
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
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWalled = true;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWalled = false;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }


}
