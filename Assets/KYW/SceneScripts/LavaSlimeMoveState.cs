using UnityEngine;

public class LavaSlimeMoveState : LavaSlimeState
{
    public LavaSlimeMoveState(LavaSlime lavaSlime, string animationParamName) : base(lavaSlime, animationParamName)
    {
    }

    public override void Update()
    {
        if (lavaSlime.DetectPlayer())
        {
            Transform player = lavaSlime.GetDetectedPlayer();
            Vector2 direction = (player.position - lavaSlime.transform.position).normalized;
            lavaSlime.SetDirection(direction);
            
            // 플레이어 방향으로 이동
            Vector2 movement = direction * lavaSlime.moveSpeed * Time.deltaTime;
            lavaSlime.transform.position += new Vector3(movement.x, movement.y, 0);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            lavaSlime.TakeDamage();
        }
    }
} 