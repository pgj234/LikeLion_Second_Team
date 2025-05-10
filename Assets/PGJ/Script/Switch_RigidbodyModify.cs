using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    bool switchOn = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (true == switchOn)
        {
            return;
        }

        if (col.CompareTag("Player"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
