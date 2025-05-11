using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MovingLavaTrigger : MonoBehaviour
{
    [SerializeField] MovingLava movingLava;
    [SerializeField] float targetLavaSpd;

    [Space(8)]
    [SerializeField] bool chaseOk;

    BoxCollider2D boxCol;

    void Awake()
    {
        boxCol = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Proc();

            boxCol.enabled = false;
        }
    }

    void Proc()
    {
        if (false == movingLava.gameObject.activeSelf)
        {
            movingLava.gameObject.SetActive(true);
        }

        StartCoroutine(ChasePlayerPos());
    }

    IEnumerator ChasePlayerPos()
    {
        if (true == chaseOk)
        {
            // 이미 추격 x좌표를 넘지 않았을 때만 추적
            if (movingLava.transform.position.x < new Vector2(transform.position.x - 90, transform.position.y).x)
            {
                movingLava.parentMoveSpeed = 0;

                while (true)
                {
                    yield return null;

                    Vector3 chasePos = new Vector2(transform.position.x - 90, transform.position.y);

                    movingLava.transform.position = Vector2.MoveTowards(movingLava.transform.position, chasePos, 20 * Time.deltaTime);

                    if (chasePos == movingLava.transform.position)
                    {
                        break;
                    }
                }
            }
        }

        movingLava.parentMoveSpeed = targetLavaSpd;
    }
}
