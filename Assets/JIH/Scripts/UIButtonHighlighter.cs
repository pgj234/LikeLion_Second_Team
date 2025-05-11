using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject arrow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlaySFX(SFX_KYW.TiltleHover);
        if (arrow != null)
            arrow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (arrow != null)
            arrow.SetActive(false);
    }
}
