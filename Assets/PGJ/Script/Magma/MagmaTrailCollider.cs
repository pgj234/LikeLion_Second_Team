using System.Collections;
using UnityEngine;

public class MagmaTrailCollider : MonoBehaviour
{
    internal void StartProc(float lifeTime)
    {
        EventManager.instance.OnPlayerRespawned += Destroy;

        StartCoroutine(DestroyTimer(lifeTime));
    }

    IEnumerator DestroyTimer(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy();
    }

    internal void Destroy()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        EventManager.instance.OnPlayerRespawned -= Destroy;
    }

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
