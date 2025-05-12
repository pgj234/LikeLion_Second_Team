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

    [Header("리스폰 위치")]
    public Vector3 respawnPosition = new Vector3(-9.88f, -1.81f, 0f); // 리스폰할 위치 설정

    private Rigidbody rb; // Rigidbody 컴포넌트

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기

        // 게임 시작 시 UI 비활성화
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        //// 버튼에 이벤트 추가 (버튼 클릭 시 재시작)
        //if (restartButton != null)
        //{
        //    restartButton.onClick.AddListener(RestartGame);
        //}
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("플레이어 사망");

        // 사망 후 1초 뒤에 게임 오버 UI 표시
        Invoke(nameof(ShowGameOverUI), 1f);

        // 물리 엔진 멈추기 (Rigidbody의 중력과 상호작용 차단)
        if (rb != null)
        {
            rb.isKinematic = true; // 물리엔진 비활성화
            rb.linearVelocity = Vector3.zero; // 속도 초기화
            rb.angularVelocity = Vector3.zero; // 회전 속도 초기화
        }
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

    //void Update()
    //{
    //    // 플레이어가 죽은 상태에서 R 키 입력을 받아서 리스폰
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        RespawnPlayer();
    //    }
    //}

    void RespawnPlayer()
    {
        // 플레이어를 리스폰 위치로 이동
        transform.position = respawnPosition;

        // 물리 엔진 복원 (Rigidbody의 중력과 상호작용 복원)
        if (rb != null)
        {
            rb.isKinematic = false; // 물리 엔진 활성화
            rb.angularVelocity = Vector3.zero; // 속도 초기화
            rb.angularVelocity = Vector3.zero; // 회전 속도 초기화
        }

        // 플레이어 사망 상태 초기화
        isDead = false;

        // 게임 오버 UI 비활성화
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    //void RestartGame()
    //{
    //    // 씬을 재시작하는 대신 리스폰 함수 호출
    //    RespawnPlayer();
    //}
}
