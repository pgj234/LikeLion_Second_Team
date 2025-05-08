using UnityEngine;
using DG.Tweening;

public class Picture : MonoBehaviour
{
    [Header("감지 설정")]
    [SerializeField] private float detectionRadius = 5f; // 감지 범위
    [SerializeField] private LayerMask playerLayer; // 플레이어 레이어
    [SerializeField] private float moveDuration = 1f; // 이동 시간

    private bool isMoving = false; // 이동 중인지 여부

    private void Update()
    {
        if (!isMoving)
        {
            // 원 범위 내 플레이어 감지
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
            
            // 플레이어가 감지되면
            if (hitColliders.Length > 0)
            {
                MoveToRandomPosition();
            }
        }
    }

    private void MoveToRandomPosition()
    {
        isMoving = true;

        // 현재 위치를 중심으로 원 범위 내 랜덤한 위치 계산
        float randomAngle = Random.Range(0f, 360f);
        float randomDistance = Random.Range(0f, detectionRadius);
        Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
        Vector3 targetPosition = transform.position + new Vector3(randomDirection.x, randomDirection.y, 0) * randomDistance;

        SoundManager.instance.PlaySFX(SFX_KYW.DDok2);
        // 랜덤한 위치로 이동
        transform.DOMove(targetPosition, moveDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() => isMoving = false);
    }

    // 디버그용 기즈모
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
} 