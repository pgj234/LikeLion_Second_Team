using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어에게 데미지를 주는 함수 호출
            PlayerDamage player = collision.gameObject.GetComponent<PlayerDamage>();
            if (player != null)
            {
                Debug.Log("닿음");
                player.OnDamaged(transform.position);
            }
            else
            {
                Debug.Log("PlayerDamage 컴포넌트가 없습니다.");
            }
        }
        else
        {
            Debug.Log("충돌한 객체는 Player가 아닙니다.");
        }
    
    }
}