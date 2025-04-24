using UnityEngine;
using DG.Tweening;

public class DoTweenManager : MonoBehaviour
{
    public static DoTweenManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 오브젝트를 서서히 사라지게 하는 함수
    // target: 페이드아웃할 오브젝트
    // duration: 페이드아웃 시간
    public void FadeOut(GameObject target, float duration)
    {
        // SpriteRenderer가 있는 경우
        SpriteRenderer spriteRenderer = target.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.DOFade(0f, duration)
                .OnComplete(() => target.SetActive(false));
            return;
        }

        // SpriteRenderer가 없는 경우 기본 페이드아웃
        target.transform.DOScale(Vector3.zero, duration)
            .OnComplete(() => target.SetActive(false));
    }

    // 오브젝트를 특정 방향으로 밀어내는 함수
    // target: 밀어낼 오브젝트
    // direction: 밀어낼 방향
    public void PushObject(GameObject target, Vector2 direction)
    {
        // 현재 위치에서 방향으로 2유닛 밀어내기
        Vector3 targetPosition = target.transform.position + new Vector3(direction.x, direction.y, 0) * 2f;
        
        // 자연스러운 이동을 위한 시퀀스
        Sequence pushSequence = DOTween.Sequence();
        
        // 이동
        pushSequence.Append(target.transform.DOMove(targetPosition, 0.5f)
            .SetEase(Ease.OutBack));
        
        // 약간의 회전 효과 추가
        // pushSequence.Join(target.transform.DORotate(new Vector3(0, 0, Random.Range(-15f, 15f)), 0.5f)
        //     .SetEase(Ease.OutQuad));
    }
} 