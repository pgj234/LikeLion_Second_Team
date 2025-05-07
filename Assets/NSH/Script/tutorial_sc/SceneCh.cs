using UnityEngine;
using UnityEngine.UI;  // UI 요소를 사용하려면 이 네임스페이스가 필요합니다.

public class SceneChangerOnContact : MonoBehaviour
{
    public string targetSceneName = "YSME";
    public FadeController fadeController;

    [Header("UI 요소")]
    public GameObject messageBubble;  // 메시지를 표시할 UI 오브젝트 (말풍선)
    public Text messageText;          // 메시지를 담을 Text 컴포넌트

    private void Start()
    {
        // 게임 시작 시 말풍선 UI 비활성화
        if (messageBubble != null)
        {
            messageBubble.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // 아이템이 있어야만 전환 허용
        if (GameState.Instance != null && GameState.Instance.hasKeyItem)
        {
            Debug.Log("씬 전환 준비 중...");
            fadeController.FadeOutAndLoad(targetSceneName);
        }
        else
        {
            Debug.Log("아이템이 없어서 전환 불가!");

            // 아이템이 없으면 말풍선 UI 활성화
            ShowMessage("아이템이 없어서 전환 불가!");
        }
    }

    // 메시지를 일정 시간 후 자동으로 비활성화하는 함수
    void ShowMessage(string message)
    {
        if (messageBubble != null && messageText != null)
        {
            messageBubble.SetActive(true);  // 말풍선 UI 활성화
            messageText.text = message;     // 메시지 설정

            // 2초 후에 메시지를 숨깁니다.
            Invoke("HideMessage", 2f);
        }
    }

    void HideMessage()
    {
        if (messageBubble != null)
        {
            messageBubble.SetActive(false);  // 말풍선 UI 비활성화
        }
    }
}
