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
        Debug.Log($"FallingRock 초기화: 초기 위치 = {initialPosition}");
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
        
        // 레이캐스트 디버그
        Debug.DrawRay(transform.position, moveDirection * detectionDistance, Color.yellow);
        
        if (hit.collider != null)
        {
            Debug.Log($"레이캐스트 감지: {hit.collider.name}, 레이어: {hit.collider.gameObject.layer}");
        }
        
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player") && !isShaking && !isFalling)
        {
            Debug.Log("플레이어 감지됨! 흔들림 시작");
            StartShaking();
        }
    }

    private void StartShaking()
    {
        isShaking = true;
        shakeTimer = shakeDuration;
        Debug.Log($"흔들림 시작: 지속시간 = {shakeDuration}초");
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
            Debug.Log("흔들림 종료, 이동 시작");
        }
    }

    private void Fall()
    {
        transform.Translate((Vector3)moveDirection * fallSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        // 감지 거리와 방향을 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(detectionDirection.normalized * detectionDistance));
        
    }
} 