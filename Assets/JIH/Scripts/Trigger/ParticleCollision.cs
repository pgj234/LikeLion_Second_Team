using UnityEngine;

public class ParticleCollision : MonoBehaviour
{

    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log($"충돌한 오브젝트: {other.name}, 태그: {other.tag}");
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("파티클이 플레이어와 충돌! 데미지 적용");
                player.Damaged(1);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Player 컴포넌트 없음!");
            }
        }
    }
}
