using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YSME_NextScene : MonoBehaviour
{
    [SerializeField] private FadeInOut fadeController;

    IEnumerator MoveNextScene()
    {
        InputManager.instance.InputStop();
        yield return StartCoroutine(fadeController.StartFadeOut());
        yield return new WaitForSeconds(0.5f);
        InputManager.instance.InputStart();
        SceneManager.LoadScene("Team9");
        SceneManager.LoadScene("Final_2 1111", LoadSceneMode.Additive);
        SceneManager.LoadScene("JIH_Final 1111", LoadSceneMode.Additive);
        SceneManager.LoadScene("KYW_FinalLava1111", LoadSceneMode.Additive);
        SceneManager.LoadScene("PGJ_Final 1111", LoadSceneMode.Additive);
        SceneManager.LoadScene("YSME_Final 1111", LoadSceneMode.Additive);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(MoveNextScene());
        }
    }
}
