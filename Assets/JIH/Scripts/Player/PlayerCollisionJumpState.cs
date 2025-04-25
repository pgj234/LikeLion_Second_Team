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

    public override void Enter()
    {
        base.Enter();

        if (triggerCollider != null && triggerCollider.CompareTag("CollisionJump"))
        {
            // 속도 0으로 설정해 플레이어 고정
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f; // 중력 비활성화
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
            // 마우스 클릭 대기 중 플레이어 고정
            rb.linearVelocity = Vector2.zero;

            // 방향 화살표 위치 및 회전 업데이트
            if (player.directionArrow != null)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 jumpDirection = (mousePos - (Vector2)player.transform.position).normalized;
                // 플레이어 위치에서 2 유닛 떨어진 곳에 화살표 배치
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
            if (player.isWalled)
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

        // 콜라이더 복원 (필요한 경우)
        if (triggerCollider != null)
        {
            triggerCollider.enabled = true;
        }

        rb.gravityScale = player.JumpGravity;
    }
}