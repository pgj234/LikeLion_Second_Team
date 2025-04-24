using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager instance = null;

    public PlayerEvents playerEvents;
    public EnemyEvents EnemyEvents;

    // 데미지 이벤트
    public event Action<int> OnPlayerDamaged;

    // UI 관련 이벤트
    public event Action<int> OnHealthChanged;
    public event Action<float> OnStaminaChanged;
    public event Action<int> OnExpressionChanged;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        playerEvents = new PlayerEvents();
        EnemyEvents = new EnemyEvents();
    }

    // 데미지 이벤트 발생 메서드
    public void PublishPlayerDamaged(int damage)
    {
        OnPlayerDamaged?.Invoke(damage);
    }

    // UI 이벤트 발생 메서드
    public void PublishHealthChanged(int health)
    {
        OnHealthChanged?.Invoke(health);
    }

    public void PublishStaminaChanged(float stamina)
    {
        OnStaminaChanged?.Invoke(stamina);
    }

    public void PublishExpressionChanged(int expressionIndex)
    {
        OnExpressionChanged?.Invoke(expressionIndex);
    }
}
