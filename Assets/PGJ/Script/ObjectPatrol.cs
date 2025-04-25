using UnityEngine;

public class ObjectPatrol : MonoBehaviour
{
    [SerializeField] float minHigh = 0;
    [SerializeField] float maxHigh = 7.3f;
    [SerializeField] float speed = 0.2f;

    bool isUp = true;

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
                //player.stateMachine.die
                Debug.Log("플레이어 사망 상태");
            }
        }
    }
}
