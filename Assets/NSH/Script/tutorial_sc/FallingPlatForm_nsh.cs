using UnityEngine;

public class FallingPlatForm_nsh : MonoBehaviour
{
    [Header("설정")]
    [Tooltip("플레이어가 밟고 나서 몇 초 후에 발판이 내려갈지 설정합니다.")]
    public float delayBeforeFall = 0.5f;

    [Tooltip("떨어지는 속도입니다.")]
    public float fallSpeed = 3f;

    [Tooltip("발판이 떨어지고 나서 몇 초 후에 다시 원래 위치로 돌아올지 설정합니다.")]
    public float resetDelay = 2f;

    private Rigidbody2D rb;
    private bool isSteppedOn = false;
    private float timer = 0f;
    private bool isFalling = false;
    private float resetTimer = 0f;

    private Vector3 originalPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        originalPosition = transform.position;
    }

    private void Update()
    {
        if (isSteppedOn)
        {
            timer += Time.deltaTime;

            if (timer >= delayBeforeFall && !isFalling)
            {
                isFalling = true;
            }

            if (isFalling)
            {
                transform.position += Vector3.down * fallSpeed * Time.deltaTime;
                resetTimer += Time.deltaTime;

                if (resetTimer >= resetDelay)
                {
                    ResetPlatform();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isSteppedOn && collision.collider.CompareTag("Player"))
        {
            isSteppedOn = true;
        }
    }

    private void ResetPlatform()
    {
        transform.position = originalPosition;
        isSteppedOn = false;
        isFalling = false;
        timer = 0f;
        resetTimer = 0f;
    }
}
