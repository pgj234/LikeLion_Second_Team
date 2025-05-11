using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopCreditText : MonoBehaviour
{
    public MoveCreditText creditText;
    public FadeInOut fade;
    public AudioSource bgm;
    public float fadeTime;
    public string sceneName;

    void Start()
    {
        StartCoroutine(Volume(0, 1, 3));
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeTime);
        StartCoroutine(Volume(1, 0, 4));
        yield return StartCoroutine(fade.StartFadeOut());
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Volume(float start, float end, float time)
    {
        float curTime = 0;
        float percent = 0;
        while (curTime < time)
        {
            curTime += Time.deltaTime;
            percent = curTime / time;
            bgm.volume = Mathf.Lerp(start, end, percent);
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            creditText.isMoving = false;
            StartCoroutine(FadeOut());
        }
    }
}
