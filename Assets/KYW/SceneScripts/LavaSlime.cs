using UnityEngine;

public class LavaSlime : MonoBehaviour
{
    internal SpriteRenderer spriteRenderer;
    internal Animator animator;
    
    [Header("기본 설정")]
    [SerializeField] public float moveSpeed = 3f;
    [SerializeField] public float detectionRange = 5f;
    [SerializeField] public int maxHealth = 3;
    [SerializeField] public float gravityScale = 1f;

    [Header("스턴 설정")]
    [SerializeField] public float stunDuration = 0.5f;
    [SerializeField] public float knockbackForce = 5f;

    [Header("피격 효과")]
    [SerializeField] public GameObject hitParticlePrefab;
    [SerializeField] public Material stunMaterial;

    internal LavaSlimeStateMachine stateMachine;
    internal Transform detectedPlayer;
    internal Vector2 currentDirection;
    internal Material originalMaterial { get; private set; }
    internal int currentHealth;
    internal Rigidbody2D rb;

    // 상태 인스턴스들
    internal LavaSlimeMoveState moveState;
    internal LavaSlimeDieState dieState;
    internal LavaSlimeStunnedState stunnedState;

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
        stateMachine = new LavaSlimeStateMachine();
        stateMachine.Initialize(moveState);
    }

    private void InitializeStates()
    {
        moveState = new LavaSlimeMoveState(this, "IsMoving");
        dieState = new LavaSlimeDieState(this, "IsDie");
        stunnedState = new LavaSlimeStunnedState(this, "IsStunned");
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
        else
        {
            stateMachine.ChangeState(stunnedState);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
} 