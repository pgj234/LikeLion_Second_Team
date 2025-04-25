using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SavePoint : MonoBehaviour
{
    [SerializeField] float intensityScale;
    [SerializeField] float scaleSize;
    [SerializeField] float speed;

    Vector3 originalLocalScale;
    float originalIntensity;

    Light2D light => GetComponent<Light2D>();

    float saveEffectTime = 1;
    float saveEffectTimer;

    bool isStop = false;

    void Awake()
    {
        originalLocalScale = transform.localScale;
        originalIntensity = light.intensity;
    }

    void Update()
    {
        if (false == isStop)
        {
            light.intensity = originalIntensity + Mathf.Sin(Time.time * speed) * intensityScale;

            transform.localScale = originalLocalScale + Vector3.one * Mathf.Sin(Time.time * speed) * scaleSize * 0.1f;
        }
        else
        {
            saveEffectTimer -= Time.deltaTime;

            if (saveEffectTimer < 0)
            {
                light.color = Color.white;
                isStop = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (false == isStop)
            {
                isStop = true;

                light.color = Color.yellow;
                light.intensity = originalIntensity + intensityScale;
                transform.localScale = originalLocalScale * 2.5f;

                // SoundManager.instance.PlaySFX(세이브 효과음);

                saveEffectTimer = saveEffectTime;
            }
        }
    }
}
