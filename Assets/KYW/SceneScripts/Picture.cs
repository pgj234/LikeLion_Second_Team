using UnityEngine;
using DG.Tweening;

public class Picture : MonoBehaviour
{
    [Header("레이저 설정")]
    [SerializeField] private float rayDistance = 5f; // 레이저 거리
    [SerializeField] private LayerMask playerLayer; // 플레이어 레이어
    [SerializeField] private float moveDistance = 2f; // 이동 거리
    [SerializeField] private float moveDuration = 1f; // 이동 시간

    private bool isMoving = false; // 이동 중인지 여부

    private void Update()
    {
        if (!isMoving)
        {
            // 아래 방향으로 레이저 발사
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, playerLayer);

            // 레이저가 플레이어를 감지하면
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                MoveDown();
            }

            // 디버그용 레이저 시각화
            Debug.DrawRay(transform.position, Vector2.down * rayDistance, Color.red);
        }
    }

    private void MoveDown()
    {
        isMoving = true;

        // 아래로 이동
        transform.DOMoveY(transform.position.y - moveDistance, moveDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() => isMoving = false);
    }
} 