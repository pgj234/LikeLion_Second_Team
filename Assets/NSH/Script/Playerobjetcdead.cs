using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Playerobjetcdead : MonoBehaviour
{
    private bool isDead = false;

    [Header("UI 요소")]
    public GameObject gameOverUI; // 게임 오버 UI (Canvas 자식으로 연결)
    public Button restartButton;  // 재시작 버튼 (Button 컴포넌트로 연결)
    public Text gameOverText;     // 게임 오버 텍스트 (Canvas 자식으로 연결)

    private void Start()
    {
        // 게임 시작 시 UI 비활성화
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        // 버튼에 이벤트 추가 (버튼 클릭 시 재시작)
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartScene);
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("플레이어 사망");

        // 사망 후 1초 뒤에 게임 오버 UI 표시
        Invoke(nameof(ShowGameOverUI), 1f);
    }

    void ShowGameOverUI()
    {
        // 게임 오버 UI 활성화
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // 게임 오버 메시지
        if (gameOverText != null)
        {
            gameOverText.text = "죽었군 자네...";
        }
    }

    void Update()
    {
        // 플레이어가 죽은 상태에서 R 키 입력을 받아서 씬 재시작
        if (isDead && Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
    }

    void RestartScene()
    {
        // 현재 씬을 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}