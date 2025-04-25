using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightCollisionState : ObjectState
{
    private Light2D lightComponent;
    private Collider2D lightCollider;
    private bool isLightOn = true;
    private const float damageThreshold = 0.5f; // 데미지 적용 기준 밝기
    public float damage = 10f; // 데미지 값

    public virtual void Start()
    {
        lightComponent = GetComponent<Light2D>();
        lightCollider = GetComponent<Collider2D>();
        if (lightComponent == null)
        {
            Debug.LogError($"Light2D component not found on {gameObject.name}. Disabling script.");
            enabled = false;
        }
        if (lightCollider == null)
        {
            Debug.LogError($"Collider2D component not found on {gameObject.name}. Disabling script.");
            enabled = false;
        }
        else
        {
            lightCollider.enabled = false; // 초기 상태: Collider 비활성화
            Debug.Log($"Initialized {gameObject.name}: Collider disabled");
        }
    }

    public virtual void Update()
    {
        // LightObject의 FadeIn/FadeOut에서 Collider 제어하므로 Update에서는 추가 제어 최소화
        if (lightComponent != null && lightCollider != null)
        {
            bool shouldEnable = lightComponent.intensity >= damageThreshold;
            if (lightCollider.enabled != shouldEnable)
            {
                lightCollider.enabled = shouldEnable;
                Debug.Log($"Collider {(shouldEnable ? "enabled" : "disabled")} on {gameObject.name}, intensity: {lightComponent.intensity} (Update)");
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (isLightOn && other.CompareTag("Player") && lightComponent != null && lightCollider != null && lightComponent.intensity >= damageThreshold && lightCollider.enabled)
        {
            Debug.Log($"Hit Player with {damage} damage at intensity {lightComponent.intensity} on {gameObject.name}");
            PlayerManager.Instance.TakeDamage((int)damage); // PlayerManager로 데미지 전달
        }
        base.OnTriggerEnter2D(other);
    }
}