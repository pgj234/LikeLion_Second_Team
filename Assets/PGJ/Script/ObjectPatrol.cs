using UnityEngine;

public class ObjectPatrol : MonoBehaviour
{
    [SerializeField] float minHigh = 0;
    [SerializeField] float maxHigh = 7.3f;
    [SerializeField] float speed = 0.2f;

    Vector2 originalPos;

    bool isUp;

    void Start()
    {
        if (0 == speed)
        {
            return;
        }

        originalPos = transform.position;

        EventManager.instance.OnPlayerRespawned += InitPos;
    }

    void Update()
    {
        if (0 == speed)
        {
            return;
        }

        if (transform.position.y < minHigh + 0.1f)
        {
            isUp = true;
        }
        else if (transform.position.y > maxHigh - 0.1f)
        {
            isUp = false;
        }

        if (true == isUp)
        {
            transform.Translate(new Vector2(transform.position.x, Time.deltaTime * speed));
        }
        else
        {
            transform.Translate(new Vector2(transform.position.x, Time.deltaTime * -speed));
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.TryGetComponent(out Player player))
            {
                // 데미지 이벤트 발생
                EventManager.instance.PublishPlayerDamaged(999);
                player.stateMachine.ChangeState(player.playerDieState);          // 사망 스테이트로
            }
        }
    }

    void OnDestroy()
    {
        if (0 == speed)
        {
            return;
        }

        EventManager.instance.OnPlayerRespawned -= InitPos;
    }

    void InitPos()
    {
        if (0 == speed)
        {
            return;
        }

        isUp = false;
        transform.position = originalPos;
    }
}
