using UnityEngine;

public class MoveRock : MonoBehaviour
{
    [Header("레이캐스트 설정")]
    [SerializeField] private float raycastDistance = 5f;
    [SerializeField] private float additionalMoveDistance = 2f; // 추가로 이동할 거리
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float moveSpeed = 3f;

    [Header("디버그 설정")]
    [SerializeField] private bool showDebugRays = true;
    [SerializeField] private Color rayColor = Color.red;

    private Vector2[] directions = new Vector2[] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };
    private Transform detectedPlayer;
    private Vector2 currentDirection;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        DetectAndMove();
        UpdateSpriteDirection();
    }

    private void DetectAndMove()
    {
        float shortestDistance = float.MaxValue;
        Vector2 shortestDirection = Vector2.zero;

        foreach (Vector2 direction in directions)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, playerLayer);
            
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                if (hit.distance < shortestDistance)
                {
                    shortestDistance = hit.distance;
                    shortestDirection = direction;
                }
            }
        }

        if (shortestDistance < float.MaxValue)
        {
            detectedPlayer = Physics2D.Raycast(transform.position, shortestDirection, raycastDistance, playerLayer).collider.transform;
            // 레이캐스트 거리 + 추가 거리만큼 이동
            Vector2 targetPosition = (Vector2)transform.position + shortestDirection * (shortestDistance + additionalMoveDistance);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            currentDirection = shortestDirection;
        }
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

    private void FlipSprite(bool isLeft)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = isLeft;
        }
    }

    private void OnDrawGizmos()
    {
        if (!showDebugRays) return;

        Gizmos.color = rayColor;
        foreach (Vector2 direction in directions)
        {
            Gizmos.DrawRay(transform.position, direction * raycastDistance);
        }
    }
} 