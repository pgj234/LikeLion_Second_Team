using UnityEngine;

public class Ghost : MonoBehaviour
{
    
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    
    [Header("기본 설정")]
    [SerializeField] public float moveSpeed = 3f;
    [SerializeField] public float detectionRange = 5f;

    [Header("Angry 상태 설정")]
    [SerializeField] public float angryDuration = 3f;
    [SerializeField] public float angryMoveSpeed = 1.5f;

    [Header("피격 효과")]
    [SerializeField] public GameObject hitParticlePrefab;

    private GhostStateMachine stateMachine;
    private Transform detectedPlayer;
    private Vector2 currentDirection;

    private void Start()
    {
        // 컴포넌트 참조
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 데미지 함수 호출
            PlayerManager.Instance.TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Sword"))
        {
            // 검과 충돌한 방향의 반대 방향으로 넉백
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            ChangeState(new GhostStunnedState(this, knockbackDirection));
        }
    }

    // 디버그용 기즈모
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
} 