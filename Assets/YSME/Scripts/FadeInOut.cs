using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeInOut : MonoBehaviour
{
    [Header("페이드 설정")]
    private Image image;
    [SerializeField] private Color fadeInColor;
    [SerializeField] private float fadeInTime;
    [SerializeField] private Color fadeOutColor;
    [SerializeField] private float fadeOutTime;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        StopCoroutine(StartFadeOut());
        StartCoroutine(StartFadeIn());
    }

    public void FadeOut()
    {
        StopCoroutine(StartFadeIn());
        StartCoroutine(StartFadeOut());
    }

    public IEnumerator StartFadeIn()
    {
        gameObject.SetActive(true);
        float curTime = 0;
        float percent = 0;
        while (curTime < fadeInTime)
        {
            curTime += Time.deltaTime;
            percent = curTime / fadeInTime;
            image.color = Vector4.Lerp(fadeOutColor, fadeInColor, percent);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    public IEnumerator StartFadeOut()
    {
        gameObject.SetActive(true);
        float curTime = 0;
        float percent = 0;
        while (curTime < fadeOutTime)
        {
            curTime += Time.deltaTime;
            percent = curTime / fadeOutTime;
            image.color = Vector4.Lerp(fadeInColor, fadeOutColor, percent);
            yield return null;
        }
    }
}
