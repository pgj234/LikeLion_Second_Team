using UnityEngine;
using UnityEngine.SceneManagement;

public class Playerobjetcdead : MonoBehaviour
{
    private bool isDead = false;

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("플레이어 사망");

        // 사망 연출, 이펙트, 애니메이션 등을 여기에 추가 가능

        // 1초 후 씬 재시작
        Invoke(nameof(RestartScene), 1f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}