using UnityEngine;
using UnityEngine.UI;  // UI 요소를 사용하려면 이 네임스페이스가 필요합니다.

public class NextScene_PGJ : MonoBehaviour
{
    public string targetSceneName = "JIH";
    public FadeController fadeController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (0 == string.Compare(targetSceneName, "PGJ_End"))
            {
                SoundManager.instance.PlaySFX(SFX_PGJ.ExitComplete);
            }

            fadeController.FadeOutAndLoad(targetSceneName);
        }
    }
}
