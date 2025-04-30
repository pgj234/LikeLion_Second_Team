using UnityEngine;

public class Ghost : MonoBehaviour
{
    internal SpriteRenderer spriteRenderer;
    internal Animator animator;
    
    [Header("기본 설정")]
    [SerializeField] public float moveSpeed = 3f;
    [SerializeField] public float detectionRange = 5f;

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

    private void Start()
    {
        // 컴포넌트 참조
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        originalMaterial = spriteRenderer.material;

        // 상태 머신 초기화
        stateMachine = new GhostStateMachine();
        stateMachine.Initialize(new GhostIdleState(this));
    }

    private void Update()
    {
        stateMachine.Update();
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

    public void ChangeState(GhostState newState)
    {
        stateMachine.ChangeState(newState);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        stateMachine.CurrentState.OnTriggerEnter2D(other);
    }

    // 디버그용 기즈모
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
} 