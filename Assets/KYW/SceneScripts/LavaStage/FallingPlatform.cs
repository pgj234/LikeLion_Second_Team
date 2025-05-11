using UnityEngine;
using DG.Tweening;

public class FallingPlatform : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private float shakeDuration = 1f;    // 흔들리는 시간
    [SerializeField] private float fallSpeed = 5f;        // 떨어지는 속도
    [SerializeField] private float shakeIntensity = 0.1f; // 흔들림 강도
    [SerializeField] private GameObject fallingParticle;  // 떨어질 때 생성할 파티클

    private bool isShaking = false;
    private bool isFalling = false;
    private Vector3 initialPosition;
    private Vector2 velocity;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (isFalling)
        {
            transform.position += (Vector3)velocity * Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent(out Player player))
            {
                if (player.stateMachine.currentState == player.idleState || 
                    player.stateMachine.currentState == player.moveState)
                {
                    if (!isShaking && !isFalling)
                    {
                        StartShaking();
                    }
                }
            }
        }
    }

    private void StartShaking()
    {
        isShaking = true;
        initialPosition = transform.position;

        // DOTween을 사용하여 흔들림 구현
        transform.DOShakePosition(shakeDuration, shakeIntensity, 10, 90, false, false)
            .OnComplete(() => {
                isShaking = false;
                isFalling = true;
                velocity = Vector2.down * fallSpeed;
                CreateFallingParticle();
            });
    }

    private void CreateFallingParticle()
    {
        SoundManager.instance.PlaySFX(SFX_KYW.GroundBreak, 0.3f);
        if (fallingParticle != null)
        {
            Instantiate(fallingParticle, transform.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        // 플랫폼의 크기를 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
} 