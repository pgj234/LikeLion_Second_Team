using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightObject : LightCollisionState
{
    private ParticleSystem particleSystem; // �ڽ� ������Ʈ�� ��ƼŬ �ý��� (�̻��, ȣȯ�� ����)
    private Light2D lightComponent; // URP Light2D ������Ʈ
    private Collider2D lightCollider; // Collider2D ����
    private bool isLightOn = false;
    public float delayTime = 0.5f; // �� ���� ������ �ð� (LightGroupTrigger���� ���)
    private float fadeDuration = 1f; // ���̵� ��/�ƿ� �ð� (��)
    private const float damageThreshold = 0.5f; // LightCollisionState�� ����ȭ

    private void OnEnable()
    {
        // GameObject Ȱ��ȭ �� �ʱ�ȭ
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
        lightComponent.enabled = true; // �׻� Ȱ��ȭ, intensity�� ����
        lightComponent.intensity = 0f;
        if (lightCollider != null)
        {
            lightCollider.enabled = false; // �ʱ� ����: Collider ��Ȱ��ȭ
            Debug.Log($"Initialized {gameObject.name}: Collider disabled, intensity: {lightComponent.intensity}");
        }
    }

    public override void Update()
    {
        // LightCollisionState�� intensity ���� ����
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
            StopAllCoroutines(); // ���� ���̵� ����
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
            // Collider ����ȭ
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
            StopAllCoroutines(); // ���� ���̵� ����
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
            // Collider ����ȭ
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