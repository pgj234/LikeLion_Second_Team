using UnityEngine;

public class SceneChangerOnContact : MonoBehaviour
{
    public string targetSceneName = "YSME";
    public FadeController fadeController;

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
        }
    }
}