using UnityEngine;

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
                SoundManager.instance.PlaySFX(SFX_PGJ.CaveExit);
            }

            fadeController.FadeOutAndLoad(targetSceneName);
        }
    }
}
