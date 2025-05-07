using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    [SerializeField] private SFX_JIH sfxType;
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅 태그로 충돌 감지
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlaySFX(SFX_JIH.HitGround);
        }
    }
}
