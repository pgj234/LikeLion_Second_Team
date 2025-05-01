using UnityEngine;
using DG.Tweening;
using System.Collections;

public class SheepTimeline : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private LayerMask playerLayer;
    
    [Header("Movement Settings")]
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float moveDuration = 2f;
    
    [Header("Animation Settings")]
    [SerializeField] private Animator animator;
    [SerializeField] private string detectionAnimationTrigger = "Detected";
    
    private bool isDetected = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        DrawGizmo();
    }

    private void Update()
    {
        if (!isDetected)
        {
            CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        
        if (playerCollider != null)
        {
            isDetected = true;
            StartCoroutine(SheepSequence());
        }
    }

    private IEnumerator SheepSequence()
    {
        // 1. 감지 애니메이션 재생
        animator.SetTrigger(detectionAnimationTrigger);
        yield return new WaitForSeconds(0.5f);

        // 2. 목표 위치로 이동
        transform.DOMove(targetPosition.position, moveDuration)
            .SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(moveDuration);

        // 3. 페이드 아웃
        spriteRenderer.DOFade(0f, 1f);
        yield return new WaitForSeconds(1f);

        // 4. 오브젝트 제거
        Destroy(gameObject);
    }

    private void DrawGizmo()
    {
        // 기즈모를 그리기 위한 빈 메서드
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
} 