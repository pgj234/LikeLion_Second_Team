using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnDamaged(Vector2 attackerPos)
    {
        // 무적 상태로 전환
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // 튕겨나가기
        int dirc = transform.position.x - attackerPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
        Debug.Log("Player damaged by " + attackerPos);
        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}