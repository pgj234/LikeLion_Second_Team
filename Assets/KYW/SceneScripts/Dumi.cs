using UnityEngine;
using System;

public class Dumi : MonoBehaviour
{
    [SerializeField] private float pushForce = 2f;  // 밀어내는 힘
    [SerializeField] private float damage = 1f;     // 데미지량

    [Header("충돌 시 실행할 효과")]
    [SerializeField] private bool usePushEffect = true;    // 밀기 효과 사용 여부
    [SerializeField] private bool useDamageEffect = true;  // 데미지 효과 사용 여부
    [SerializeField] private bool useFadeEffect = false;   // 페이드 효과 사용 여부

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            // 칼의 방향 계산
            Vector2 swordDirection = (transform.position - other.transform.position).normalized;
            
            // 선택된 효과 실행
            if (usePushEffect)
            {
                ApplyPushEffect(swordDirection);
            }
            
            if (useDamageEffect)
            {
                TakeDamage();
            }
            
            if (useFadeEffect)
            {
                ApplyFadeEffect();
            }
        }
    }

    // 밀기 효과 적용
    private void ApplyPushEffect(Vector2 direction)
    {
        DoTweenManager.instance.PushObject(gameObject, direction);
    }

    // 데미지 처리
    private void TakeDamage()
    {
        Debug.Log("Dumi가 데미지를 받았습니다!");
        // 여기에 데미지 처리 로직 추가
    }

    // 페이드 효과 적용
    private void ApplyFadeEffect()
    {
        DoTweenManager.instance.FadeOut(gameObject, 0.5f);
    }
} 