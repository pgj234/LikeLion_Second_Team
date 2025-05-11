using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MovingLavaTrigger : MonoBehaviour
{
    [SerializeField] GameObject beforeTriggerObj = null;
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
            if (null != beforeTriggerObj)
            {
                beforeTriggerObj.SetActive(false);
            }

            boxCol.enabled = false;

            Proc();
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
            if (0 == string.Compare(gameObject.name, "06"))
            {
                movingLava.transform.position = new Vector3(-386.3f, movingLava.transform.position.y, movingLava.transform.position.z);
            }
            else
            {
                // 이미 추격 x좌표를 넘지 않았을 때만 추적
                if (movingLava.transform.position.x < new Vector2(transform.position.x - 87, transform.position.y).x)
                {
                    movingLava.parentMoveSpeed = 0;

                    while (true)
                    {
                        yield return null;

                        Vector3 chasePos = new Vector2(transform.position.x - 87, movingLava.transform.position.y);

                        movingLava.transform.position = Vector2.MoveTowards(movingLava.transform.position, chasePos, 20 * Time.deltaTime);

                        if (chasePos == movingLava.transform.position)
                        {
                            break;
                        }
                    }
                }
            }
        }

        movingLava.parentMoveSpeed = targetLavaSpd;
    }
}
