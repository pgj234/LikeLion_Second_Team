using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("체력 UI")]
    [SerializeField] private List<Image> heartImages = new List<Image>();

    [Header("스태미나 UI")]
    [SerializeField] private Image staminaBar;

    [Header("플레이어 표정 UI")]
    [SerializeField] private List<GameObject> playerExpressions = new List<GameObject>();

    private PlayerManager playerManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        playerManager = PlayerManager.Instance;
        if (playerManager != null)
        {
            // 이벤트 구독
            EventManager.instance.OnHealthChanged += UpdateHealthUI;
            EventManager.instance.OnStaminaChanged += UpdateStaminaUI;
            EventManager.instance.OnExpressionChanged += UpdateExpression;

            // 초기 UI 상태 설정
            UpdateHealthUI(playerManager.CurrentHealth);
            UpdateStaminaUI(playerManager.CurrentStamina);
            UpdateExpression(4); // 기본 표정으로 설정
        }
    }

    private void OnDestroy()
    {
        if (EventManager.instance != null)
        {
            // 이벤트 구독 해제
            EventManager.instance.OnHealthChanged -= UpdateHealthUI;
            EventManager.instance.OnStaminaChanged -= UpdateStaminaUI;
            EventManager.instance.OnExpressionChanged -= UpdateExpression;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager != null)
        {
            UpdateStaminaUI(playerManager.CurrentStamina);
        }
    }

    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            heartImages[i].gameObject.SetActive(i < currentHealth);
        }
    }

    public void UpdateStaminaUI(float currentStamina)
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = currentStamina / playerManager.MaxStamina;
        }
    }

    public void UpdateExpression(int expressionIndex)
    {
        // 모든 표정 비활성화
        foreach (var expression in playerExpressions)
        {
            expression.SetActive(false);
        }

        // 선택된 표정만 활성화
        if (expressionIndex >= 0 && expressionIndex < playerExpressions.Count)
        {
            playerExpressions[expressionIndex].SetActive(true);
        }
    }
}
