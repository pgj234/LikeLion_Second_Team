using UnityEngine;

public class StepFallGround : MonoBehaviour
{
    Rigidbody2D rb => GetComponentInParent<Rigidbody2D>();
    Player player;
    Vector2 originalPos;

    bool isOn = false;

    void Awake()
    {
        originalPos = transform.parent.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (false == enabled && false == isOn)
        {
            return;
        }

        if (col.gameObject.CompareTag("Player"))
        {
            if (col.TryGetComponent(out Player _player))
            {
                isOn = true;

                player = _player;
                player.transform.SetParent(transform);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.transform.SetParent(null);
        }
    }

    void Update()
    {
        if (false == isOn)
        {
            return;
        }

        if (originalPos != (Vector2)transform.parent.position)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, originalPos, Time.deltaTime * 2.5f));
            //transform.parent.position = Vector2.MoveTowards(transform.position, originalPos, Time.deltaTime * 2.5f);
        }
    }
}
