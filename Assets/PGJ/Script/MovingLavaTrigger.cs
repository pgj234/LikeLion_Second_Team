using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MovingLavaTrigger : MonoBehaviour
{
    [SerializeField] MovingLava movingLava;
    [SerializeField] float targetLavaSpd;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            MovingLavaSpeedSet();

            enabled = false;
        }
    }

    void MovingLavaSpeedSet()
    {
        if (false == movingLava.gameObject.activeSelf)
        {
            movingLava.gameObject.SetActive(true);
        }

        movingLava.parentMoveSpeed = targetLavaSpd;
    }
}
