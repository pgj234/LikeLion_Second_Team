using UnityEngine;

public class RockTrigger : MonoBehaviour
{
    public GameObject rock; // 바위 오브젝트
    public Transform dropPoint; // 빈 오브젝트의 Transform (떨어질 위치)
    public float dropHeight = 10f; // 바위가 떨어지기 시작할 높이

    private bool isTriggered = false; // 중복 트리거 방지

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            DropRock();
            SoundManager.instance.PlaySFX(SFX_JIH.RockFallSound);
        }
    }

    private void DropRock()
    {
        rock.SetActive(true);

        Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // 중력 적용
            rb.gravityScale = 1f;
            rb.linearVelocity = Vector2.zero; // 초기 속도 제거
        }
        
    }
}