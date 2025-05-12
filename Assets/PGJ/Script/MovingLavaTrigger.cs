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

    float minusX = 80;

    bool isSpecial_06 = false;

    void Awake()
    {
        boxCol = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        if (0 == string.Compare(gameObject.name, "01"))
        {
            minusX = 82;
        }
        else if (0 == string.Compare(gameObject.name, "03 - 2"))
        {
            minusX = 75;
        }
        else if (0 == string.Compare(gameObject.name, "04"))
        {
            minusX = 65;
        }
        else if (0 == string.Compare(gameObject.name, "06"))
        {
            isSpecial_06 = true;
        }
        else if (0 == string.Compare(gameObject.name, "10"))
        {
            minusX = 60;
        }
        else if (0 == string.Compare(gameObject.name, "12"))
        {
            minusX = 30;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (null != beforeTriggerObj)
            {
                beforeTriggerObj.SetActive(false);
            }

            if (0 == string.Compare(gameObject.name, "21"))
            {
                movingLava.GetComponent<BoxCollider2D>().enabled = false;
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
            movingLava.parentMoveSpeed = 0;

            // 06번 스페셜
            if (true == isSpecial_06)
            {
                while (true)
                {
                    yield return null;

                    Vector3 chasePos = new Vector3(-386.3f, movingLava.transform.position.y, movingLava.transform.position.z);
                    movingLava.transform.position = Vector2.MoveTowards(movingLava.transform.position, chasePos, 12 * Time.deltaTime);

                    if (chasePos == movingLava.transform.position)
                    {
                        break;
                    }
                }
            }
            else
            {
                // 이미 추격 x좌표를 넘지 않았을 때만 추적
                if (movingLava.transform.position.x < new Vector2(transform.position.x - minusX, transform.position.y).x)
                {
                    Vector3 chasePos = Vector2.one;

                    while (true)
                    {
                        yield return null;

                        chasePos = new Vector2(transform.position.x - minusX, movingLava.transform.position.y);
                        movingLava.transform.position = Vector2.Lerp(movingLava.transform.position, chasePos, 1.2f * Time.deltaTime);

                        if (chasePos.x - 0.5f < movingLava.transform.position.x)
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
