using UnityEngine;

public class LavaSlimeDieState : LavaSlimeState
{
    private float destroyDelay = 1f;
    private float destroyTimer;

    public LavaSlimeDieState(LavaSlime lavaSlime, string animationParamName) : base(lavaSlime, animationParamName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        destroyTimer = destroyDelay;

        // 콜라이더 비활성화
        Collider2D collider = lavaSlime.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Rigidbody2D 비활성화
        if (lavaSlime.rb != null)
        {
            lavaSlime.rb.simulated = false;
        }
    }

    public override void Update()
    {
        destroyTimer -= Time.deltaTime;
        if (destroyTimer <= 0)
        {
            GameObject.Destroy(lavaSlime.gameObject);
        }
    }
} 