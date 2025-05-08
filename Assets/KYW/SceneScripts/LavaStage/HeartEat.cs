using UnityEngine;
using DG.Tweening;

public class HeartEat : MonoBehaviour
{
    private void Start()
    {
        // 1초마다 0.5초 동안 y축으로 한 바퀴 회전하는 애니메이션
        transform.DORotate(new Vector3(0, 360, 0), 0.5f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)
            .SetDelay(1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager.Instance.Heal(1);
            Destroy(gameObject);
        }
    }
} 