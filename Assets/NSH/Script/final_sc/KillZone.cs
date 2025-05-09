using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject); // 플레이어 삭제
           
        }
    }
}
