using UnityEngine;

public class PlayerParryingState : PlayerState
{
    private float slowMotionScale = 0.3f;
    private float normalTimeScale = 1f;
    private float parryForce = 30f;
    private LayerMask parryableLayer;
    private Camera mainCamera;
    private bool exitRequested = false;
    private float stateDuration = 2f;
    private float stateTimer = 0f;
    private float parryAnimationDuration = 0.4f;
    private float parryAnimationTimer = 0f;
    private bool isParrying = false;


    public PlayerParryingState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.anim.SetBool("ParryingReady", true);
        player.anim.SetBool("Parrying", false);
        player.anim.SetBool("Idle", false);
        mainCamera = Camera.main;
        parryableLayer = LayerMask.GetMask("Parryable");
        Time.timeScale = slowMotionScale;
        SkillManager.instance.ParrySkill.DotsActive(true);
        SkillManager.instance.ParrySkill.LineActive(true);
        stateTimer = 0f;
        exitRequested = false;
        isParrying = false;
        parryAnimationTimer = 0f;
    }

    public override void Update()
    {
        base.Update();
        SkillManager.instance.ParrySkill.UpdateDots();
        SkillManager.instance.ParrySkill.UpdateLine();

        if (isParrying)
        {
            parryAnimationTimer += Time.deltaTime;
            if (parryAnimationTimer >= parryAnimationDuration)
            {
                InputManager.instance.ParryPressed = false;
                player.anim.SetBool("Parrying", false);
                player.anim.SetBool("Idle", true);
                stateMachine.ChangeState(player.idleState);
                return;
            }
            return;
        }

        stateTimer += Time.deltaTime;
        if (stateTimer >= stateDuration)
        {
            InputManager.instance.ParryPressed = false;
            player.anim.SetBool("ParryingReady", false);
            player.anim.SetBool("Parrying", false);
            player.anim.SetBool("Idle", true);
            stateMachine.ChangeState(player.idleState);
            return;
        }

        if (InputManager.instance.leftClick)
        {
            bool parrySuccess = PerformParry();
            Time.timeScale = normalTimeScale;
            InputManager.instance.ParryPressed = false;
            isParrying = true;
            parryAnimationTimer = 0f;
            player.anim.SetBool("ParryingReady", false);
            player.anim.SetBool("Parrying", true);
            return;
        }

        if (InputManager.instance.xInput != 0)
        {
            player.SetVelocity(InputManager.instance.xInput * player.moveSpd, rb.linearVelocity.y);
        }
        else
        {
            player.SetVelocity(0, rb.linearVelocity.y);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("ParryingReady", false);
        player.anim.SetBool("Parrying", false);
        Time.timeScale = normalTimeScale;
        if (SkillManager.instance != null && SkillManager.instance.ParrySkill != null)
        {
            SkillManager.instance.ParrySkill.DotsActive(false);
            SkillManager.instance.ParrySkill.LineActive(false);
        }

    }

    private bool PerformParry()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(player.ParryingCheck.position, player.ParryingCheckRadius, parryableLayer);
        bool parrySuccess = false;
        if (hits.Length > 0)
        {
            Vector3 mouseWorldPos = SkillManager.instance.ParrySkill.GetMouseWorldPositionPublic();
            foreach (var hit in hits)
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // 기존 속도 초기화
                    rb.linearVelocity = Vector2.zero;
                    // 중력 일시 비활성화
                    float originalGravity = rb.gravityScale;
                    rb.gravityScale = 0f;
                    // 마우스 방향으로만 힘 적용
                    Vector3 direction = (mouseWorldPos - hit.transform.position).normalized;
                    rb.AddForce(direction * parryForce, ForceMode2D.Impulse);
                    // 0.5초 후 중력 복구
                    player.StartCoroutine(ResetGravity(rb, originalGravity, 0.5f));
                    parrySuccess = true;
                }
            }
        }
        if (parrySuccess)
        {
            AudioSource audio = player.GetComponent<AudioSource>();
            if (audio != null) audio.Play();
        }
        return parrySuccess;
    }

    private System.Collections.IEnumerator ResetGravity(Rigidbody2D rb, float originalGravity, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (rb != null)
        {
            rb.gravityScale = originalGravity;
            Debug.Log($"Gravity restored for {rb.gameObject.name}: {originalGravity}");
        }
    }

}