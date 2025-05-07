using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInEffect : MonoBehaviour
{
    public Image fadeImage; // 화면을 덮을 검은색 이미지
    public float fadeDuration = 2f; // 페이드 인 지속 시간

    private void Start()
    {
        // 화면이 시작되면 페이드 인 효과를 시작합니다.
        StartCoroutine(FadeIn());
    }

    // 페이드 인 효과
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        // 이미지의 색상을 검은색으로 설정하고 시작
        Color startColor = fadeImage.color;
        startColor.a = 1f; // 처음에는 완전 불투명
        fadeImage.color = startColor;

        // 알파 값을 1에서 0으로 애니메이션 효과를 줍니다.
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 끝났을 때 완전히 투명한 상태로 설정
        fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }
}
