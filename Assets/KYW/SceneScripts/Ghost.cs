using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float detectionRange = 5f;  // 플레이어 감지 범위

    private SpriteRenderer spriteRenderer;
    private bool isMovingLeft = false;

    private void Start()
    {
        // 컴포넌트 참조
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // RaycastCircle로 플레이어 감지 (태그 사용)
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        
        if (hitColliders.Length > 0)
        {
            // 가장 가까운 플레이어 찾기
            float closestDistance = float.MaxValue;
            Transform closestPlayer = null;

            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    float distance = Vector2.Distance(transform.position, hitCollider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPlayer = hitCollider.transform;
                    }
                }
            }

            if (closestPlayer != null)
            {
                // 플레이어 방향으로 이동
                Vector2 direction = (closestPlayer.position - transform.position).normalized;
                transform.position += new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime;

                // 이동 방향에 따라 스프라이트 뒤집기
                if (direction.x < 0 && !isMovingLeft)
                {
                    isMovingLeft = true;
                    spriteRenderer.flipX = true;
                }
                else if (direction.x > 0 && isMovingLeft)
                {
                    isMovingLeft = false;
                    spriteRenderer.flipX = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 데미지 함수 호출
            PlayerManager.instance.TakeDamage(1);
        }
    }

    // 디버그용 기즈모
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
} 