using UnityEngine;

public class Candle : MonoBehaviour
{
    [Header("감지 설정")]
    [SerializeField] private float detectionRadius = 5f; // 감지 범위
    [SerializeField] private LayerMask playerLayer; // 플레이어 레이어
    [SerializeField] private GameObject childObject; // 활성화할 자식 오브젝트

    private bool isActivated = false; // 이미 활성화되었는지 여부

    private void Update()
    {
        if (!isActivated)
        {
            // 원 범위 내 플레이어 감지
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
            
            // 플레이어가 감지되면
            if (hitColliders.Length > 0)
            {
                ActivateCandle();
            }
        }
    }

    private void ActivateCandle()
    {
        isActivated = true;
        childObject.SetActive(true);
        SoundManager.instance.PlaySFX(SFX_KYW.CandleLight);
    }

    // 디버그용 기즈모
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
} 