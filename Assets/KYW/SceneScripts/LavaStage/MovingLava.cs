using UnityEngine;

public class MovingLava : MonoBehaviour
{
    [SerializeField] private float parentMoveSpeed = 2f; // 부모의 이동 속도
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // 부모를 오른쪽으로 이동
        rb.MovePosition(rb.position + Vector2.right * parentMoveSpeed * Time.fixedDeltaTime);
    }
} 