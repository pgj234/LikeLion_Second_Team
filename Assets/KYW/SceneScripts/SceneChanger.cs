using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f; // 전환 딜레이
    private float timer = 0f;
    private bool isTriggered = false;

    private void Update()
    {
        if (isTriggered)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                // 씬 전환
                SceneManager.LoadScene("NSH");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
            timer = 0f;
        }
    }
} 