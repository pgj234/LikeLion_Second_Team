using System.Collections;
using UnityEngine;

public class CircleRockRotateMove : MonoBehaviour
{
    [SerializeField] Vector2 destinationPos;
    [SerializeField] float moveSpd;
    [SerializeField] float rotateSpd;
    [SerializeField] float boostSpd;

    Rigidbody2D rb;

    float boostTimer = 0;

    bool grounded = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (true == grounded)
        {
            return;
        }

        if (col.CompareTag("Ground"))
        {
            grounded = true;
            rb.bodyType = RigidbodyType2D.Static;
            //SoundManager.instance.PlaySFX(SFX_PGJ.용암에 돌 떨어지는 소리);

            StartCoroutine(Proc());
        }
    }

    IEnumerator Proc()
    {
        while (true)
        {
            yield return null;

            if (1 > boostTimer)
            {
                boostTimer += Time.deltaTime * boostSpd;
            }
            Debug.Log(boostTimer);
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, destinationPos, moveSpd * boostTimer * Time.deltaTime);
            transform.Rotate(new Vector3(0, 0, -rotateSpd) * boostTimer * Time.deltaTime);

            if (destinationPos.x <= transform.localPosition.x)
            {
                break;
            }
        }

        //SoundManager.instance.PlaySFX(SFX_PGJ.바위와 바위가 부딪히는 소리);
    }
}
