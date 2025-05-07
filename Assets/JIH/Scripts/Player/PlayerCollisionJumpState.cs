using UnityEngine;

public class PlayerCollisionJumpState : PlayerState
{
    private float jumpTimer;
    private Collider2D triggerCollider;
    private bool isWaitingForClick;

    public PlayerCollisionJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public void SetTriggerCollider(Collider2D _collider)
    {
        triggerCollider = _collider;
    }

    public void StopPlayer()
    {
        rb.linearVelocity = Vector2.zero; // 플레이어 정지
        rb.gravityScale = 0f; // 중력 비활성화
    }

    public override void Enter()
    {
        base.Enter();

        if (triggerCollider != null && triggerCollider.CompareTag("CollisionJump"))
        {
            StopPlayer(); // 초기 정지
            isWaitingForClick = true;

            // 충돌한 오브젝트의 콜라이더 비활성화
            triggerCollider.enabled = false;

            // 방향 화살표 표시
            if (player.directionArrow != null)
            {
                player.directionArrow.SetActive(true);
            }
        }
    }

    public override void Update()
    {
        base.Update();

        if (isWaitingForClick)
        {
            // 정지 상태 유지
            rb.linearVelocity = Vector2.zero;

            // 방향 화살표 위치 및 회전 업데이트
            if (player.directionArrow != null)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 jumpDirection = (mousePos - (Vector2)player.transform.position).normalized;
                player.directionArrow.transform.position = (Vector2)player.transform.position + jumpDirection * 2f;
                float angle = Mathf.Atan2(jumpDirection.y, jumpDirection.x) * Mathf.Rad2Deg;
                player.directionArrow.transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            // 마우스 클릭 시 점프
            if (InputManager.instance.leftClick)
            {
                isWaitingForClick = false;
                rb.gravityScale = player.JumpGravity; // 중력 복원

                // 마우스 커서 방향 계산
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 jumpDirection = (mousePos - (Vector2)player.transform.position).normalized;

                // 점프 힘 적용
                rb.linearVelocity = jumpDirection * player.collisionJumpPower;
                jumpTimer = player.objectJumpTime;

                SoundManager.instance.PlaySFX(SFX_JIH.JumpSound);
            }
        }
        else
        {
            // 점프 중 로직
            if (jumpTimer > 0)
            {
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                // 점프 시간 종료 시 낙하 상태로 전환
                if (rb.linearVelocityY < 0)
                {
                    stateMachine.ChangeState(player.fallState);
                }
            }

            // 착지 감지
            if (player.isGrounded)
            {
                player.DoubleJumpCount = player.MaxDoubleJumpCount;
                stateMachine.ChangeState(player.idleState);
            }

            // 벽 감지
            if (player.isWalled && PlayerManager.Instance.CurrentStamina >= 25)
            {
                stateMachine.ChangeState(player.wallslideState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        // 방향 화살표 비활성화
        if (player.directionArrow != null)
        {
            player.directionArrow.SetActive(false);
        }

        // 콜라이더 복원
        if (triggerCollider != null)
        {
            triggerCollider.enabled = true;
        }

        rb.gravityScale = player.JumpGravity;

    }
    
}