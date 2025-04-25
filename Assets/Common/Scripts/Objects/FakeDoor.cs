using UnityEngine;
using DG.Tweening;

public class FakeDoor : MonoBehaviour
{
    [SerializeField] private float delay = 0.5f; // 애니메이션 딜레이
    private bool isPlayerInRange = false;
    private Player player; // 플레이어 인스턴스 저장
    private float timer = 0f;

    [Header("문 애니메이션 설정")]
    [SerializeField] private bool isRotate = false; // 회전 애니메이션 사용 여부
    [SerializeField] private bool isFadeOut = false; // 페이드아웃 애니메이션 사용 여부

    private void Update()
    {
        if (isPlayerInRange && InputManager.instance.upHold)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                PlayDoorAnimation();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            player = collision.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            player = null;
            timer = 0f;
        }
    }

    private void PlayDoorAnimation()
    {
        Sequence doorSequence = DOTween.Sequence();

        // 회전 애니메이션
        if (isRotate)
        {
            doorSequence.Join(transform.DORotate(new Vector3(0, 90, 0), 1f)
                .SetEase(Ease.OutQuad));
        }

        // 페이드아웃 애니메이션
        if (isFadeOut)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                doorSequence.Join(spriteRenderer.DOFade(0f, 1f));
            }
        }
    }
} 