using UnityEngine;

public class DashSkill : MonoBehaviour
{
    [Header("대시 관련")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float staminaCost = 20f; // 대시 스킬 사용 시 소모되는 스태미나

    private Vector2 dashDirection;
    private float currentDashTimer;
    private bool isDashing;
    private int previousExpression; // 이전 표정 저장

    public void StartDash(Player player, Rigidbody2D rb)
    {
        // 스태미나가 충분한지 확인
        if (!PlayerManager.Instance.UseStamina(staminaCost))
        {
            return; // 스태미나가 부족하면 대시를 시작하지 않음
        }

        isDashing = true;
        currentDashTimer = dashDuration;
        rb.gravityScale = 0;

        // 방향 설정
        dashDirection = new Vector2(InputManager.instance.xInput, 0).normalized;
        if (dashDirection == Vector2.zero)
            dashDirection = Vector2.right * player.faceDir;
        
        // 이전 표정 저장
        previousExpression = PlayerManager.Instance.GetCurrentExpression();
        // 대시 시작 시 표정 변경
        PlayerManager.Instance.SetExpression(2); // 대시 표정
    }

    public void UpdateDash(Player player, Rigidbody2D rb)
    {
        if (!isDashing) return;

        currentDashTimer -= Time.deltaTime;
        player.SetVelocity(dashDirection.x * dashSpeed, dashDirection.y * dashSpeed);

        if (currentDashTimer <= 0f)
        {
            EndDash(rb, player.JumpGravity);
        }
    }

    public void EndDash(Rigidbody2D rb, float jumpGravity)
    {
        isDashing = false;
        rb.gravityScale = jumpGravity;
        // 대시 종료 시 이전 표정으로 복원
        PlayerManager.Instance.SetExpression(previousExpression);
    }

    public bool IsDashing()
    {
        return isDashing;
    }
} 