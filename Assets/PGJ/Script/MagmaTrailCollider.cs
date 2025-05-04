using UnityEngine;

public class MagmaTrailCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (col.TryGetComponent(out Player player))
            {
                if (player.stateMachine.currentState != player.playerDieState)
                {
                    EventManager.instance.PublishPlayerDamaged(999);
                    //player.stateMachine.ChangeState(player.playerDieState);
                }
            }
        }
    }
}
