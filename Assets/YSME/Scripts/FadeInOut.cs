using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class FadeInOut : MonoBehaviour
{
    private Image image;

    void Start()
    {
        // EventManager.instance.OnPlayerRespawned
    }

    public void FadeIn()
    {
        StartCoroutine(StartFadeIn());
    }

    public void FadeOut()
    {
        StartCoroutine(StartFadeOut());
    }

    IEnumerator StartFadeIn()
    {
        yield return null;
    }

    IEnumerator StartFadeOut()
    {
        yield return null;
    }
}
