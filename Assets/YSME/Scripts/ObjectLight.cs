using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ObjectLight : LightInBlackArea
{
    private PlayerLight player;
    [Space, Header("빛 영역 참조")]
    [SerializeField] private CircleCollider2D circle;

    protected override void Awake()
    {
        base.Awake();
        circle = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        light2D.pointLightOuterRadius = OuterRadius;
        light2D.pointLightInnerRadius = Mathf.Clamp(InnerRadius, 0, OuterRadius);
        circle.radius = InnerRadius + (OuterRadius - InnerRadius) / 2;
    }

    void Update()
    {
        if (player != null)
        {
            player.isInLight = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponentInChildren<PlayerLight>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.isInLight = false;
            player = null;
        }
    }
}
