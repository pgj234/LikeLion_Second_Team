using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private float shakeDuration = 1f;    // 흔들리는 시간
    [SerializeField] private float fallSpeed = 5f;        // 이동 속도
    [SerializeField] private float detectionDistance = 5f; // 감지 거리
    [SerializeField] private float shakeIntensity = 0.1f; // 흔들림 강도
    [SerializeField] private Vector2 detectionDirection = Vector2.right; // 감지 방향

    private bool isShaking = false;
    private bool isFalling = false;
    private Vector3 initialPosition;
    private float shakeTimer;
    private LayerMask playerMask;
    private Vector2 moveDirection;

    private void Start()
    {
        initialPosition = transform.position;
        playerMask = LayerMask.GetMask("Player");
        moveDirection = detectionDirection.normalized;
    }

    private void Update()
    {
        CheckForPlayer();
        
        if (isShaking)
        {
            Shake();
        }
        
        if (isFalling)
        {
            Fall();
        }
    }

    private void CheckForPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, detectionDistance, playerMask);
        
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player") && !isShaking && !isFalling)
        {
            StartShaking();
        }
    }

    private void StartShaking()
    {
        isShaking = true;
        shakeTimer = shakeDuration;
    }

    private void Shake()
    {
        if (shakeTimer > 0)
        {
            transform.position = initialPosition + Random.insideUnitSphere * shakeIntensity;
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            isShaking = false;
            isFalling = true;
        }
    }

    private void Fall()
    {
        transform.Translate((Vector3)moveDirection * fallSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                player.Damaged(1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // 감지 거리와 방향을 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(detectionDirection.normalized * detectionDistance));
    }
} 