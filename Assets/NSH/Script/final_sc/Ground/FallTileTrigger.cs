using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class FallTileTrigger : MonoBehaviour
{
    [SerializeField] float delayTime;
    [SerializeField] float shakeDuration = 0.1f;

    Rigidbody2D rb => GetComponent<Rigidbody2D>();

    float timer;

    bool isOn = false;

    void Awake()
    {
        timer = delayTime;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (true == isOn)
        {
            return;
        }

        if (col.gameObject.CompareTag("Player"))
        {
            if (col.gameObject.TryGetComponent(out Player player))
            {
                if (player.stateMachine.currentState == player.idleState || player.stateMachine.currentState == player.moveState)
                {
                    isOn = true;

                    StartCoroutine(FallProc());
                }
            }
        }
    }

    IEnumerator FallProc()
    {
        transform.DOShakePosition(shakeDuration, new Vector2(0.1f, 0), fadeOut: false).SetEase(Ease.InOutCirc).SetLoops(-1, LoopType.Yoyo);

        while (true)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                break;
            }

            yield return null;
        }

        transform.DOKill();

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 2.5f;
    }
}
