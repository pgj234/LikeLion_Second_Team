using UnityEngine;
using UnityEngine.Rendering.Universal;
public class TitleLight : MonoBehaviour
{

    public Light2D light2D;           // Sprite Light 2D
    public float minIntensity = 0f;
    public float maxIntensity = 1f;
    public float speed = 1f;

    private bool increasing = true;

    void Start()
    {
        if (light2D == null)
            light2D = GetComponent<Light2D>(); // 자동 연결
    }

    void Update()
    {
        if (increasing)
        {
            light2D.intensity += Time.deltaTime * speed;
            if (light2D.intensity >= maxIntensity)
                increasing = false;
        }
        else
        {
            light2D.intensity -= Time.deltaTime * speed;
            if (light2D.intensity <= minIntensity)
                increasing = true;
        }
    }
}
