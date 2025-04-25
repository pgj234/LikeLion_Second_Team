using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightObject : LightCollisionState
{
    private ParticleSystem particleSystem; // 자식 오브젝트의 파티클 시스템 (미사용, 호환성 유지)
    private Light2D lightComponent; // URP Light2D 컴포넌트
    private Collider2D lightCollider; // Collider2D 참조
    private bool isLightOn = false;
    public float delayTime = 0.5f; // 빛 켜짐 딜레이 시간 (LightGroupTrigger에서 사용)
    private float fadeDuration = 1f; // 페이드 인/아웃 시간 (초)
    private const float damageThreshold = 0.5f; // LightCollisionState와 동기화

    private void OnEnable()
    {
        // GameObject 활성화 시 초기화
        InitializeComponents();
    }

    public override void Start()
    {
        base.Start();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        InitializeComponents();

        if (particleSystem != null)
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void InitializeComponents()
    {
        if (lightComponent == null)
        {
            lightComponent = GetComponent<Light2D>();
            if (lightComponent == null)
            {
                Debug.LogError($"Light2D component missing on {gameObject.name}. Disabling script.");
                enabled = false;
                return;
            }
        }

        if (lightCollider == null)
        {
            lightCollider = GetComponent<Collider2D>();
            if (lightCollider == null)
            {
                Debug.LogError($"Collider2D component missing on {gameObject.name}.");
            }
        }

        isLightOn = false;
        lightComponent.enabled = true; // 항상 활성화, intensity로 제어
        lightComponent.intensity = 0f;
        if (lightCollider != null)
        {
            lightCollider.enabled = false; // 초기 상태: Collider 비활성화
            Debug.Log($"Initialized {gameObject.name}: Collider disabled, intensity: {lightComponent.intensity}");
        }
    }

    public override void Update()
    {
        // LightCollisionState의 intensity 변동 방지
    }

    public void TurnOnLight()
    {
        if (!isLightOn)
        {
            if (lightComponent == null)
            {
                InitializeComponents();
                if (lightComponent == null)
                {
                    Debug.LogError($"Cannot turn on light: Light2D component is null on {gameObject.name}");
                    return;
                }
            }
            StopAllCoroutines(); // 기존 페이드 중지
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        if (lightComponent == null)
        {
            Debug.LogError($"FadeIn failed: Light2D component is null on {gameObject.name}");
            yield break;
        }

        isLightOn = true;
        float elapsed = 0f;
        float startIntensity = lightComponent.intensity;
        float targetIntensity = 1f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            lightComponent.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
            // Collider 동기화
            if (lightCollider != null && lightComponent.intensity >= damageThreshold && !lightCollider.enabled)
            {
                lightCollider.enabled = true;
                Debug.Log($"Collider enabled on {gameObject.name}, intensity: {lightComponent.intensity}");
            }
            yield return null;
        }

        lightComponent.intensity = targetIntensity;
        if (lightCollider != null && lightComponent.intensity >= damageThreshold)
        {
            lightCollider.enabled = true;
            Debug.Log($"Collider enabled on {gameObject.name}, intensity: {lightComponent.intensity}");
        }
    }

    public void TurnOffLight()
    {
        if (isLightOn)
        {
            if (lightComponent == null)
            {
                InitializeComponents();
                if (lightComponent == null)
                {
                    Debug.LogError($"Cannot turn off light: Light2D component is null on {gameObject.name}");
                    return;
                }
            }
            StopAllCoroutines(); // 기존 페이드 중지
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        if (lightComponent == null)
        {
            Debug.LogError($"FadeOut failed: Light2D component is null on {gameObject.name}");
            yield break;
        }

        isLightOn = false;
        float elapsed = 0f;
        float startIntensity = lightComponent.intensity;
        float targetIntensity = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            lightComponent.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
            // Collider 동기화
            if (lightCollider != null && lightComponent.intensity < damageThreshold && lightCollider.enabled)
            {
                lightCollider.enabled = false;
                Debug.Log($"Collider disabled on {gameObject.name}, intensity: {lightComponent.intensity}");
            }
            yield return null;
        }

        lightComponent.intensity = targetIntensity;
        if (lightCollider != null && lightCollider.enabled)
        {
            lightCollider.enabled = false;
            Debug.Log($"Collider disabled on {gameObject.name}, intensity: {lightComponent.intensity}");
        }
    }

    public bool IsLightOn => isLightOn;
}