using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class LightInBlackArea : MonoBehaviour
{
    [Header("컴포넌트 참조")]
    [SerializeField] protected Light2D light2D;

    [Space, Header("빛(마스크) 초기 변수")]
    [SerializeField, Range(0, 20f)] protected float OuterRadius = 5;
    [SerializeField, Range(0, 20f)] protected float InnerRadius = 3;
    [SerializeField] protected bool isEnemy;
    public bool IsEnemy => isEnemy;

    protected virtual void Awake()
    {
        if (TryGetComponent(out Light2D _light2D))
        {
            light2D = _light2D;
        }
        else
        {
            light2D = gameObject.AddComponent<Light2D>();
            light2D.lightType = Light2D.LightType.Point;
        }
    }
}
