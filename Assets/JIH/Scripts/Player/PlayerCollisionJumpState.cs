using UnityEngine;

public class PlayerCollisionJumpState : PlayerState
{

    private bool hasJumped;
    private Vector2 initialPosition;
    private Collider2D triggerCollider;

    public PlayerCollisionJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public void SetTriggerCollider(Collider2D collider)
    {
        triggerCollider = collider;
    }

    public override void Enter()
    {
        base.Enter();
        hasJumped = false;
        initialPosition = player.transform.position;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;

        // 화살표 오브젝트 활성화
        if (player.directionArrow != null)
        {
            player.directionArrow.SetActive(true);
        }
    }

    public override void Update()
    {
        base.Update();

        if (!hasJumped)
        {
            player.transform.position = initialPosition;

            // 마우스 커서 방향으로 화살표 회전 및 위치 조정
            if (player.directionArrow != null)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (mousePos - (Vector2)player.transform.position).normalized;

                // 화살표 위치를 플레이어 근처로 설정
                player.directionArrow.transform.position = player.transform.position + (Vector3)(direction * 10f); // 0.5f는 플레이어와의 거리

                // 화살표 회전 (2D에서 Z축 회전)
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                player.directionArrow.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        if (!hasJumped && Input.GetMouseButtonDown(0))
        {
            rb.gravityScale = 1f;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)player.transform.position).normalized;
            rb.linearVelocity = direction * player.doubleJumpPower;
            hasJumped = true;

            // 트리거 콜라이더 비활성화
            if (triggerCollider != null)
            {
                triggerCollider.enabled = false;
            }

            // 화살표 오브젝트 비활성화
            if (player.directionArrow != null)
            {
                player.directionArrow.SetActive(false);
            }

            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = 1f;

        // 상태 종료 시 화살표 비활성화
        if (player.directionArrow != null)
        {
            player.directionArrow.SetActive(false);
        }
    }
}
