using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Playerobjetcdead player = collision.transform.GetComponent<Playerobjetcdead>();
            if (player != null)
            {
                player.Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Playerobjetcdead player = other.GetComponent<Playerobjetcdead>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}