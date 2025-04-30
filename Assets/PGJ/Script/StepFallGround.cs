using UnityEngine;

public class StepFallGround : MonoBehaviour
{
    Rigidbody2D rb => GetComponentInParent<Rigidbody2D>();

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.mass = 11;
            rb.gravityScale = 0.1f;
        }
    }
}
