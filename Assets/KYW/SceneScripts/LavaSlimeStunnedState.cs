using UnityEngine;

public class LavaSlimeStunnedState : LavaSlimeState
{
    private float stunTimer;

    public LavaSlimeStunnedState(LavaSlime lavaSlime, string animationParamName) : base(lavaSlime, animationParamName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stunTimer = lavaSlime.stunDuration;
        lavaSlime.spriteRenderer.material = lavaSlime.stunMaterial;

        // 피격 효과 생성
        if (lavaSlime.hitParticlePrefab != null)
        {
            GameObject.Instantiate(lavaSlime.hitParticlePrefab, lavaSlime.transform.position, Quaternion.identity);
        }
    }

    public override void Exit()
    {
        base.Exit();
        lavaSlime.spriteRenderer.material = lavaSlime.originalMaterial;
    }

    public override void Update()
    {
        stunTimer -= Time.deltaTime;
        if (stunTimer <= 0)
        {
            lavaSlime.stateMachine.ChangeState(lavaSlime.moveState);
        }
    }
} 