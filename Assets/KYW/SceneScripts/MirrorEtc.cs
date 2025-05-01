using UnityEngine;

public class MirrorEtc : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isTriggered = false;
    private Collider2D myCollider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();
        
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer 컴포넌트가 없습니다!");
        }
        if (myCollider == null)
        {
            Debug.LogWarning("Collider2D 컴포넌트가 없습니다!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered && other.CompareTag("Sword"))
        {
            // 색상을 빨간색으로 변경
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.red;
            }

            // 랜덤하게 좌우 방향 선택 (-45도 또는 45도)
            float randomRotation = Random.value > 0.5f ? 45f : -45f;
            transform.rotation = Quaternion.Euler(0, 0, randomRotation);

            // 콜라이더 삭제
            if (myCollider != null)
            {
                Destroy(myCollider);
            }

            isTriggered = true;
        }
    }
} 