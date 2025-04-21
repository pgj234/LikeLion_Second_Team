using UnityEngine;

public class Player : Entity
{
    [Header("이동 속성")]
    [SerializeField] internal float moveSpd;
    [SerializeField] internal float jumpPower;
    [SerializeField] internal float doubleJumpPower;
    [Header("점프조작감 관련 중력")]
    [SerializeField] internal float fallGravity;
    [SerializeField] internal float defaultGravity;
    [SerializeField] internal float jumpTime = 0.1f;
    [Header("최대점프횟수/현재점프횟수")]
    [SerializeField] internal int MaxjumpCount;
    [SerializeField] internal int jumpCount;

    internal PlayerStateMachine stateMachine { get; private set; }

    // 상태
    internal PlayerIdleState idleState { get; private set; }
    internal PlayerMoveState moveState { get; private set; }
    internal PlayerJumpState jumpState { get; private set; }
    internal PlayerDoubleJumpState doubleJumpState { get; private set; }
    internal PlayerFallState fallState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        doubleJumpState = new PlayerDoubleJumpState(this, stateMachine, "Fly");
        fallState = new PlayerFallState(this, stateMachine, "Fall");

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

    internal void ZeroVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
