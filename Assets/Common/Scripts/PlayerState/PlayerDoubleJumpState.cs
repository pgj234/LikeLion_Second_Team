using UnityEngine;
using DG.Tweening;

public class PlayerDoubleJumpState : PlayerState
{
    private float Jumptimer;
    private Sequence rotationSequence;

    public PlayerDoubleJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.doubleJumpPower);
        Jumptimer = player.DoubleJumpTime;

        // 점프 이펙트 생성
        if (player.jumpEffectPoint != null)
        {
            EffectManager.instance.SpawnJumpEffect(
                player.jumpEffectPoint.position,
                player.jumpEffectPoint.rotation,
                0.1f
            );
        }
        // 스프라이트만 3바퀴 회전 (로컬 회전 사용)
        rotationSequence = DOTween.Sequence();
        rotationSequence.Append(player.spriteRenderer.transform.DOLocalRotate(new Vector3(0, 0, 1080), player.DoubleJumpTime, RotateMode.FastBeyond360))
            .SetRelative(true) // 상대적 회전으로 변경
            .SetEase(Ease.Linear); // 선형 회전으로 변경
    }

    public override void Update()
    {
        base.Update();

        // 점프 유지 중
        if (InputManager.instance.jumpHold && Jumptimer > 0)
        {
            // 점프 힘 보간: 점점 줄어듦
            float t = 1 - (Jumptimer / player.DoubleJumpTime); // 0 → 1
            float jumpForce = Mathf.Lerp(player.doubleJumpPower, 0, t); // 점점 줄어듦
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            Jumptimer -= Time.deltaTime;
        }

        // 점프 키 뗐을 때 강제 컷
        if (InputManager.instance.jumpReleased)
        {
            Jumptimer = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }

        // 벽 감지 및 입력 방향 체크
        if (player.isWalled)
        {
            // 벽이 왼쪽에 있고 왼쪽 키를 누르거나, 벽이 오른쪽에 있고 오른쪽 키를 누를 때
            if (((player.faceDir == -1 && InputManager.instance.xInput < 0) || 
                (player.faceDir == 1 && InputManager.instance.xInput > 0)) && 
                PlayerManager.Instance.CurrentStamina >= 25)
            {
                stateMachine.ChangeState(player.wallslideState);
                return;
            }
        }

        //하강 감지 → 낙하 상태로 전환
        if (rb.linearVelocityY < 0f)
        {
            stateMachine.ChangeState(player.fallState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
