using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string targetSceneName; // 이동할 씬 이름
    [SerializeField] private float delay = 0.5f; // 텔레포트 딜레이
    private bool isPlayerInRange = false;
    private Player player; // 플레이어 인스턴스 저장
    private float timer = 0f;

    private void Update()
    {
        if (isPlayerInRange && InputManager.instance.upHold)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                TeleportToScene();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            player = collision.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            player = null;
            timer = 0f;
        }
    }

    private void TeleportToScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneChager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogWarning("이동할 씬 이름이 설정되지 않았습니다.");
        }
    }
} 