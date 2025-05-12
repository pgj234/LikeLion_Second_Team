using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ObjectLight : LightInBlackArea
{
    [Space, Header("빛 영역 참조")]
    [SerializeField] private CircleCollider2D circle;
    [SerializeField, Range(0, 1)] private float percent;

    protected override void Awake()
    {
        base.Awake();
        circle = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        light2D.pointLightOuterRadius = OuterRadius;
        light2D.pointLightInnerRadius = Mathf.Clamp(InnerRadius, 0, OuterRadius);
    }

    void Update()
    {
        circle.radius = light2D.pointLightInnerRadius + (light2D.pointLightOuterRadius - light2D.pointLightInnerRadius) * percent;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerLight pLight = collision.GetComponentInChildren<PlayerLight>();
            if (pLight != null)
            {
                pLight.AddLights(this);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerLight pLight = collision.GetComponentInChildren<PlayerLight>();
            if (pLight != null)
            {
                pLight.RemoveLights(this);
            }
        }
    }
}
