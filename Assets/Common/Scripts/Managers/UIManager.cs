using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("체력 UI")]
    [SerializeField] private List<Image> heartImages = new List<Image>();

    [Header("스태미나 UI")]
    [SerializeField] private Image staminaBar;

    [Header("플레이어 표정 UI")]
    [SerializeField] private List<GameObject> playerExpressions = new List<GameObject>();

    [Header("설명 텍스트 UI")]
    [SerializeField] private TextMeshProUGUI descriptionText; // 공유 텍스트
    [SerializeField] private float fadeDuration = 0.5f; // 페이드 지속 시간

    private PlayerManager playerManager;
    private int activeTriggers = 0; // 활성 트리거 수

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

        // 설명 텍스트 초기화
        if (descriptionText != null)
        {
            descriptionText.alpha = 0f;
            descriptionText.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("descriptionText is not assigned in UIManager!");
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

    private void Update()
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
        foreach (var expression in playerExpressions)
        {
            expression.SetActive(false);
        }

        if (expressionIndex >= 0 && expressionIndex < playerExpressions.Count)
        {
            playerExpressions[expressionIndex].SetActive(true);
        }
    }

    public void ShowDescription(string text)
    {
        if (descriptionText != null)
        {
            activeTriggers++;
            descriptionText.DOKill(); // 기존 애니메이션 종료
            descriptionText.text = text;
            descriptionText.DOFade(1f, fadeDuration).SetEase(Ease.InQuad);
            Debug.Log($"Showing description: {text}, Active Triggers: {activeTriggers}");
        }
    }

    public void HideDescription()
    {
        if (descriptionText != null)
        {
            activeTriggers = Mathf.Max(0, activeTriggers - 1);
            if (activeTriggers == 0)
            {
                descriptionText.DOKill();
                descriptionText.DOFade(0f, fadeDuration).SetEase(Ease.OutQuad);
                Debug.Log("Hiding description");
            }
        }
    }
}