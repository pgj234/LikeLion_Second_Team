using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Volumetric_Flicker : MonoBehaviour
{
    [SerializeField] float avgVolIntensity = 0.01f;

    Light2D light => GetComponent<Light2D>();

    void Update()
    {
        light.volumeIntensity = avgVolIntensity * Mathf.Abs(Mathf.Sin(Time.time)) + 0.005f;
    }
}
