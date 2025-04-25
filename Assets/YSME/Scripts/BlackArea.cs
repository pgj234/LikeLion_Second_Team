using UnityEngine;

public class BlackArea : MonoBehaviour
{
    [Header("테두리 블러 처리")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField, Range(0, 1)] private float UpAlpha;
    [SerializeField, Range(-1, 0)] private float DownAlpha;
    [SerializeField, Range(-1, 0)] private float LeftAlpha;
    [SerializeField, Range(0, 1)] private float RightAlpha;

    void Start()
    {
        spriteRenderer.material.SetFloat("_UpAlpha", UpAlpha);
        spriteRenderer.material.SetFloat("_DownAlpha", DownAlpha);
        spriteRenderer.material.SetFloat("_LeftAlpha", LeftAlpha);
        spriteRenderer.material.SetFloat("_RightAlpha", RightAlpha);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInChildren<PlayerLight>().AddArea(this);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInChildren<PlayerLight>().RemoveArea(this);
        }
    }
}
