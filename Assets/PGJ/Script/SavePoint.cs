using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SavePoint : MonoBehaviour
{
    [SerializeField] float maxIntensity;

    float originalIntensity;

    Light2D light => GetComponent<Light2D>();

    float saveEffectTime = 0.15f;
    float saveEffectTimer;

    bool isOn = false;

    void Awake()
    {
        originalIntensity = light.intensity;
    }

    void Start()
    {
        light.color = Color.cyan;
        light.enabled = false;
    }

    void Update()
    {
        if (true == isOn)
        {
            saveEffectTimer -= Time.deltaTime;

            light.intensity = Mathf.Lerp(light.intensity, maxIntensity, saveEffectTime * 100 * Time.deltaTime);

            if (saveEffectTimer < 0)
            {
                light.enabled = false;
                isOn = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (false == isOn)
            {
                isOn = true;

                light.intensity = originalIntensity;

                saveEffectTimer = saveEffectTime;

                // SoundManager.instance.PlaySFX(세이브 효과음);

                light.enabled = true;
            }
        }
    }
}
