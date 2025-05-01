using UnityEngine;
using DG.Tweening;
using TMPro;

/// DOTween 애니메이션을 관리하는 매니저 클래스

/// Ease 타입 설명:
/// 1. In 타입
///    - Ease.InQuad: 천천히 시작해서 빠르게 끝남
///    - Ease.InBack: 천천히 시작해서 목표를 지나치고 돌아옴
/// 
/// 2. Out 타입
///    - Ease.OutQuad: 빠르게 시작해서 천천히 끝남 (자연스러운 감속)
///    - Ease.OutBack: 빠르게 시작해서 목표를 지나치고 돌아옴 (튕기는 효과)
/// 
/// 3. InOut 타입
///    - Ease.InOutQuad: 천천히 시작해서 중간에 빠르게 움직이다가 천천히 끝남
///    - Ease.InOutBack: 시작과 끝에서 목표를 지나치는 효과

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
    // 오브젝트를 서서히 나타나게 하는 함수
    public void FadeIn(GameObject target, float duration)
    {
        target.SetActive(true); // 오브젝트 활성화

        // TextMeshProUGUI가 있는 경우
        TextMeshProUGUI tmpText = target.GetComponent<TextMeshProUGUI>();
        if (tmpText != null)
        {
            tmpText.alpha = 0f; // 초기 투명
            tmpText.DOFade(1f, duration).SetEase(Ease.InQuad);
            return;
        }

        // SpriteRenderer가 있는 경우
        SpriteRenderer spriteRenderer = target.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            spriteRenderer.DOFade(1f, duration).SetEase(Ease.InQuad);
            return;
        }

        // 기본 페이드인 (스케일 증가)
        target.transform.localScale = Vector3.zero;
        target.transform.DOScale(Vector3.one, duration).SetEase(Ease.InQuad);
    }

    // 오브젝트를 특정 방향으로 밀어내는 함수
    // target: 밀어낼 오브젝트
    // direction: 밀어낼 방향
    public void PushObject(GameObject target, Vector2 direction)
    {
        // 현재 위치에서 방향으로 2유닛 밀어내기
        Vector3 targetPosition = target.transform.position + new Vector3(direction.x, direction.y, 0) * 2f;
        
        // Sequence: 여러 애니메이션을 순차적으로 또는 동시에 실행할 수 있는 기능
        // Append(): 시퀀스에 애니메이션을 추가 (순차 실행)
        // Join(): 이전 애니메이션과 동시에 실행할 애니메이션 추가
        // Prepend(): 시퀀스의 맨 앞에 애니메이션 추가
        Sequence pushSequence = DOTween.Sequence();
        
        // 이동 - Ease.OutBack을 사용하여 목표 지점을 살짝 지나쳤다가 돌아오는 효과
        pushSequence.Append(target.transform.DOMove(targetPosition, 0.5f)
            .SetEase(Ease.OutBack));
        
        // 회전 애니메이션을 이동과 동시에 실행 (Join 사용)
        // pushSequence.Join(target.transform.DORotate(new Vector3(0, 0, Random.Range(-15f, 15f)), 0.5f)
        //     .SetEase(Ease.OutQuad));
    }

    // 오브젝트를 자연스럽게 회전시키는 함수
    // target: 회전시킬 오브젝트
    // duration: 회전 시간
    // angle: 회전할 각도 (도 단위)
    public void RotateObject(GameObject target, float duration, float angle)
    {
        // 현재 회전값에 angle을 더해서 회전
        Vector3 currentRotation = target.transform.eulerAngles;
        Vector3 targetRotation = new Vector3(currentRotation.x, currentRotation.y, currentRotation.z + angle);
        
        // Ease.OutQuad를 사용하여 자연스러운 감속 효과 적용
        target.transform.DORotate(targetRotation, duration)
            .SetEase(Ease.OutQuad);
    }
} 