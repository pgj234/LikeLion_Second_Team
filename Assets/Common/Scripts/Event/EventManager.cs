using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager instance = null;

    public PlayerEvents playerEvents;
    public EnemyEvents EnemyEvents;

    // 플레이어 데미지 이벤트
    public event Action<int> OnPlayerDamaged;
    // 플레이어 리스폰 이벤트
    public event Action OnPlayerRespawned;

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

    // 플레이어 리스폰 이벤트 실행 메서드
    public void PublishPlayerRespawned()
    {
        OnPlayerRespawned?.Invoke();
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
