using UnityEngine;
using UnityEngine.Rendering.Universal;

public class InteractionObjectLightFlicker : MonoBehaviour
{
    [SerializeField] float speed;

    float originalIntensity;

    Light2D light => GetComponent<Light2D>();

    void Awake()
    {
        originalIntensity = light.intensity;
    }

    void Update()
    {
        float sinValue = Mathf.Sin(Time.time * speed);
        float cosValue = Mathf.Cos(Time.time * speed);

        light.intensity = originalIntensity + sinValue * 0.7f;

        light.color = Color.Lerp(new Color(1, 1, sinValue), new Color(1, 1, cosValue), speed);
    }
}
