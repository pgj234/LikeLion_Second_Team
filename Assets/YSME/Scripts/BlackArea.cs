using UnityEngine;

public class BlackArea : MonoBehaviour
{
    private PlayerLight player;

    [Header("테두리 블러 처리")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField, Range(0, 0.999f)] private float UpAlpha;
    [SerializeField, Range(-1, -0.001f)] private float DownAlpha;
    [SerializeField, Range(-1, -0.001f)] private float LeftAlpha;
    [SerializeField, Range(0, 0.999f)] private float RightAlpha;

    void Start()
    {
        spriteRenderer.material.SetFloat("_UpAlpha", UpAlpha);
        spriteRenderer.material.SetFloat("_DownAlpha", DownAlpha);
        spriteRenderer.material.SetFloat("_LeftAlpha", LeftAlpha);
        spriteRenderer.material.SetFloat("_RightAlpha", RightAlpha);
    }

    void Update()
    {
        if (player != null)
        {
            player.isInBlackArea = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponentInChildren<PlayerLight>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.isInBlackArea = false;
            player = null;
        }
    }
}
