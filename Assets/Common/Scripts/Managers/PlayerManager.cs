using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public Player player;
    [Header("체력 설정")]
    [SerializeField] private int maxHealth = 5;
    private int currentHealth;

    [Header("스태미나 설정")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaRegenRate = 5f;
    private float currentStamina;

    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }
    public float CurrentStamina { get; private set; }
    public float MaxStamina { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 플레이어 찾기
            player = FindFirstObjectByType<Player>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
        CurrentStamina = currentStamina;
        MaxStamina = maxStamina;
        
        // 이벤트 구독
        EventManager.instance.OnPlayerDamaged += TakeDamage;
    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        if (EventManager.instance != null)
        {
            EventManager.instance.OnPlayerDamaged -= TakeDamage;
        }
    }

    private void Update()
    {
        RegenerateStamina();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        CurrentHealth = currentHealth;
        EventManager.instance.PublishHealthChanged(currentHealth);
        
        // 체력에 따른 표정 변경 (예시)
        int expressionIndex = Mathf.Clamp(currentHealth, 0, 4);
        EventManager.instance.PublishExpressionChanged(expressionIndex);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        CurrentHealth = currentHealth;
        EventManager.instance.PublishHealthChanged(currentHealth);
    }

    public bool UseStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            CurrentStamina = currentStamina;
            EventManager.instance.PublishStaminaChanged(currentStamina);
            return true;
        }
        return false;
    }

    private void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRegenRate * Time.deltaTime);
            CurrentStamina = currentStamina;
            EventManager.instance.PublishStaminaChanged(currentStamina);
        }
    }

    private void Die()
    {
        // 플레이어 사망 처리
        Debug.Log("플레이어 사망");
    }
}
