using UnityEngine;

public class LightCollisionState : ObjectState
{
    public UnityEngine.Light lightSource;
    public float fadeSpeed = 2f;
    public float targetIntensity = 1f; // Target intensity for the light
    public bool isLightOn = false;

    public virtual void Start()
    {
        lightSource = GetComponent<UnityEngine.Light>();
        lightSource.intensity = 0f; // Start with light off
    }

    public virtual void Update()
    {
        FadeInOut();
    }

    public void FadeInOut()
    {
        float currentIntensity = lightSource.intensity;
        float newIntensity = Mathf.MoveTowards(currentIntensity, isLightOn ? targetIntensity : 0f, fadeSpeed * Time.deltaTime);
        lightSource.intensity = newIntensity;
    }
}
