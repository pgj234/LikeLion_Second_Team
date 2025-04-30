using UnityEngine;
using DG.Tweening;

public class GhostDieState : GhostState
{
    private float destroyTimer = 0f;
    private const string IS_DIE = "IsDie";
    private bool isDestroying = false;

    public GhostDieState(Ghost ghost) : base(ghost)
    {
    }

    public override void Enter()
    {
        // Die 애니메이션 재생
        ghost.animator.SetBool(IS_DIE, true);

        // Rigidbody2D 설정
        ghost.rb.gravityScale = ghost.gravityScale;
        ghost.rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // 90도 회전하여 옆으로 눕기
        ghost.transform.DORotate(new Vector3(0, 0, 90f), 0.5f);

        // 알파값 점점 투명하게 만들기
        Color startColor = ghost.spriteRenderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        ghost.spriteRenderer.DOColor(endColor, 1f);

        // 1초 후에 오브젝트 삭제
        DOVirtual.DelayedCall(1f, () => {
            if (ghost != null && ghost.gameObject != null)
            {
                Object.Destroy(ghost.gameObject);
            }
        });
    }

    public override void Update()
    {
        // Update에서는 타이머를 사용하지 않고 DOTween의 DelayedCall을 사용
    }

    public override void Exit()
    {
        // Die 애니메이션 종료
        ghost.animator.SetBool(IS_DIE, false);
    }
} 