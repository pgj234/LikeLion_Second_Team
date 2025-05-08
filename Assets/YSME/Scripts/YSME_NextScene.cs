using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YSME_NextScene : MonoBehaviour
{
    [SerializeField] private FadeInOut fadeController;
    [SerializeField] private string nextSceneName;

    IEnumerator MoveNextScene()
    {
        InputManager.instance.InputStop();
        yield return StartCoroutine(fadeController.StartFadeOut());
        yield return new WaitForSeconds(0.5f);
        InputManager.instance.InputStart();
        SceneManager.LoadScene(nextSceneName);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(MoveNextScene());
        }
    }
}
