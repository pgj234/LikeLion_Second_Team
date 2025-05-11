using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{

    public Transform pointB; // B 지점의 Transform

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = pointB.position;
        }



    }
}
