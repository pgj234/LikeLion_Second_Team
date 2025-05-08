using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Player : Entity
{
    [Header("이동 관련")]
    [SerializeField] internal float moveSpd;
    [SerializeField] internal float jumpPower;
    [SerializeField] internal float doubleJumpPower;
    [SerializeField] internal float collisionJumpPower;

    [Header("마찰력 관련")]
    [SerializeField] private PhysicsMaterial2D defaultPhysicsMaterial;
    [SerializeField] private PhysicsMaterial2D groundPhysicsMaterial;
    private CapsuleCollider2D capsuleCollider;

    [Header("점프조작감 관련 중력")]
    [SerializeField] internal float fallGravity;
    [SerializeField] internal float JumpGravity;
    [SerializeField] internal float jumpTime = 0.2f;
    [SerializeField] internal float DoubleJumpTime = 0.2f;

    [Header("최대더블점프횟수/남은더블점프횟수")]
    [SerializeField] internal int MaxDoubleJumpCount;
    [SerializeField] internal int DoubleJumpCount;

    [Header("바닥/벽 체크 관련")]
    [SerializeField] internal bool isGrounded;
    [SerializeField] internal bool isWalled;

    [Header("체크 오브젝트")]
    [SerializeField] internal GameObject groundCheck;
    [SerializeField] internal GameObject wallCheck;

    [Header("벽점프 관련")]
    [SerializeField] internal Vector2 walljumpDirection;
    [SerializeField] internal float wallJumpSpeed = 10f;
    [SerializeField] internal float wallJumpDuration = 0.15f;
    [SerializeField] internal float wallJumpPowerX;
    [SerializeField] internal float wallJumpPowerY;

    [Header("벽 스태미나 관련")]
    [SerializeField] internal float wallSlideStaminaCost = 5f;

    [Header("무적 시간 관련")]
    [SerializeField] private float invincibilityDuration = 1f;
    private float invincibilityTimer;
    public bool IsInvincible { get; private set; }

    [Header("오브젝트 점프 관련련")]
    [SerializeField] internal float objectJumpPower; // 기본 값 40으로 생각하고 있습니다.
    [SerializeField] internal float objectJumpTime = 0.5f; // 기본 값 0.5초로 생각하고 있습니다.

    [Header("방향 표시")]
    [SerializeField] internal GameObject directionArrow;
    
    internal bool ghostAvailable = false;//유령체크

    public Vector3 lastSavePointPos { get; private set; } 

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
    internal PlayerObjectJumpState objectJumpState {get; private set;}
    internal PlayerParryingState parryingState { get; private set; }
    internal PlayerCollisionJumpState collisionJumpState { get; private set; }
    internal PlayerOutofFluidState outofFluidState {get; private set;}
    internal PlayerDieState playerDieState { get; private set; }

    [Header("이펙트 생성 위치")]
    public Transform dashEffectPoint;
    public Transform jumpEffectPoint;

    protected override void Awake()
    {
        base.Awake();

        capsuleCollider = GetComponent<CapsuleCollider2D>();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        doubleJumpState = new PlayerDoubleJumpState(this, stateMachine, "DoubleJump");
        fallState = new PlayerFallState(this, stateMachine, "Fall");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallslideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "WallJump");
        objectJumpState = new PlayerObjectJumpState(this, stateMachine, "Jump"); // 점프 애니메이션 그대로 사용하면
        outofFluidState = new PlayerOutofFluidState (this, stateMachine, "Die");
        parryingState = new PlayerParryingState(this, stateMachine, "Parrying");
        collisionJumpState = new PlayerCollisionJumpState(this, stateMachine, "Jump");
        playerDieState = new PlayerDieState(this, stateMachine, "Die");
    }

    public void SetFriction(bool isGrounded)
    {
        capsuleCollider.sharedMaterial = isGrounded ? groundPhysicsMaterial : defaultPhysicsMaterial;
    }

    protected void Start()
    {
        stateMachine.Init(idleState);
    }

    protected void Update()
    {
        stateMachine.currentState.Update();
        
        // 무적 시간 업데이트
        if (IsInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                IsInvincible = false;
            }
        }
    }
    
    internal GameObject ghostPlayerObj => transform.Find("Ghost_Player").gameObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SavePoint"))
        {
            if (lastSavePointPos != collision.transform.position)
            {
                lastSavePointPos = collision.transform.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CollisionJump"))
        {
            stateMachine.ChangeState(collisionJumpState);
        }
    }

    public void Damaged(int damage)
    {
        if (IsInvincible) return;

        // 데미지 이벤트 발생
        EventManager.instance.PublishPlayerDamaged(damage);
        
        // 무적 시간 시작
        IsInvincible = true;
        invincibilityTimer = invincibilityDuration;

        // 깜빡임 효과 시작
        StartBlinking();
    }

    private void StartBlinking()
    {
        // 새로운 깜빡임 시퀀스 생성
        var sequence = DOTween.Sequence();
        
        // 무적 시간 동안 반복
        float blinkDuration = 0.2f; // 한 번 깜빡이는 시간
        int blinkCount = Mathf.CeilToInt(invincibilityDuration / blinkDuration);
        
        for (int i = 0; i < blinkCount; i++)
        {
            sequence.Append(spriteRenderer.DOFade(0.3f, blinkDuration / 2))
                   .Append(spriteRenderer.DOFade(1f, blinkDuration / 2));
        }

        // 시퀀스가 끝나면 알파값을 1로 복구
        sequence.OnComplete(() => {
            spriteRenderer.DOFade(1f, 0.1f);
            IsInvincible = false;
        });
    }

}
