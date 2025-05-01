using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float lifetime = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GameTimeManager.instance != null)
        {
            GameTimeManager.instance.AddProjectile(rb);
            Debug.Log($"Projectile {gameObject.name} registered to GameTimeManager, RB: {rb}");
        }
        else
        {
            Debug.LogError("GameTimeManager instance is null!");
        }
        Destroy(gameObject, lifetime);
    }

    void OnDestroy()
    {
        if (GameTimeManager.instance != null)
        {
            GameTimeManager.instance.RemoveProjectile(rb);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Projectile hit {collision.name}, Tag: {collision.tag}");
            GameTimeManager.instance.ApplySlowMotion();

            Player player = collision.GetComponent<Player>();
            if (player != null && player.stateMachine != null)
            {
                PlayerCollisionJumpState jumpState = player.collisionJumpState;
                jumpState.SetTriggerCollider(GetComponent<Collider2D>());
                jumpState.StopPlayer();
                player.stateMachine.ChangeState(jumpState);
            }
        }
    }
}