using UnityEngine;

public class FallingPlatForm_nsh : MonoBehaviour
{
    [Header("설정")]
    [Tooltip("플레이어가 밟고 나서 몇 초 후에 발판이 내려갈지 설정합니다.")]
    public float delayBeforeFall = 0.5f;

    [Tooltip("떨어지는 속도입니다.")]
    public float fallSpeed = 3f;

    private Rigidbody2D rb;
    private bool isSteppedOn = false;
    private float timer = 0f;
    private bool isFalling = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        if (!isSteppedOn)
            return;

        timer += Time.deltaTime;

        if (timer >= delayBeforeFall && !isFalling)
        {
            isFalling = true;
        }

        if (isFalling)
        {
            // 직접 아래로 이동시키기 (물리 충돌 간섭 없음)
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isSteppedOn && collision.collider.CompareTag("Player"))
        {
            isSteppedOn = true;
        }
    }
}
