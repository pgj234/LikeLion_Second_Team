using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float stunDuration = 2f; // 기절 지속 시간

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어가 맞았을 경우 기절 처리
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Stun(stunDuration);
            }

            // 오브젝트는 충돌 후 파괴
            Destroy(gameObject);
        }
    }
}
