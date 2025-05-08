using UnityEngine;
using DG.Tweening;

public class Log : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private int maxHp = 3;           // 최대 체력
    [SerializeField] private GameObject destroyEffect; // 파괴 시 파티클 효과
    [SerializeField] private float shakeDuration = 0.2f;    // 흔들리는 시간
    [SerializeField] private float shakeStrength = 0.1f;    // 흔들림 강도
    [SerializeField] private int shakeVibrato = 10;         // 진동 횟수

    private int currentHp;
    private bool isShaking = false;

    private void Start()
    {
        currentHp = maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword") && !isShaking)
        {
            SoundManager.instance.PlaySFX(SFX_KYW.DDok2);
            Shake();
            TakeDamage();
        }
    }

    private void Shake()
    {
        isShaking = true;
        transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, 90, false, false)
            .OnComplete(() => isShaking = false);
    }

    private void TakeDamage()
    {
        currentHp--;
        
        if (currentHp <= 0)
        {
            // 파티클 효과 생성
            if (destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            }
            
            // 오브젝트 제거
            Destroy(gameObject);
        }
    }
} 