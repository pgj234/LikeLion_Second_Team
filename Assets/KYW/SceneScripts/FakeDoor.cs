using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class FakeDoor : MonoBehaviour
{
    [SerializeField] private float delay = 0.5f; //문 딜레이
    private bool isPlayerInRange = false;
    private Player player; // 플레이어 인스턴스 저장
    private float timer = 0f;

    [Header("문 애니메이션 설정")]
    [Header("회전 설정")]
    [SerializeField] private bool isRotateX = false; // X축 회전 사용 여부
    [SerializeField] private bool isRotateY = false; // Y축 회전 사용 여부
    [SerializeField] private bool isRotateZ = false; // Z축 회전 사용 여부
    [SerializeField] private float rotateDuration = 2f; // 회전 시간

    [Header("페이드아웃 설정")]
    [SerializeField] private bool isFadeOut = false; // 페이드아웃 애니메이션 사용 여부

    [Header("이동 설정")]
    [SerializeField] private bool isMove = false; // 이동 애니메이션 사용 여부
    [SerializeField] private Transform targetTransform; // 이동할 목표 지점
    [SerializeField] private float moveDuration = 1f; // 이동 시간

    private void Update()
    {
        if (isPlayerInRange && InputManager.instance.upHold)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                PlayAnimations();
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

    private void PlayAnimations()
    {
        Sequence sequence = DOTween.Sequence();

        // 회전 애니메이션
        Vector3 rotation = Vector3.zero;
        if (isRotateX) rotation.x = 1080f;
        if (isRotateY) rotation.y = 1080f;
        if (isRotateZ) rotation.z = 1080f;

        if (rotation != Vector3.zero)
        {
            sequence.Join(transform.DORotate(rotation, rotateDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuad));
        }

        // 페이드아웃 애니메이션
        if (isFadeOut)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                sequence.Join(spriteRenderer.DOFade(0f, 1f)
                    .SetEase(Ease.OutQuad));
            }
        }

        // 이동 애니메이션
        if (isMove && targetTransform != null)
        {
            sequence.Join(transform.DOMove(targetTransform.position, moveDuration)
                .SetEase(Ease.OutBack));
        }
    }
} 