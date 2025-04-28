using UnityEngine;

public class ObjectPatrol : MonoBehaviour
{
    [SerializeField] float minHigh = 0;
    [SerializeField] float maxHigh = 7.3f;
    [SerializeField] float speed = 0.2f;

    bool isUp = true;

    void Awake()
    {
        EventManager.instance.OnPlayerRespawned += InitPos;
    }

    void Update()
    {
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
                // 임시로 그냥 바로 세이브 포인트로

                // 데미지 이벤트 발생
                EventManager.instance.PublishPlayerDamaged(999);
                player.stateMachine.ChangeState(player.playerDieState);          // 사망 스테이트로
            }
        }
    }

    void OnDestroy()
    {
        EventManager.instance.OnPlayerRespawned -= InitPos;
    }

    void InitPos()
    {
        transform.position = new Vector3(0, minHigh);
    }
}
