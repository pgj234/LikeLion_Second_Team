using UnityEngine;

public class PlayerCollisionCheck : MonoBehaviour
{

    private Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoubleJumpTrigger"))
        {
            player.collisionJumpState.SetTriggerCollider(collision);
            player.stateMachine.ChangeState(player.collisionJumpState);
        }
    }
}
