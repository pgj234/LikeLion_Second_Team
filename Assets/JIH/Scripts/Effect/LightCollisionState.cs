using UnityEngine.Rendering.Universal;
using UnityEngine;

public class LightCollisionState : ObjectState
{
    private Light2D lightComponent;
    private Collider2D lightCollider;
    private bool isLightOn = true;
    private float timer = 0f;
    private const float cycleDuration = 4f; // 한 사이클(켜짐->꺼짐->켜짐) 시간
    private const float maxIntensity = 1f; // 최대 밝기
    private const float minIntensity = 0f; // 최소 밝기
    private const float damageThreshold = 0.5f; // 데미지 적용 기준 밝기
    public virtual void Start()
    {
        lightComponent = GetComponent<Light2D>();
        lightCollider = GetComponent<Collider2D>();
        if (lightComponent == null)
        {
            Debug.LogError("Light component not found!");
        }
    }

    public virtual void Update()
    {
        timer += Time.deltaTime;
        float t = (Mathf.Sin(2f * Mathf.PI * timer / cycleDuration) + 1f) / 2f;
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
        lightComponent.intensity = intensity;

        // 밝기 기준으로 충돌체 활성화
        lightCollider.enabled = intensity >= damageThreshold;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (isLightOn && other.CompareTag("Player"))
        {
            Debug.Log($"Hit Player with {damage} damage");
            // 플레이어 데미지 로직 추가
            // 예: other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        base.OnTriggerEnter2D(other); // 부모 클래스 로직 호출
    }

}
