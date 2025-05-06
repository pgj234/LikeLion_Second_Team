using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightIntensityController : MonoBehaviour
{
    public Light2D targetLight; // Inspector에서 할당할 2D 라이트
    public float minIntensity = 5f; // 최소 강도
    public float maxIntensity = 10f; // 최대 강도
    public float interval = 2f; // 전환 간격 (초)

    private float timer = 0f;
    private float targetIntensity;
    private float currentIntensity;

    void Start()
    {
        if (targetLight == null)
        {
            targetLight = GetComponent<Light2D>();
        }
        currentIntensity = minIntensity;
        targetIntensity = maxIntensity;
        targetLight.intensity = currentIntensity;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 2초마다 목표 강도 전환
        if (timer >= interval)
        {
            targetIntensity = (targetIntensity == maxIntensity) ? minIntensity : maxIntensity;
            timer = 0f;
        }

        // 부드러운 강도 변화
        currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime);
        targetLight.intensity = currentIntensity;
    }
}