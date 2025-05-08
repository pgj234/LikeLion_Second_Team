using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Chair : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private List<Transform> waypoints = new List<Transform>(); // 이동할 경로 포인트들
    [SerializeField] private float moveSpeed = 2f; // 이동 속도
    [SerializeField] private float waitTime = 1f; // 각 포인트에서 대기 시간

    [Header("흔들림 설정")]
    [SerializeField] private float shakeAmount = 0.1f; // 흔들림 강도
    [SerializeField] private float shakeSpeed = 2f; // 흔들림 속도

    [Header("플레이어 감지 설정")]
    [SerializeField] private float detectionRadius = 5f; // 플레이어 감지 범위

    private int currentWaypointIndex = 0;
    private bool isMoving = false;
    private Vector3 originalPosition;
    private float shakeTimer = 0f;
    private float soundTimer = 0f;
    private float soundInterval = 1f; // 소리 재생 간격
    private Transform playerTransform;

    private void Start()
    {
        originalPosition = transform.position;
        if (waypoints.Count > 0)
        {
            MoveToNextWaypoint();
        }
    }

    private void Update()
    {
        if (!isMoving)
        {
            // 흔들림 효과 적용
            shakeTimer += Time.deltaTime * shakeSpeed;
            float offsetX = Mathf.Sin(shakeTimer) * shakeAmount;
            float offsetY = Mathf.Cos(shakeTimer * 0.5f) * shakeAmount;
            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0);

            // 플레이어 감지 및 소리 재생
            CheckPlayerAndPlaySound();
        }
    }

    private void CheckPlayerAndPlaySound()
    {
        // 플레이어 찾기
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }

        // 플레이어가 감지 범위 안에 있는지 확인
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            
            if (distanceToPlayer <= detectionRadius)
            {
                soundTimer += Time.deltaTime;
                if (soundTimer >= soundInterval)
                {
                    SoundManager.instance.PlaySFX(SFX_KYW.KwangKwang);
                    soundTimer = 0f;
                }
            }
        }
    }

    private void MoveToNextWaypoint()
    {
        if (waypoints.Count == 0) return;

        isMoving = true;
        originalPosition = transform.position;

        // 다음 웨이포인트로 이동
        transform.DOMove(waypoints[currentWaypointIndex].position, moveSpeed)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => {
                isMoving = false;
                originalPosition = transform.position;
                
                // 다음 웨이포인트 인덱스 계산
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
                
                // 대기 후 다음 웨이포인트로 이동
                DOVirtual.DelayedCall(waitTime, MoveToNextWaypoint);
            });
    }

    // 디버그용 기즈모
    private void OnDrawGizmos()
    {
        if (waypoints.Count == 0) return;

        Gizmos.color = Color.cyan;
        
        // 웨이포인트들을 선으로 연결
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (waypoints[i] == null) continue;
            
            Vector3 current = waypoints[i].position;
            Vector3 next = waypoints[(i + 1) % waypoints.Count].position;
            
            Gizmos.DrawLine(current, next);
            Gizmos.DrawWireSphere(current, 0.2f);
        }

        // 플레이어 감지 범위 표시
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
} 