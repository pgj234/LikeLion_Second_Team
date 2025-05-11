using UnityEngine;

public class MovingLava : MonoBehaviour
{
    [SerializeField] internal float parentMoveSpeed = 2f; // 부모의 이동 속도
    private Rigidbody2D rb;

    bool playerDie = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (true == playerDie)
        {
            return;
        }

        // 부모를 오른쪽으로 이동
        rb.MovePosition(rb.position + Vector2.right * parentMoveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (col.TryGetComponent(out Player player))
            {
                playerDie = true;
                GetComponent<BoxCollider2D>().enabled = false;
                EventManager.instance.PublishPlayerDamaged(999);
            }
        }
    }
} 