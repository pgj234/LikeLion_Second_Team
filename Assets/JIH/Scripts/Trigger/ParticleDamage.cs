using UnityEngine;

public class ParticleDamage : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"2D 충돌: {collision.gameObject.name}, 태그: {collision.gameObject.tag}");
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            player.Damaged(1);
            Destroy(gameObject);

        }
    }
}
