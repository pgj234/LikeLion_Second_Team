using UnityEngine;

public class Ghost : MonoBehaviour
{
    internal SpriteRenderer spriteRenderer;
    internal Animator animator;
    
    [Header("기본 설정")]
    [SerializeField] public float moveSpeed = 3f;
    [SerializeField] public float detectionRange = 5f;
    [SerializeField] public int maxHealth = 3; // 최대 체력
    [SerializeField] public float gravityScale = 1f; // 중력 크기

    [Header("Angry 상태 설정")]
    [SerializeField] public float angryDuration = 3f;
    [SerializeField] public float angryMoveSpeed = 1f;

    [Header("스턴 설정")]
    [SerializeField] public float stunDuration = 0.5f;
    [SerializeField] public float knockbackForce = 5f;

    [Header("피격 효과")]
    [SerializeField] public GameObject hitParticlePrefab;
    [SerializeField] public Material stunMaterial;

    internal GhostStateMachine stateMachine;
    internal Transform detectedPlayer;
    internal Vector2 currentDirection;
    internal Material originalMaterial { get; private set; }
    internal int currentHealth; // 현재 체력
    internal Rigidbody2D rb; // Rigidbody2D 참조

    // 상태 인스턴스들
    internal GhostIdleState idleState;
    internal GhostAngryState angryState;
    internal GhostDieState dieState;
    internal GhostStunnedState stunnedState;

    private void Start()
    {
        // 컴포넌트 참조
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originalMaterial = spriteRenderer.material;
        currentHealth = maxHealth;

        // 상태 인스턴스 초기화
        InitializeStates();

        // 상태 머신 초기화
        stateMachine = new GhostStateMachine();
        stateMachine.Initialize(idleState);
    }

    private void InitializeStates()
    {
        idleState = new GhostIdleState(this, "IsIdle");
        angryState = new GhostAngryState(this, "IsAngry");
        dieState = new GhostDieState(this, "IsDie");
        stunnedState = new GhostStunnedState(this, "IsStunned");
    }

    private void Update()
    {
        stateMachine.CurrentState.Update();
        UpdateSpriteDirection();
    }

    private void UpdateSpriteDirection()
    {
        if (currentDirection.x < 0)
        {
            FlipSprite(true);
        }
        else if (currentDirection.x > 0)
        {
            FlipSprite(false);
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            stateMachine.ChangeState(dieState);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        currentDirection = direction;
    }

    public bool DetectPlayer()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                detectedPlayer = hitCollider.transform;
                return true;
            }
        }
        
        detectedPlayer = null;
        return false;
    }

    public Transform GetDetectedPlayer()
    {
        return detectedPlayer;
    }

    public void FlipSprite(bool isLeft)
    {
        spriteRenderer.flipX = isLeft;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        stateMachine.CurrentState.OnTriggerEnter2D(other);
    }

    // 디버그용 기즈모
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
} 